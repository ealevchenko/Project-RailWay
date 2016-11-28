using EFRailWay.Entities;
using EFRailWay.Settings;
using KIS.Service;
using Logs;
//using LogsRailWay;
using MetallurgTrans.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RWServiceArrival
{
    public partial class RWServiceArrival : ServiceBase
    {
        private int eventID = 0;
        private int Interval = 60000;
        private bool runTimer = false;
        private string ServiceDescription = "Перенос вагонов на станции АМКР";

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public long dwServiceType;
            public ServiceState dwCurrentState;
            public long dwControlsAccepted;
            public long dwWin32ExitCode;
            public long dwServiceSpecificExitCode;
            public long dwCheckPoint;
            public long dwWaitHint;
        };

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        public RWServiceArrival(string[] args)
        {
            InitializeComponent();
            InitializeService(); 
        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            LogRW.LogWarning(String.Format("Сервис {0} - запущен. Интервал выполнения - {1} мсек.", this.ServiceName, this.Interval.ToString()), this.eventID);
            // Set up a timer to trigger every minute.
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = this.Interval; // 60 seconds
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnStop()
        {
            LogRW.LogWarning(String.Format("Сервис {0} - остановлен", this.ServiceName), this.eventID);
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            try
            {
                if (!runTimer)
                {
                    DateTime dt_start = DateTime.Now;
                    LogRW.LogInformation(String.Format("Сервис {0} - активен", this.ServiceName), this.eventID);
                    ArrivalMT amt = new ArrivalMT();
                    amt.Transfer();
                    ArrivalKIS akis = new ArrivalKIS();
                    akis.Transfer();
                    SynchronizeKIS skis = new SynchronizeKIS();
                    skis.Synchronize();
                    TimeSpan ts = DateTime.Now - dt_start;
                    LogRW.LogInformation(String.Format("Сервис {0} - время выполнения: {1} мин {2} сек {3} мсек", this.ServiceName,ts.Minutes, ts.Seconds, ts.Milliseconds), this.eventID);
                }
                else 
                {
                    LogRW.LogWarning(String.Format("Сервис {0} - занят", this.ServiceName), this.eventID);
                }
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[RWServiceArrival.OnTimer] : Общая ошибка выполнения сервиса {0} (источник: {1}, № {2}, описание:  {3})", this.ServiceName, e.Source, e.HResult, e.Message), this.eventID);
            }
            finally 
            {
                runTimer = false;
                //LogRW.LogInformation(String.Format("Сервис {0} - не активен", this.ServiceName), this.eventID);
            }
        }
        /// <summary>
        /// Инициализация сервиса (проверка данных в БД и создание settings)
        /// </summary>
        public void InitializeService() 
        {
            try
            {
                Settings set = new Settings();
                set.InsertTypeValue(); // Обновим типы
                // Проверим наличие секций проекты
                set.Get_Project(this.ServiceName, this.ServiceDescription, true);
                this.Interval = (int)set.GetIntSettingConfigurationManager("Interval", this.ServiceName, true);
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[RWServiceArrival.InitializeService] : Ошибка выполнения инициализации сервиса {0}  (источник: {1}, № {2}, описание:  {3})", this.ServiceName,  e.Source, e.HResult, e.Message), this.eventID);
                return;
            } 
            

        }

    }
}
