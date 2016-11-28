using EFRailWay.Concrete.KIS;
using EFRailWay.Entities.KIS;
using EFRailWay.KIS;
using EFRailWay.MT;
using EFWagons.Entities;
using EFWagons.KIS;
using Logs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferWagons.RailCars;
using TransferWagons.RailWay;

namespace TransferWagons.Transfers
{
    public class KIS_Transfer
    {
        KIS_RC_Transfer transfer_rc = new KIS_RC_Transfer();
        KIS_RW_Transfer transfer_rw = new KIS_RW_Transfer();
        
        private eventID eventID = eventID.TransferWagons_KIS_Transfer;
        ArrivalSostav oas = new ArrivalSostav();
        PromContent pc = new PromContent();
        MTContent mtcont = new MTContent();
        private int dayControllingAddNatur;
        public int DayControllingAddNatur { get { return this.dayControllingAddNatur; } set { this.dayControllingAddNatur = value; } }

        public KIS_Transfer()
        {

        }

        #region Таблица переноса составов из КИС [Oracle_ArrivalSostav]
        /// <summary>
        /// Сохранить состав из КИС
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        protected int SaveArrivalSostav(PromSostav ps, statusSting status)
        {
            try
            {
                DateTime DT = DateTime.Parse(ps.D_DD.ToString() + "-" + ps.D_MM.ToString() + "-" + ps.D_YY.ToString() + " " + ps.T_HH.ToString() + ":" + ps.T_MI.ToString() + ":00", CultureInfo.CreateSpecificCulture("ru-RU"));
                return oas.SaveOracle_ArrivalSostav(new Oracle_ArrivalSostav()
                {
                    IDOrcSostav = 0,
                    DateTime = DT,
                    Day = (int)ps.D_DD,
                    Month = (int)ps.D_MM,
                    Year = (int)ps.D_YY,
                    Hour = (int)ps.T_HH,
                    Minute = (int)ps.T_MI,
                    NaturNum = ps.N_NATUR,
                    IDOrcStation = (int)ps.K_ST,
                    WayNum = ps.N_PUT,
                    Napr = ps.NAPR,
                    CountWagons = null,
                    CountNatHIist = null,
                    CountSetWagons = null,
                    CountSetNatHIist = null,
                    Close = null,
                    Status = (int)status,
                    ListWagons = null,
                    ListNoSetWagons = null,
                    ListNoUpdateWagons = null,
                });
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[KISTransfer.SaveSostav]: Ошибка выполнения переноса информации о составе из базы данных КИС в таблицу учета прибытия составов на АМКР (источник: {0}, № {1}, описание:  {2})", e.Source, e.HResult, e.Message), this.eventID);
                return -1;
            }
        }
        /// <summary>
        /// Найти и удалить из списка Oracle_ArrivalSostav елемент natur
        /// </summary>
        /// <param name="list"></param>
        /// <param name="natur"></param>
        /// <returns></returns>
        protected bool DelExistArrivalSostav(ref List<Oracle_ArrivalSostav> list, int natur)
        {
            bool Result = false;
            int index = list.Count() - 1;
            while (index >= 0)
            {
                if (list[index].NaturNum == natur)
                {
                    list.RemoveAt(index);
                    Result = true;
                }
                index--;
            }
            return Result;
        }
        /// <summary>
        /// Проверяет списки PromSostav и Oracle_ArrivalSostav на повторяющие натурные листы, оставляет в списке PromSostav - добавленные составы, Oracle_ArrivalSostav - удаленные из КИС составы
        /// </summary>
        /// <param name="list_ps"></param>
        /// <param name="list_as"></param>
        protected void DelExistArrivalSostav(ref List<PromSostav> list_ps, ref List<Oracle_ArrivalSostav> list_as)
        {
            int index = list_ps.Count() - 1;
            while (index >= 0)
            {
                if (DelExistArrivalSostav(ref list_as, list_ps[index].N_NATUR))
                {
                    list_ps.RemoveAt(index);
                }
                index--;
            }
        }
        /// <summary>
        /// Удалить ранее перенесеные составы
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected int DeleteArrivalSostav(List<Oracle_ArrivalSostav> list)
        {
            if (list == null | list.Count == 0) return 0;
            int delete = 0;
            int errors = 0;
            foreach (Oracle_ArrivalSostav or_as in list)
            {
                // Удалим вагоны из системы RailCars
                transfer_rc.DeleteVagonsToNaturList(or_as.NaturNum, or_as.DateTime);
                // TODO: Сделать код удаления вагонов из RailWay

                or_as.Close = DateTime.Now;
                or_as.Status = (int)statusSting.Delete;
                int res = oas.SaveOracle_ArrivalSostav(or_as);
                if (res > 0) delete++;
                if (res < 1)
                {
                    LogRW.LogError(String.Format("[KISTransfer.DeleteArrivalSostav] :Ошибка удаления данных из таблицы учета прибытия составов на АМКР, IDOrcSostav: {0}", or_as.IDOrcSostav), this.eventID);
                    errors++;
                }
            }
            LogRW.LogWarning(String.Format("Определено удаленных ранее прибывших составов в системе КИС {0}, удалено из таблицы учета прибытия составов на АМКР {1}, ошибок удаления {2}", list.Count(), delete, errors), this.eventID);
            return delete;
        }
        /// <summary>
        /// Добавить новые составы появившиеся после переноса
        /// </summary>
        /// <param name="list"></param>
        protected int InsertArrivalSostav(List<PromSostav> list)
        {
            if (list == null | list.Count == 0) return 0;
            int insers = 0;
            int errors = 0;
            foreach (PromSostav ps in list)
            {
                int res = SaveArrivalSostav(ps, statusSting.Insert);
                if (res > 0) insers++;
                if (res < 1)
                {
                    LogRW.LogError(String.Format("[KISTransfer.InsertArrivalSostav] :Ошибка добавления данных в таблицу учета прибытия составов на АМКР, натурный лист: {0}, дата:{1}-{2}-{3} {4}:{5}", ps.N_NATUR, ps.D_DD, ps.D_MM, ps.D_YY, ps.T_HH, ps.T_MI), this.eventID);
                    errors++;
                }
            }
            LogRW.LogWarning(String.Format("Определено добавленных прибывших составов в системе КИС {0}, добавлено таблицу учета прибытия составов на АМКР {1}, ошибок добавления {2}", list.Count(), insers, errors), this.eventID);
            return insers;
        }
        #endregion

