using System;

namespace ServiceKiller.Core
{
    /// <summary>
    /// Считыватель параметров
    /// </summary>
    public class ParamsReader
    {
        /// <summary>
        /// Название процесса
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// Владелец процесса
        /// </summary>
        public string ProcessOwner { get; set; }

        /// <summary>
        /// Критическое значение по загрузке CPU
        /// </summary>
        public int CriticalUsage { get; set; }

        /// <summary>
        /// Время ожидания между попытками
        /// </summary>
        public int TimeToWait { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public ParamsReader()
        {
            ProcessName = System.Configuration.ConfigurationManager.AppSettings["ProcessName"];
            ProcessOwner = System.Configuration.ConfigurationManager.AppSettings["ProcessOwner"];
            CriticalUsage = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["CriticalUsage"]);
            TimeToWait = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["TimeToWait"]);
        }
    }
}
