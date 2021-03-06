﻿using EFRailWay.Settings;
using Logs;
//using LogsRailWay;
using MetallurgTrans.Helpers;
using MetallurgTrans.SFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetallurgTrans.Service
{
    public class ArrivalMT
    {
        private eventID eventID = eventID.MetallurgTrans_Service_ArrivalMT;
        private string className = "ArrivalMT";
        private string classDescription = "Прибытие составов на станции Кривого Рога (информация МеталлургТранс)";
        private bool error_settings = false;
        private connectSFTP connect_SFTP;
        private string fromPathHost;
        private string FileFiltrHost;
        private string toDirPath = null;
        private string toTMPDirPath = null;
        private bool DeleteFile;
        private bool RewriteFile;
        private int dayMonitoringTrains;

        /// <summary>
        /// Конструктор
        /// </summary>
        public ArrivalMT()
        {
            try
            {
                Settings set = new Settings();
                set.Get_Project(this.className, this.classDescription, true); // Проверим наличие проекта
                // Получим настройки подключения 
                connect_SFTP = new connectSFTP()
                {
                    Host = set.GetStringSettingConfigurationManager("Host", this.className, true),
                    Port = (int)set.GetIntSettingConfigurationManager("Port", this.className, true),
                    User = set.GetStringSettingConfigurationManager("User", this.className, true),
                    PSW = set.GetStringSettingConfigurationManager("PSW", this.className, true)
                };
                this.fromPathHost = set.GetStringSettingConfigurationManager("fromPathsHost", this.className, true);
                this.FileFiltrHost = set.GetStringSettingConfigurationManager("FileFiltrHost", this.className, true);
                this.toDirPath = set.GetStringSettingConfigurationManager("toDirPath", this.className, true);
                this.toTMPDirPath = set.GetStringSettingConfigurationManager("toTMPDirPath", this.className, true);
                this.DeleteFile = (bool)set.GetBoolSettingConfigurationManager("DeleteFile", this.className, true);
                this.RewriteFile = (bool)set.GetBoolSettingConfigurationManager("RewriteFile", this.className, true);
                this.dayMonitoringTrains = (int)set.GetIntSettingConfigurationManager("dayMonitoringTrains", this.className, true);
            }
            catch (Exception e)
            {
                error_settings = true;
                LogRW.LogError(String.Format("[ArrivalMT.ArrivalMT] : Ошибка выполнения инициализации classa {0} (источник: {1}, № {2}, описание:  {3})", this.className, e.Source, e.HResult, e.Message), this.eventID);
            }
        }
        /// <summary>
        /// Перенос вагонов
        /// </summary>
        public int Transfer()
        {
            MT mettrans;
            if (error_settings)
            {
                LogRW.LogWarning("Выполнение метода ArrivalMT.Transfer() - отменено, ошибка нет данных Settings.", this.eventID);
                return 0;
            }
            LogRW.LogInformation(String.Format("Сервис переноса вагонов из МТ в БД Railway :{0} - запущен", this.className), this.eventID);
            try
            {
                SFTPClient csftp = new SFTPClient(connect_SFTP);
                csftp.fromPathsHost = fromPathHost;
                csftp.FileFiltrHost = FileFiltrHost;
                csftp.toDirPath = toDirPath;
                csftp.toTMPDirPath = toTMPDirPath;
                csftp.DeleteFileHost = DeleteFile;
                csftp.RewriteFile = RewriteFile;
                csftp.CopyToDir();
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[ArrivalMT.Transfer]: Общая ошибка выполнения копирования из SFTP (источник: {0}, № {1}, описание:  {2})", e.Source, e.HResult, e.Message), this.eventID);
            }
            try
            {
                mettrans = new MT();
                mettrans.FromPath = toTMPDirPath;
                mettrans.DeleteFile = DeleteFile;
                mettrans.DayMonitoringTrains = dayMonitoringTrains;
                return mettrans.Transfer();
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[ArrivalMT.Transfer]: Общая ошибка переноса xml-файлов в БД Railway (источник: {0}, № {1}, описание:  {2})", e.Source, e.HResult, e.Message), this.eventID);
            }
            return 0;
        }
    }
}