        #region Перенос и обнавление вагонов из системы КИС в RailWay
        /// <summary>
        /// Перенос информации о составах защедших на АМКР по системе КИС (с проверкой на изменение натуральных листов)
        /// </summary>
        /// <returns></returns>
        public int CopyArrivalSostavToRailway()
        {
            int errors = 0;
            int normals = 0;
            // список новых составов в системе КИС
            IQueryable<PromSostav> list_newsostav = null;
            // список уже перенесенных в RailWay составов в системе КИС (с учетом периода контроля dayControllingAddNatur)
            IQueryable<PromSostav> list_oldsostav = null;
            // список уже перенесенных в RailWay составов (с учетом периода контроля dayControllingAddNatur)
            IQueryable<Oracle_ArrivalSostav> list_arrivalsostav = null;
            try
            {
                // Считаем дату последненго состава
                DateTime? lastDT = oas.GetLastDateTime();
                if (lastDT != null)
                {
                    // Данные есть получим новые
                    list_newsostav = pc.GetInputPromSostav(((DateTime)lastDT).AddSeconds(1), DateTime.Now, false);
                    list_oldsostav = pc.GetInputPromSostav(((DateTime)lastDT).AddDays(this.dayControllingAddNatur * -1), ((DateTime)lastDT).AddSeconds(1), false);
                    list_arrivalsostav = oas.Get_ArrivalSostav(((DateTime)lastDT).AddDays(this.dayControllingAddNatur * -1), ((DateTime)lastDT).AddSeconds(1));
                }
                else
                {
                    // Таблица пуста получим первый раз
                    list_newsostav = pc.GetInputPromSostav(DateTime.Now.AddDays(this.dayControllingAddNatur * -1), DateTime.Now, false);
                }
                // Переносим информацию по новым составам
                if (list_newsostav != null & list_newsostav.Count() > 0)
                {
                    foreach (PromSostav ps in list_newsostav)
                    {

                        int res = SaveArrivalSostav(ps, statusSting.Normal);
                        if (res > 0) normals++;
                        if (res < 1) { errors++; }
                    }
                    LogRW.LogWarning(String.Format("Определено для переноса новых составов из базы данных КИС в таблицу учета прибытия составов на АМКР: {0}, перенесено {1}, ошибок переноса {2}", list_newsostav.Count(), normals, errors), this.eventID);
                }
                // Обновим информацию по составам которые были перенесены
                if (list_oldsostav != null & list_oldsostav.Count() > 0 &
                    list_arrivalsostav != null & list_arrivalsostav.Count() > 0)
                {
                    List<PromSostav> list_ps = new List<PromSostav>();
                    list_ps = list_oldsostav.ToList();
                    List<Oracle_ArrivalSostav> list_as = new List<Oracle_ArrivalSostav>();
                    list_as = list_arrivalsostav.ToList().Where(a => a.Status != (int)statusSting.Delete).ToList();
                    DelExistArrivalSostav(ref list_ps, ref list_as);
                    int ins = InsertArrivalSostav(list_ps);
                    int del = DeleteArrivalSostav(list_as);
                }
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[KISTransfer.PutCarsToStations]: Ошибка, источник: {0}, № {1}, описание:  {2}", e.Source, e.HResult, e.Message), this.eventID);
            }

            return normals;
        }
        /// <summary>
        /// Поставить все составы прибывшие на АМКР по системе КИС (перечень составов берется из таблицы учета прибытия составов на АМКР системы RailWay)
        /// </summary>
        /// <returns></returns>
        public int PutCarsToStations()
        {
            IQueryable<Oracle_ArrivalSostav> list_noClose = oas.Get_ArrivalSostavNoClose();
            if (list_noClose == null | list_noClose.Count() == 0) return 0;
            foreach (Oracle_ArrivalSostav or_as in list_noClose)
            {
                Oracle_ArrivalSostav kis_sostav = new Oracle_ArrivalSostav();
                kis_sostav = or_as;
                // Поставим состав на станции АМКР системы RailCars
                int res_put = transfer_rc.PutCarsToStation(ref kis_sostav);
                //T-ODO: ВКЛЮЧИТЬ КОД: Обновление составов на станции АМКР системы RailCars
                int res_upd = transfer_rc.UpdateCarsToStation(ref kis_sostav); 
                //TODO: ВЫПОЛНИТЬ КОД: Поставим состав на станции АМКР системы RailWay         
                //.............................

                //Закрыть состав
                if (kis_sostav.CountWagons != null & kis_sostav.CountNatHIist != null & kis_sostav.CountSetWagons != null & kis_sostav.CountSetNatHIist !=null
                    & kis_sostav.CountWagons == kis_sostav.CountNatHIist & kis_sostav.CountWagons == kis_sostav.CountSetWagons & kis_sostav.CountWagons == kis_sostav.CountSetNatHIist)
                {
                    kis_sostav.Close = DateTime.Now;
                    int res_close = oas.SaveOracle_ArrivalSostav(kis_sostav);

                    int res_del_arr = transfer_rc.DeleteInArrival(kis_sostav.NaturNum, kis_sostav.DateTime);
                    //TODO: ВЫПОЛНИТЬ КОД: Убрать с прибытия с УЗ на станции АМКР в системе RailWay
                }
            }
            return 0; // TODO: исправить возврат
        }
        #endregion

        #region Синхронизация справочников
        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public int SynchronizeWagons(int day) 
        {
            //TODO: ВЫПОЛНИТЬ синхронизацию справочника системы RailWay
            return transfer_rc.SynchronizeWagons(day);

        }
        #endregion

        #region Чистка старых запесей прибытие и зачисление вагонов на станцию
        /// <summary>
        /// Зачистить список вагонов в закладках прибытие и ожидают зачисления
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public int ClearArrivingWagons(int day) 
        {
            //TODO: ВЫПОЛНИТЬ чистку  в системе RailWay
            int res_ca = transfer_rc.ClearArrivingWagons(new int[] { 3,9,10,11,14,18,19,21,22,25,26 } ,day);
            int res_cp = transfer_rc.ClearPendingWagons(new int[] { 3,9,10,11,14,18,19,21,22,25,26 }, day);
            LogRW.LogWarning(String.Format("Очищено – закладка прибытие: {0} строк, закладка ожидают зачисления: {1} строк.", res_ca, res_cp), this.eventID);
            return res_ca + res_cp;
        }
        #endregion

    }
}
