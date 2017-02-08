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
    public class CopyingInlandKIS
    {
        private eventID eventID = eventID.KIS_Service_CopyingInlandKIS;
        
        private string className = "CopyingInlandKIS";
        private string classDescription = "Копирование по внутреним станциям (по прибытию и отправке)";
        private bool error_settings = false;

        private bool activeCopyOutput = false;  // признак активности копированиия данных
        private bool activeCopyInput = false;  // признак активности копированиия данных
        private bool activeDelete = false;  // признак активности удаления данных из прибытия и отправки


        private int dayControllingCopyOutput_double;   // кол. дней период проверки повторяющихся строк.
        private int dayControllingCopyOutput_ins;   // кол. дней период проверки добавленных строк.
        private int dayControllingCopyOutput_close;   // кол. дней  после которого строки закроются автоматически.

        private int dayControllingCopyInput_del;   // кол. дней период проверки удаленных строк.
        private int dayControllingCopyInput_ins;   // кол. дней период проверки добавленных строк.

        private int dayControllingDelete;   // кол. дней период присутствия информации в прибытии и отправке.

        public CopyingInlandKIS()
        {
            try
            {
                Settings set = new Settings();
                set.Get_Project(this.className, this.classDescription, true); // Проверим наличие проекта
                this.activeCopyOutput = (bool)set.GetBoolSettingConfigurationManager("activeCopyOutput", this.className, true);
                this.activeCopyInput = (bool)set.GetBoolSettingConfigurationManager("activeCopyInput", this.className, true);
                this.activeDelete = (bool)set.GetBoolSettingConfigurationManager("activeDelete", this.className, true);


                this.dayControllingCopyOutput_double = (int)set.GetIntSettingConfigurationManager("dayControllingCopyOutput_double", this.className, true);
                this.dayControllingCopyOutput_ins = (int)set.GetIntSettingConfigurationManager("dayControllingCopyOutput_ins", this.className, true);
                this.dayControllingCopyOutput_close = (int)set.GetIntSettingConfigurationManager("dayControllingCopyOutput_close", this.className, true);

                this.dayControllingCopyInput_del = (int)set.GetIntSettingConfigurationManager("dayControllingCopyInput_del", this.className, true);
                this.dayControllingCopyInput_ins = (int)set.GetIntSettingConfigurationManager("dayControllingCopyInput_ins", this.className, true);

                this.dayControllingDelete = (int)set.GetIntSettingConfigurationManager("dayControllingDelete", this.className, true);
            }
            catch (Exception e)
            {
                error_settings = true;
                LogRW.LogError(String.Format("[CopyingInlandKIS]: Ошибка выполнения инициализации classa {0} (источник: {1}, № {2}, описание:  {3})", this.className, e.Source, e.HResult, e.Message), this.eventID);
            }
        }

        public int Copy()
        {
            if (error_settings)
            {
                LogRW.LogWarning("Выполнение метода CopyingInlandKIS.Copy() - отменено, ошибка нет данных Settings.", this.eventID);
                return 0;
            }   
            if (!this.activeCopyInput)
            {
                LogRW.LogWarning(String.Format("Копирование по внутреним станциям, вагонов по прибытию, classa:{0} - отключено. (Settings:activeCopyInput).", this.className), this.eventID);
            }         
            if (!this.activeCopyOutput)
            {
                LogRW.LogWarning(String.Format("Копирование по внутреним станциям, вагонов по отправке, classa:{0} - отключено. (Settings:activeCopyOutput).", this.className), this.eventID);
            }


            KIS_Transfer kist = new KIS_Transfer();
            LogRW.LogInformation(String.Format("Сервис копирования по внутреним станциям системамы КИС в систему RailWay :{0} - запущен", this.className), this.eventID);
            try
            {
                //TODO: перенести необходимые переменные
                if (activeCopyInput) 
                {
                    // Перенесем или обновим информацию о составах защедших на АМКР по системе КИС
                    int result_ci = kist.CopyInputSostavToRailway(this.dayControllingCopyInput_ins);
                    int result_pi = kist.PutInputSostavToStation();
                }
                if (activeCopyOutput) 
                {
                    int result_co = kist.CopyOutputSostavToRailway(this.dayControllingCopyOutput_ins);
                }
                if (activeDelete) 
                {
                    int res_del = kist.ClearArrivingWagons(dayControllingDelete);
                }
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[CopyingInlandKIS.Copy]: Общая ошибка копирования по внутреним станциям системамы КИС в систему RailWay (источник: {0}, № {1}, описание:  {2})", e.Source, e.HResult, e.Message), this.eventID);
            }
            return 0; // TODO: исправить возврат
        }
    }
}
