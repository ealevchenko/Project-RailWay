using EFRailWay.Concrete.KIS;
using EFRailWay.Entities.KIS;
using EFRailWay.KIS;
using EFRailWay.MT;
using EFRailWay.Statics;
using EFWagons.Entities;
using EFWagons.Statics;
using Logs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferWagons.Railcars;
using TransferWagons.RailWay;

namespace TransferWagons.Transfers
{
    public class KIS_Transfer
    {
        KIS_RC_Transfer transfer_rc = new KIS_RC_Transfer();
        KIS_RW_Transfer transfer_rw = new KIS_RW_Transfer();

        private eventID eventID = eventID.TransferWagons_Transfers_KIS_Transfer;
        ArrivalSostav oas = new ArrivalSostav();
        InputSostav ois = new InputSostav();
        RulesCopy orc = new RulesCopy();

        VagonsContent vc = new VagonsContent();
        PromContent pc = new PromContent();
        MTContent mtcont = new MTContent();
        //private int dayControllingAddNatur;
        //public int DayControllingAddNatur { get { return this.dayControllingAddNatur; } set { this.dayControllingAddNatur = value; } }

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
                LogRW.LogError(String.Format("[KISTransfer.SaveArrivalSostav]: Ошибка выполнения переноса информации о составе из базы данных КИС в таблицу учета прибытия составов на АМКР (источник: {0}, № {1}, описание:  {2})", e.Source, e.HResult, e.Message), this.eventID);
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

