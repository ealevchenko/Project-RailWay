﻿using EFRailWay.Settings;
//using KIS.Helpers;
using Logs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferWagons.Transfers;

namespace KIS.Service
{
    public class ArrivalKIS
    {
        private eventID eventID = eventID.KIS_Service_ArrivalKIS;
        private string className = "ArrivalKIS";
        private string classDescription = "Прибытие составов на станцию Восточная (информация КИС)";
        private bool error_settings = false;

        private bool active = false;        // признак активности копированиия данных
        private int mode = 0;               // режим копированиия данных
        private int dayControllingAddNatur; // контроль добавления натуральных листов

        public ArrivalKIS()
        {
            try
            {
                Settings set = new Settings();
                set.Get_Project(this.className, this.classDescription, true); // Проверим наличие проекта
                this.active = (bool)set.GetBoolSettingConfigurationManager("activeArrivalKIS", this.className, true);
                this.mode = (int)set.GetIntSettingConfigurationManager("modeArrivalKIS", this.className, true);
                this.dayControllingAddNatur = (int)set.GetIntSettingConfigurationManager("dayControllingAddNatur", this.className, true);
            }
            catch (Exception e)
            {
                error_settings = true;
                LogRW.LogError(String.Format("[ArrivalKIS]: Ошибка выполнения инициализации classa {0} (источник: {1}, № {2}, описание:  {3})", this.className, e.Source, e.HResult, e.Message), this.eventID);
            }
        }
        /// <summary>
        /// Перенести вагоны 
        /// </summary>
        /// <returns></returns>
        public int Transfer()
        {
            if (!this.active) {
                LogRW.LogWarning(String.Format("Сервис переноса данных из КИС :{0} - отключен. (Settings:activeArrivalKIS).",this.className), this.eventID);                
                return 0; }
            if (error_settings)
            {
                LogRW.LogWarning("Выполнение метода ArrivalKIS.Transfer() - отменено, ошибка нет данных Settings.", this.eventID);
                return 0;
            }
            KIS_Transfer kist = new KIS_Transfer();
            LogRW.LogInformation(String.Format("Сервис переноса данных из КИС в БД RailWay :{0} - запущен, режим копирования: {1}", this.className, this.mode), this.eventID);
            try
            {
                // Перенесем или обновим информацию о составах защедших на АМКР по системе КИС
                int result_cs = kist.CopyArrivalSostavToRailway(this.dayControllingAddNatur);
                int res_pc = 0;
                switch (this.mode) 
                { 
                    //case 0:
                    //    // Полное копирование из КИС
                    //    res_pc = kist.PutCarsToStations(0);
                    //    break;
                    case 1:
                        // Обновление данных скопированных из МТ с переносом из прибытия на станцию 
                        res_pc = kist.PutCarsToStations(1);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[ArrivalKIS.Transfer]: Общая ошибка переноса данных из БД КИС (источник: {0}, № {1}, описание:  {2})", e.Source, e.HResult, e.Message), this.eventID);
            }
            return 0; // TODO: исправить возврат
        }

    }
}
