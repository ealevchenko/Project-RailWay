using EFRailWay.Settings;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferWagons.Transfers;

namespace KIS.Service
{
    public class SynchronizeKIS
    {
        private eventID eventID = eventID.KIS_Service_SynchronizeKIS;

        private string className = "SynchronizeKIS";
        private string classDescription = "Синхронизация справочников КИС и справочников системы RailWay";
        private bool error_settings = false;

        private bool activeWagons = false;  // признак активности копированиия данных
        private int dayControllingWagons;   // кол. дней контроля изменения справочника.

        public SynchronizeKIS()
        {
            try
            {
                Settings set = new Settings();
                set.Get_Project(this.className, this.classDescription, true); // Проверим наличие проекта
                this.activeWagons = (bool)set.GetBoolSettingConfigurationManager("activeSynchronizeWagons", this.className, true);
                this.dayControllingWagons = (int)set.GetIntSettingConfigurationManager("dayControllingWagons", this.className, true);

            }
            catch (Exception e)
            {
                error_settings = true;
                LogRW.LogError(String.Format("[SynchronizeKIS]: Ошибка выполнения инициализации classa {0} (источник: {1}, № {2}, описание:  {3})", this.className, e.Source, e.HResult, e.Message), this.eventID);
            }
        }

        public int Synchronize()
        {
            if (error_settings)
            {
                LogRW.LogWarning("Выполнение метода SynchronizeKIS.Synchronize() - отменено, ошибка нет данных Settings.", this.eventID);
            }            
            if (!this.activeWagons)
            {
                LogRW.LogWarning(String.Format("Синхронизация справочника вагонов classa:{0} - отключена. (Settings:activeSynchronizeWagons).", this.className), this.eventID);
                return 0;
            }
            KIS_Transfer kist = new KIS_Transfer();
            LogRW.LogInformation(String.Format("Сервис синхронизации справочников между системами КИС и RailWay :{0} - запущен", this.className), this.eventID);
            try
            {
                if (activeWagons)
                {
                    int res_swagons = kist.SynchronizeWagons(this.dayControllingWagons);
                }
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[SynchronizeKIS.Synchronize]: Общая ошибка синхронизации справочников между системами КИС и RailWay (источник: {0}, № {1}, описание:  {2})", e.Source, e.HResult, e.Message), this.eventID);
            }
            return 0; // TODO: исправить возврат
        }
    }
}