        #region Таблица переноса составов из КИС [Oracle_InputSostav]
        /// <summary>
        /// Получить список составов за указаный перилод согласно правила копирования
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IQueryable<NumVagStanStpr1InStDoc> GetNumVagStanStpr1InStDocIsRules(DateTime start, DateTime stop) 
        {
            List<OracleRules> list = orc.GetRulesCopyToOracleRulesOfKis(typeOracleRules.Input);
            string wh =null;
            return vc.GetSTPR1InStDocOfAmkr(wh.ConvertWhere(list, "a.k_stan", "st_in_st ", "OR")).Where(v => v.DATE_IN_ST > start & v.DATE_IN_ST <= stop);

        }
        /// <summary>
        ///  Получить список составов за указаный перилод согласно правила копирования и сортировкой
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public IQueryable<NumVagStanStpr1InStDoc> GetNumVagStanStpr1InStDocIsRules(DateTime start, DateTime stop, bool sort) 
        {
            if (sort)
            {
                return GetNumVagStanStpr1InStDocIsRules(start, stop).OrderByDescending(v => v.DATE_IN_ST);
            }
            else return GetNumVagStanStpr1InStDocIsRules(start, stop).OrderBy(v => v.DATE_IN_ST);

        }
        /// <summary>
        /// Создать и сохранить строку Oracle_InputSostav
        /// </summary>
        /// <param name="inp_sostav"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        protected int SaveInputSostav(NumVagStanStpr1InStDoc inp_sostav, statusSting status)
        {
            try
            {
                return ois.SaveOracle_InputSostav(new Oracle_InputSostav()
                {
                    ID = 0,
                    DateTime = inp_sostav.DATE_IN_ST,
                    DocNum = inp_sostav.ID_DOC,
                    IDOrcStationFrom = inp_sostav.ST_IN_ST != null ? (int)inp_sostav.ST_IN_ST : 0,
                    WayNumFrom = inp_sostav.N_PUT_IN_ST != null ? (int)inp_sostav.N_PUT_IN_ST : 0,
                    NaprFrom = inp_sostav.NAPR_IN_ST != null ? (int)inp_sostav.NAPR_IN_ST : 0,
                    IDOrcStationOn = inp_sostav.K_STAN != null ? (int)inp_sostav.K_STAN : 0,
                    CountWagons = null,
                    CountSetWagons = null,
                    CountUpdareWagons = null,
                    Close = null,
                    Status = (int)status
                });
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[KISTransfer.SaveInputSostav]: Ошибка выполнения переноса информации о составе (копирование по прибытию из внутрених станций) из базы данных КИС в таблицу учета прибытия составов на АМКР (источник: {0}, № {1}, описание:  {2})", e.Source, e.HResult, e.Message), this.eventID);
                return -1;
            }
        }
        /// <summary>
        /// Проверить изменения в количестве вагонов в составе
        /// </summary>
        /// <param name="o_is"></param>
        /// <param name="doc"></param>
        protected void CheckChangeExistInputSostav(Oracle_InputSostav o_is, int doc)
        {
            int count_vag = vc.GetCountSTPR1InStVag(doc);
            // Количество вагонов изменено
            if (o_is.CountWagons > 0 & count_vag > 0 & o_is.CountWagons != count_vag)
            {
                // Изменим количество вагонов и отправим на переустановку вагонов
                o_is.CountWagons = count_vag;
                o_is.Status = (int)statusSting.Update;
                o_is.Close = null;
                ois.SaveOracle_InputSostav(o_is);
            }
        }
        /// <summary>
        /// Найти и удалить из списка Oracle_InputSostav елемент doc
        /// </summary>
        /// <param name="list"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        protected bool DelExistInputSostav(ref List<Oracle_InputSostav> list, int doc)
        {
            bool Result = false;
            int index = list.Count() - 1;
            while (index >= 0)
            {
                if (list[index].DocNum == doc)
                {
                    CheckChangeExistInputSostav(list[index], doc); // количество вагонов
                    list.RemoveAt(index);
                    Result = true;
                }
                index--;
            }
            return Result;
        }
        /// <summary>
        /// Проверяет списки NumVagStanStpr1InStDoc и Oracle_InputSostav на повторяющие документы, оставляет в списке NumVagStanStpr1InStDoc - добавленные составы, Oracle_InputSostav - удаленные из КИС составы
        /// </summary>
        /// <param name="list_is"></param>
        /// <param name="list_ois"></param>
        protected void DelExistInputSostav(ref List<NumVagStanStpr1InStDoc> list_is, ref List<Oracle_InputSostav> list_ois)
        {
            int index = list_is.Count() - 1;
            while (index >= 0)
            {
                if (DelExistInputSostav(ref list_ois, list_is[index].ID_DOC))
                {
                    list_is.RemoveAt(index);
                }
                index--;
            }
        }
        /// <summary>
        /// удалить строку состава отсутсвующего после переноса
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        protected int DeleteInputSostav(List<Oracle_InputSostav> list)
        {
            if (list == null | list.Count == 0) return 0;
            int delete = 0;
            int errors = 0;
            foreach (Oracle_InputSostav or_is in list)
            {
                // Удалим вагоны из системы RailCars
                transfer_rc.DeleteVagonsToDocInput(or_is.DocNum);
                // TODO: Сделать код удаления вагонов из RailWay

                or_is.Close = DateTime.Now;
                or_is.Status = (int)statusSting.Delete;
                int res = ois.SaveOracle_InputSostav(or_is);
                if (res > 0) delete++;
                if (res < 1)
                {
                    LogRW.LogError(String.Format("[KISTransfer.DeleteInputSostav] :Ошибка удаления данных из таблицы учета прибытия составов (копирование по прибытию из внутрених станций), ID: {0}", or_is.ID), this.eventID);
                    errors++;
                }
            }
            LogRW.LogWarning(String.Format("Определено для удаленния прибывших составов (копирование по прибытию из внутрених станций) {0}, удалено {1}, ошибок удаления {2}", list.Count(), delete, errors), this.eventID);
            return delete;
        }
        /// <summary>
        /// Добавить новые составы появившиеся после переноса
        /// </summary>
        /// <param name="list"></param>
        protected int InsertInputSostav(List<NumVagStanStpr1InStDoc> list)
        {
            if (list == null | list.Count == 0) return 0;
            int insers = 0;
            int errors = 0;
            foreach (NumVagStanStpr1InStDoc inp_s in list)
            {
                int res = SaveInputSostav(inp_s, statusSting.Insert);
                if (res > 0) insers++;
                if (res < 1)
                {
                    LogRW.LogError(String.Format("[KISTransfer.InsertInputSostav] :Ошибка добавления данных в таблицу учета прибытия составов (копирование по прибытию из внутрених станций), номер документа: {0}", inp_s.ID_DOC), this.eventID);
                    errors++;
                }
            }
            LogRW.LogWarning(String.Format("Определено для добавленных новых составов (копирование по прибытию из внутрених станций) {0}, добавлено {1}, ошибок добавления {2}", list.Count(), insers, errors), this.eventID);
            return insers;
        }
        #endregion

