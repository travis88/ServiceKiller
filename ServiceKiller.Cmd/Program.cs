using ServiceKiller.Core;
using System.Threading;

namespace ServiceKiller.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            ParamsReader reader = new ParamsReader();
            while (true)
            {
                Starter.Run(reader);
                Thread.Sleep(reader.TimeToWait);
            }
        }
    }
}
