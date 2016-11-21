using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    public enum eventID : int
    {
        EFISA95 = 1000,
        EFRailWay= 2000,
        EFRailWay_KIS = 2100,
        EFRailWay_KIS_EFOracleArrivalSostavRepository = 2101,
        EFRailWay_RailCars = 2200,
        EFRailWay_RailCars_RC_VagonsOperations = 2201,
        EFRailWay_RailCars_EFVagonsOperationsRepository = 2202,
        EFRailWay_RailWay = 2300,
        EFWagons= 3000,
        KIS= 4000,
        MetallurgTrans= 5000,
        TransferWagons = 6000,
        TransferWagons_Transfer = 6100,
        TransferWagons_KIS_Transfer = 6100,
        TransferWagons_RailCars= 6300,
        TransferWagons_RailWay= 6400,
    }

    public class LogRW
    {
        static private EventLog elog;
        static public EventLog EL { get { return elog; } }
        static private string eventSourceName;
        static private string logName;

        static LogRW()
        {
            try
            {
                eventSourceName = ConfigurationManager.AppSettings["eventSourceName"].ToString();
                logName = ConfigurationManager.AppSettings["logName"].ToString();

            }
            catch (Exception e)
            {
                eventSourceName = "RailWay";
                logName = "RailWayLogFile1";
            }

            try
            {
                if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
                {
                    System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
                }
                elog = new System.Diagnostics.EventLog();
                elog.Source = eventSourceName; elog.Log = logName;
            }
            catch (Exception e)
            {
                elog = null;
            }
        }

        /// <summary>
        /// Логирование Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        static public void LogError(string message, int eventID)
        {
            if (elog != null) elog.WriteEntry(message, EventLogEntryType.Error, eventID);
        }
        /// <summary>
        /// Логирование Warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        static public void LogWarning(string message, int eventID)
        {
            if (elog != null) elog.WriteEntry(message, EventLogEntryType.Warning, eventID);
        }
        /// <summary>
        /// Логирование Information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        static public void LogInformation(string message, int eventID)
        {
            if (elog != null) elog.WriteEntry(message, EventLogEntryType.Information, eventID);
        }
        /// <summary>
        /// Логирование Error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        static public void LogError(string message, eventID eventID)
        {
            LogError(message, (int)eventID);
        }
        /// <summary>
        /// Логирование Warning
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        static public void LogWarning(string message, eventID eventID)
        {
            LogWarning(message, (int)eventID);
        }
        /// <summary>
        /// Логирование Information
        /// </summary>
        /// <param name="message"></param>
        /// <param name="eventID"></param>
        static public void LogInformation(string message, eventID eventID)
        {
            LogInformation(message, (int)eventID);
        }

        static public void LogError(Exception e, string source, eventID eventID)
        {
            LogRW.LogError(String.Format("[" + source + "] : Ошибка источник: {0}, № {1}, описание:  {2})", e.Source, e.HResult, e.Message), (int)eventID);
        }
    }
}