        #region Перенос и обнавление вагонов из системы КИС в RailWay
        /// <summary>
        /// Перенос информации о составах защедших на АМКР по системе КИС (с проверкой на изменение натуральных листов)
        /// </summary>
        /// <returns></returns>
        public int CopyArrivalSostavToRailway(int day_control_add_natur)
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
                    list_oldsostav = pc.GetInputPromSostav(((DateTime)lastDT).AddDays(day_control_add_natur * -1), ((DateTime)lastDT).AddSeconds(1), false);
                    list_arrivalsostav = oas.Get_ArrivalSostav(((DateTime)lastDT).AddDays(day_control_add_natur * -1), ((DateTime)lastDT).AddSeconds(1));
                }
                else
                {
                    // Таблица пуста получим первый раз
                    list_newsostav = pc.GetInputPromSostav(DateTime.Now.AddDays(day_control_add_natur * -1), DateTime.Now, false);
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
                LogRW.LogError(String.Format("[KISTransfer.CopyArrivalSostavToRailway]: Ошибка, источник: {0}, № {1}, описание:  {2}", e.Source, e.HResult, e.Message), this.eventID);
            }

            return normals;
        }
        /// <summary>
        /// Поставить все составы прибывшие на АМКР по системе КИС (перечень составов берется из таблицы учета прибытия составов на АМКР системы RailWay)
        /// </summary>
        /// <returns></returns>
        public int PutCarsToStations(int mode)
        {
            IQueryable<Oracle_ArrivalSostav> list_noClose = oas.Get_ArrivalSostavNoClose();
            if (list_noClose == null | list_noClose.Count() == 0) return 0;
            foreach (Oracle_ArrivalSostav or_as in list_noClose)
            {
                Oracle_ArrivalSostav kis_sostav = new Oracle_ArrivalSostav();
                kis_sostav = or_as;
                // Поставим состав на станции АМКР системы RailCars
                int res_put = transfer_rc.PutCarsToStation(ref kis_sostav, mode);
                //TODO: ВКЛЮЧИТЬ КОД: Обновление составов на станции АМКР системы RailCars
                int res_upd = transfer_rc.UpdateCarsToStation(ref kis_sostav, mode); 
                //TODO: ВЫПОЛНИТЬ КОД: Поставим состав на станции АМКР системы RailWay         
                //.............................

                //Закрыть состав
                if (kis_sostav.CountWagons != null & kis_sostav.CountNatHIist != null & kis_sostav.CountSetWagons != null & kis_sostav.CountSetNatHIist !=null
                    & kis_sostav.CountWagons == kis_sostav.CountNatHIist & kis_sostav.CountWagons == kis_sostav.CountSetWagons & kis_sostav.CountWagons == kis_sostav.CountSetNatHIist)
                {
                    kis_sostav.Close = DateTime.Now;
                    int res_close = oas.SaveOracle_ArrivalSostav(kis_sostav);

                    if (mode == 0) { 
                        int res_del_arr = transfer_rc.DeleteInArrival(kis_sostav.NaturNum, kis_sostav.DateTime); 
                        //TODO: ВЫПОЛНИТЬ КОД: Убрать с прибытия с УЗ на станции АМКР в системе RailWay
                    }

                }
            }
            return 0; // TODO: исправить возврат
        }
        /// <summary>
        /// Перенос информации о составах по внутреним станциям по прибытию
        /// </summary>
        /// <returns></returns>
        public int CopyInputSostavToRailway(int day_control_ins)
        {
            int errors = 0;
            int normals = 0;
            // список новых составов в системе КИС
            IQueryable<NumVagStanStpr1InStDoc> list_newsostav = null;
            // список уже перенесенных в RailWay составов в системе КИС (с учетом периода контроля dayControllingAddNatur)
            IQueryable<NumVagStanStpr1InStDoc> list_oldsostav = null;
            // список уже перенесенных в RailWay составов (с учетом периода контроля dayControllingAddNatur)
            IQueryable<Oracle_InputSostav> list_inputsostav = null;
            try
            {
                // Считаем дату последненго состава
                DateTime? lastDT = ois.GetLastDateTime();
                if (lastDT != null)
                {
                    // Данные есть получим новые
                    list_newsostav = GetNumVagStanStpr1InStDocIsRules(((DateTime)lastDT).AddSeconds(1), DateTime.Now, false);
                    list_oldsostav = GetNumVagStanStpr1InStDocIsRules(((DateTime)lastDT).AddDays(day_control_ins * -1), ((DateTime)lastDT).AddSeconds(1), false);
                    list_inputsostav = ois.GetInputSostav(((DateTime)lastDT).AddDays(day_control_ins * -1), ((DateTime)lastDT).AddSeconds(1));
                }
                else
                {
                    // Таблица пуста получим первый раз
                    list_newsostav = GetNumVagStanStpr1InStDocIsRules(DateTime.Now.AddDays(day_control_ins * -1), DateTime.Now, false);
                }
                // Переносим информацию по новым составам
                if (list_newsostav != null & list_newsostav.Count() > 0)
                {
                    foreach (NumVagStanStpr1InStDoc inps in list_newsostav)
                    {

                        int res = SaveInputSostav(inps, statusSting.Normal);
                        if (res > 0) normals++;
                        if (res < 1) { errors++; }
                    }
                    LogRW.LogWarning(String.Format("Определено для переноса новых составов из базы данных КИС в таблицу учета прибытия составов (копирование по прибытию из внутрених станций): {0}, перенесено {1}, ошибок переноса {2}", list_newsostav.Count(), normals, errors), this.eventID);
                }
                // Обновим информацию по составам которые были перенесены
                if (list_oldsostav != null & list_oldsostav.Count() > 0 &
                    list_inputsostav != null & list_inputsostav.Count() > 0)
                {
                    List<NumVagStanStpr1InStDoc> list_is = new List<NumVagStanStpr1InStDoc>();
                    list_is = list_oldsostav.ToList();
                    List<Oracle_InputSostav> list_ois = new List<Oracle_InputSostav>();
                    list_ois = list_inputsostav.ToList().Where(a => a.Status != (int)statusSting.Delete).ToList();
                    DelExistInputSostav(ref list_is, ref list_ois);
                    int ins = InsertInputSostav(list_is);
                    int del = DeleteInputSostav(list_ois);
                }
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[KISTransfer.CopyInputSostavToRailway]: Ошибка, источник: {0}, № {1}, описание:  {2}", e.Source, e.HResult, e.Message), this.eventID);
            }

            return normals;
        }
        /// <summary>
        /// Поставить все составы из прибытия системы КИС (перечень составов берется из таблицы учета прибытия из внутренихстанций системы КИС)
        /// </summary>
        /// <returns></returns>
        public int PutInputSostavToStation() 
        {
            IQueryable<Oracle_InputSostav> list_noClose = ois.GetInputSostavNoClose();
            if (list_noClose == null | list_noClose.Count() == 0) return 0;
            foreach (Oracle_InputSostav or_is in list_noClose.ToList()) 
                //TODO: .ToList() добавлен чтобы небыло ошибок
                //Дело в том, что функция Where(Function()...) возвращает результат типа Linq.IQueryable(), который создает транзакцию, 
                // которая в свою очередь остается открытой на все время исполнения запроса — на весь цикл перебора. А вызов метода db.SaveChanges() 
                // спытается открыть новую транзакцию, при открытой предыдущей и это вызывает исключение. Чтобы «завершить» открытую транзакцию необходимо привести результат 
                // к «конечному» типу, например Array или List(Of ...). Для этого необходимо изменить код: 
            {
                Oracle_InputSostav kis_inp_sostav = new Oracle_InputSostav();
                kis_inp_sostav = or_is;
                //Закрыть состав
                if (kis_inp_sostav.CountWagons != null & kis_inp_sostav.CountSetWagons != null & kis_inp_sostav.CountWagons == kis_inp_sostav.CountSetWagons)
                {
                    kis_inp_sostav.Close = DateTime.Now;
                    int res_close = ois.SaveOracle_InputSostav(kis_inp_sostav);
                }

                // Поставим состав на станции АМКР системы RailCars
                int res_put = transfer_rc.PutInputSostavToStation(ref kis_inp_sostav);
                //TODO: ВЫПОЛНИТЬ КОД: Поставим состав на станции АМКР системы RailWay 
      
                //.............................


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
