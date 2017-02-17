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
        EFRailWay_KIS_EFOracleRulesCopyRepository = 2102,
        EFRailWay_KIS_EFOracleInputSostavRepository = 2103,
        EFRailWay_KIS_EFOracleOutputSostavRepository = 2104, 
        EFRailWay_KIS_RulesCopy = 2110,
        EFRailWay_KIS_ArrivalSostav = 2111,
        EFRailWay_KIS_InputSostav = 2112,

        EFRailWay_RailWay = 2300,
        EFRailWay_RailWay_EFReferenceRailwayRepository = 2301,
        EFRailWay_RailWay_EFReferenceCargoRepository = 2302,
        EFRailWay_RailWay_EFReferenceCountryRepository = 2303,
        EFRailWay_RailWay_EFReferenceStationRepository= 2304,
        EFRailWay_RailWay_EFCodeCountryRepository = 2305,

        EFRailWay_MT = 2400,
        EFRailWay_MT_MTContent = 2401,

        EFRailWay_SAP = 2500,
        EFRailWay_SAP_EFSAPIncSupplyRepository = 2501,

        EFRailWay_References = 2600,
        EFRailWay_References_ReferenceRailway = 2601,
        EFRailWay_References_GeneralReferences = 2602,


        
        
        EFWagons= 3000,
        EFWagons_KIS= 3100,
        EFWagons_KIS_PromContent = 3101,
        EFWagons_KIS_KometaContent = 3102,
        EFWagons_KIS_VagonsContent = 3103,
        
        KIS= 4000,
        KIS_Service=4100,
        KIS_Service_ArrivalKIS = 4101,
        KIS_Service_SynchronizeKIS =4102,
        KIS_Service_CopyingInlandKIS = 4103,

        MetallurgTrans= 5000,
        MetallurgTrans_Service = 5100,
        MetallurgTrans_Service_ArrivalMT = 5101,
        MetallurgTrans_Helpers  = 5200,
        MetallurgTrans_Helpers_MT  = 5201,
        MetallurgTrans_SFTP = 5300,
        MetallurgTrans_SFTP_SFTPClient = 5301,

        TransferWagons = 6000,
        TransferWagons_Transfers = 6100,
        TransferWagons_Transfers_Transfer = 6101, 
        TransferWagons_Transfers_SAP_Transfer = 6102,
        TransferWagons_Transfers_KIS_Transfer = 6103,
        TransferWagons_Transfers_References = 6104,
        TransferWagons_RailCars= 6300,
        TransferWagons_RailWay= 6400,
        TransferWagons_SAP = 6500,
        TransferWagons_SAP_References = 6501,
        TransferWagons_KIS = 6600,
        TransferWagons_KIS_ReferencesKIS = 6601,

        EFRailCars = 7000,
        EFRailCars_Helpers =7100,
        EFRailCars_Helpers_Maneuvers =7101,

        EFRailCars_RailCars = 7200,
        EFRailCars_RailCars_EFVagonsOperationsRepository = 7201,
        EFRailCars_RailCars_EFVagonsRepository = 7202,
        EFRailCars_RailCars_EFShopsRepository = 7203,


        EFRailCars_RailCars_EFTupikiRepository = 7206,

        EFRailCars_RailCars_RC_VagonsOperations = 7211,
        EFRailCars_RailCars_RC_Vagons = 7212,
        EFRailCars_RailCars_RC_Shops = 7213,
        EFRailCars_RailCars_RC_Stations = 7214,
        EFRailCars_RailCars_RC_Ways = 7215,
        EFRailCars_RailCars_RC_Tupiki =7216 
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
            LogRW.LogError(String.Format("[" + source + "] : Ошибка источник: {0}, № {1}, описание:  {2}, InnerException:  {3})", e.Source, e.HResult, e.Message, e.InnerException.Message), (int)eventID);
        }
    }
}
