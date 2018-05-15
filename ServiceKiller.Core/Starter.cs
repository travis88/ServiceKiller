using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;

namespace ServiceKiller.Core
{
    /// <summary>
    /// Запуск
    /// </summary>
    public static class Starter
    {
        /// <summary>
        /// Запускает основную логику
        /// </summary>
        /// <param name="process"></param>
        public static void Run(ParamsReader reader)
        {
            string processName = reader.ProcessName;

            Process[] processes = Process.GetProcesses();

            foreach (Process process in processes.Where(w => w.ProcessName == processName))
            {
                var perfCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName);
                while (true)
                {
                    string owner = GetProcessOwner($"{processName}.exe");
                    try
                    {
                        Thread.Sleep(500);
                        float cpu = perfCounter.NextValue() / Environment.ProcessorCount;
                        //ServiceLogger.Info("{work}", $"{process.ProcessName}: {cpu} | owner: {owner}");

                        if (cpu > reader.CriticalUsage && owner == reader.ProcessOwner)
                        {
                            ServiceLogger.Info("{work}", $"process {process.ProcessName} killed");
                            process.Kill();
                        }
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Возвращает владельца процесса
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        private static string GetProcessOwner(string processName)
        {
            try
            {
                string query = "Select * from Win32_Process Where Name = \"" + processName + "\"";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                ManagementObjectCollection processList = searcher.Get();

                foreach (ManagementObject obj in processList)
                {
                    string[] argList = new string[] { string.Empty, string.Empty };
                    int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                    if (returnVal == 0)
                    {
                        string owner = argList[1] + "\\" + argList[0];
                        return owner;
                    }
                }
            }
            catch { }
            return "NO OWNER";
        }
    }
}
