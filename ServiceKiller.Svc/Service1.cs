using ServiceKiller.Core;
using System;
using System.ServiceProcess;
using System.Threading;

namespace ServiceKiller.Svc
{
    public partial class Service1 : ServiceBase
    {
        /// <summary>
        /// Разрешённость
        /// </summary>
        private bool enableIntegration = true;

        /// <summary>
        /// Поток
        /// </summary>
        private static Thread integrationWorker = null;

        /// <summary>
        /// Конструктор
        /// </summary>
        public Service1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Запуск
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                integrationWorker = new Thread(WannaKill);
                integrationWorker.Start();
            }
            catch (Exception e)
            {
                ServiceLogger.Error("{work}", $"глобальная ошибка {e.ToString()}");
            }
        }

        /// <summary>
        /// Остановка
        /// </summary>
        protected override void OnStop()
        {
            enableIntegration = false;
            integrationWorker.Abort();
        }

        /// <summary>
        /// Запуск ядра
        /// </summary>
        /// <param name="data"></param>
        private void WannaKill(object data)
        {
            ParamsReader reader = new ParamsReader();
            while (enableIntegration)
            {
                Starter.Run(reader);
                Thread.Sleep(reader.TimeToWait);
            }
        }
    }
}
