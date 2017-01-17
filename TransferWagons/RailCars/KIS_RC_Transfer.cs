using EFRailWay.Entities;
using EFRailWay.Entities.KIS;
using EFRailWay.Entities.Railcars;
using EFRailWay.KIS;
using EFRailWay.MT;
using EFRailWay.Railcars;
//using EFRailWay.SAP;
using EFWagons.Entities;
using EFWagons.KIS;
//using Errors;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferWagons.Transfers;

namespace TransferWagons.RailCars
{
    
    
    public class KIS_RC_Transfer : Transfer
    {
        References ref_kis = new References();
        VagonsContent vc = new VagonsContent();
        PromContent pc = new PromContent();
        SAP_Transfer sap_tr = new SAP_Transfer();
        //SAPIncomingSupply sap_is = new SAPIncomingSupply();
        RC_VagonsOperations rc_vo = new RC_VagonsOperations();
        RC_Stations rc_st = new RC_Stations();
        ArrivalSostav oas = new ArrivalSostav();
        MTContent mtcont = new MTContent();
        private eventID eventID = eventID.TransferWagons_RailCars;

        public KIS_RC_Transfer()
            : base()
        {

        }

        /// <summary>
        /// Удалить вагоны защедшие с указаному натурному листу и дате
        /// </summary>
        /// <param name="natur_list"></param>
        /// <param name="dt_amkr"></param>
        /// <returns></returns>
        public int DeleteVagonsToNaturList(int natur_list, DateTime dt_amkr)
        {
            int res = rc_vo.DeleteVagonsToNaturList(natur_list, dt_amkr); // Удалить предыдущий состав 
            if (res < 0)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.DeleteVagonsToNaturList] :Ошибка удаления вагонов принадлежащих натурному листу: {0} дата: {1}", natur_list,dt_amkr), eventID);
                return (int)errorTransfer.global;
            }
            return res;
        }

        public int DeleteVagonsToDocInput(int doc)
        {
            int res = rc_vo.DeleteVagonsToDocInput(doc); // Удалить состав по номеру документа по прибытию
            if (res < 0)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.DeleteVagonsToDocInput] :Ошибка удаления вагонов принадлежащих документу прибытия на станцию: {0}", doc), eventID);
                return (int)errorTransfer.global;
            }
            return res;
        }

        #region Общие методы

        #endregion

        #region Поставить вагоны в прибытие из станций Кривого Рога
        /// <summary>
        /// Поставить вагоны в прибытие из станций УЗ
        /// </summary>
        /// <param name="sostav"></param>
        /// <returns></returns>
        public int PutInArrival(trSostav sostav)
        {
            if (sostav == null) return 0;
            ResultTransfers result = new ResultTransfers(sostav.Wagons != null ? sostav.Wagons.Count() : 0, 0, null, null, 0, 0);
            if (sostav.ParentID != null)
            {
                try
                {
                    //TODO: !! ВОЗМОЖНО (ПРИБЫТИЕ С УЗ) - доработать удаление составов по которым происходит тсп (удалять только те вагоны которых нет в новом тсп и добавлять новые - вдруг поезд приняли уже на станцию)
                    int res = rc_vo.DeleteVagonsToInsertMT((int)sostav.ParentID); // Удалить предыдущий состав (По этому составу было новое ТСП) 
                    if (res < 0) {
                        LogRW.LogError(String.Format("[KIS_RC_Transfer.PutInArrival] :Ошибка удаления предыдущего состав ID_MT: {0}", sostav.ParentID), eventID);
                        return (int)errorTransfer.global;
                    }
                }
                catch (Exception e)
                {
                    LogRW.LogError(String.Format("[KIS_RC_Transfer.PutInArrival] :Ошибка удаления предыдущего состав ID_MT: {0}. Подробно: (источник: {1}, № {2}, описание: {3}",
                        sostav.ParentID, e.Source, e.HResult, e.Message), eventID);
                    return (int)errorTransfer.global;
                }
            }
            if (sostav.Wagons == null) return 0;
            foreach (trWagon wag in sostav.Wagons)
            {
                try
                {
                    if (result.SetResultInsert(ArrivalWagon(wag, sostav.id, sostav.DateTime, sostav.codecs_in_station))) {
                        LogRW.LogError(String.Format("[KIS_RC_Transfer.PutInArrival] : Ошибка переноса вагона в прибытие, состав ID_MT: {0}, № вагона: {1}, код ошибки: {2}", sostav.id, wag.CarriageNumber, result.result), eventID);
                    }
                }
                catch (Exception e)
                {
                    LogRW.LogError(String.Format("[KIS_RC_Transfer.PutInArrival] : Ошибка переноса вагона в прибытие, состав ID_MT: {0}, № вагона: {1}. Подробно: (источник: {2}, № {3}, описание: {4}",
                        sostav.id, wag.CarriageNumber, e.Source, e.HResult, e.Message), eventID);
                    result.IncError();
                }
            }
            LogRW.LogWarning(String.Format(" Состав ID_MT: {0}, определено для переноса из станций УЗ в прибытие системы RailCars {1} вагонов, перенесено: {2}, перенесено ранее: {3}, ошибок переноса: {4}.",
                sostav.id, result.counts, result.inserts, result.skippeds, result.errors), eventID);
            return result.ResultInsert;
        }
        /// <summary>
        /// Поставить вагон в прибытие из станций УЗ
        /// </summary>
        /// <param name="wag"></param>
        /// <param name="id_sostav"></param>
        /// <param name="dt"></param>
        /// <param name="codecs_station"></param>
        /// <returns></returns>
        public int ArrivalWagon(trWagon wag, int id_sostav, DateTime dt, int? codecs_station)
        {
            //ResultTransfer result = new ResultTransfer();
            if (codecs_station == null)
            {
                return (int)errorTransfer.no_stations;
            }

                ////Определим страну собственника
                //int country = 0;
                //if (wag.CountryCode > 0)
                //{
                //    country = int.Parse(wag.CountryCode.ToString().Substring(1, 2));
                //    int? id_owner_country = ref_kis.DefinitionIDOwnersContries(country);
                //}
                // Определим id вагона и собственника вагона
                int id_vagon = ref_kis.DefinitionSetIDVagon(wag.CarriageNumber, dt, wag.TrainNumber, id_sostav, null, wag.Conditions == 17 ? false : true);// если вагон имеет состояние ожидает прибытие c УЗ
                if (id_vagon < 0)
                {
                    return (int)errorTransfer.no_owner_country;
                }
            //TODO: !! УБРАТЬ (ПОСТАНОВКА ВАГОНА В ПРИБЫТИЕ С УЗ) - убрать определение груза он будет братся из справочника САП вход поставки
            int? id_gruz = ref_kis.DefinitionIDGruzs(wag.IDCargo);
            if (!rc_vo.IsVagonOperationMT(id_sostav, dt, id_vagon)) // вагон не стоит
            {
                // Поставим вагон
                try
                {
                    int res1 = 0;
                    int res2 = 0;
                    if (codecs_station == 467004) // Кривой Рог Гл.
                    {
                        res1 = rc_vo.InsertVagon(id_sostav, id_vagon, wag.CarriageNumber, dt, 33, wag.Position, id_gruz, (decimal)wag.Weight, 13, wag.TrainNumber, wag.Conditions);
                        res2 = rc_vo.InsertVagon(id_sostav, id_vagon, wag.CarriageNumber, dt, 33, wag.Position, id_gruz, (decimal)wag.Weight, 4, wag.TrainNumber, wag.Conditions);
                        if (res1 < 0) return res1;
                        if (res2 < 0) return res2;
                        return res1;
                    }
                    if (codecs_station == 467201) // Кривой Рог черв.
                    {

                        res1 = rc_vo.InsertVagon(id_sostav, id_vagon, wag.CarriageNumber, dt, 35, wag.Position, id_gruz, (decimal)wag.Weight, 20, wag.TrainNumber, wag.Conditions);
                        return res1;
                    }
                    return (int)errorTransfer.no_stations; ;
                }
                catch (Exception e)
                {
                    LogRW.LogError(String.Format("[KIS_RC_Transfer.ArrivalWagon] :Ошибка переноса состав ID_MT: {0}, вагон № {1}, id вагона {2}. Подробно: (источник: {3}, № {4}, описание: {5}",
                        id_sostav, wag.CarriageNumber, id_vagon, e.Source, e.HResult, e.Message), eventID);
                    return (int)errorTransfer.global;
                }
            }
            return 0;
        }
        #endregion

        #region Поставить вагоны или обновить информацию о вагонах на пути внутрених станций АМКР
        /// <summary>
        /// Получить или обновить общий список вагонов, список не поставленных и не обнавленных вагонов
        /// </summary>
        /// <param name="orc_sostav"></param>
        public int SetListWagon(ref Oracle_ArrivalSostav orc_sostav, List<PromVagon> list_pv, List<PromNatHist> list_nh) 
        {
            if (list_pv.Count() == 0 & list_nh.Count() == 0) return 0; // Списков вагонов нет
            try
            {
                
                //Создать и список вагонов заново и поставить их на путь ( or_as.CountWagons, or_as.ListWagons )
                List<int> old_wagons = GetWagonsToListInt(orc_sostav.ListWagons);
                if (list_pv.Count()>0) 
                    {orc_sostav.ListWagons = GetWagonsToString(list_pv); } 
                        else { orc_sostav.ListWagons = GetWagonsToString(list_nh);}
                List<int> new_wagons = GetWagonsToListInt(orc_sostav.ListWagons);

                List<int> wagons_no_set = GetWagonsToListInt(orc_sostav.ListNoSetWagons);
                List<int> wagons_no_upd = GetWagonsToListInt(orc_sostav.ListNoUpdateWagons);
                List<int> wagons_buf = new List<int>();
                List<int> wagons_no_set_buf = new List<int>();
                List<int> wagons_no_upd_buf = new List<int>();
                // Удалить вагоны не найденные в новом списке из списка непоставленных на станцию вагонов 
                if (wagons_no_set != null)
                {
                    wagons_buf = GetWagonsToListInt(orc_sostav.ListWagons);
                    wagons_no_set_buf = GetWagonsToListInt(orc_sostav.ListNoSetWagons);
                    DeleteExistWagon(ref wagons_buf, ref wagons_no_set_buf);
                    foreach (int wag in wagons_no_set_buf)
                    {
                        DeleteExistWagon(ref wagons_no_set, wag);
                    }
                }
                // Удалить вагоны не найденные в новом списке из списка необновленных вагонов 
                if (wagons_no_upd != null)
                {
                    wagons_buf = GetWagonsToListInt(orc_sostav.ListWagons);
                    wagons_no_upd_buf = GetWagonsToListInt(orc_sostav.ListNoUpdateWagons);
                    DeleteExistWagon(ref wagons_buf, ref wagons_no_upd_buf);
                    foreach (int wag in wagons_no_upd_buf)
                    {
                        DeleteExistWagon(ref wagons_no_upd, wag);
                    }
                }
                // сформировать строчные списки не поставленных и не обнавленных вагонов
                orc_sostav.ListNoSetWagons = GetWagonsToString(wagons_no_set);
                orc_sostav.ListNoUpdateWagons = GetWagonsToString(wagons_no_upd);
                // Добавить в списки не поставленных и не обнавленных вагонов новые вагоны из нового списка
                DeleteExistWagon(ref new_wagons, ref old_wagons);
                foreach (int wag in new_wagons)
                {
                    if (wagons_no_set != null)
                    { orc_sostav.ListNoSetWagons += wag.ToString() + ";"; }
                    if (wagons_no_upd != null)
                    { orc_sostav.ListNoUpdateWagons += wag.ToString() + ";"; }
                }

                if (list_pv.Count()>0) 
                    { orc_sostav.CountWagons = list_pv.Count(); } 
                        else { orc_sostav.CountWagons = list_nh.Count(); }
                
                // Сохранить и вернуть результат
                return oas.SaveOracle_ArrivalSostav(orc_sostav);
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.SetListWagon] : Ошибка обновления общего списка вагонов в таблице учета прибытия составов на АМКР, IDOrcSostav: {0}. Подробно: (источник: {1}, № {2}, описание: {3})",
                    orc_sostav.IDOrcSostav, e.Source, e.HResult, e.Message), eventID);
                return (int)errorTransfer.global;
            }
        }
        /// <summary>
        /// Поставить вагон из КИС на станцию АМКР
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="num_vag"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_stations"></param>
        /// <param name="id_ways"></param>
        /// <param name="id_stat_kis"></param>
        /// <returns></returns>
        protected int SetCarKISToStation(int natur, int num_vag, DateTime dt_amkr, int id_stations, int id_ways, int id_stat_kis) 
        {
            try
            {
                int idsostav = natur * -1;
                // Получим информацию для заполнения вагона с учетом отсутствия данных в PromVagon
                PromVagon pv = pc.GetVagon(natur, id_stat_kis, dt_amkr.Day, dt_amkr.Month, dt_amkr.Year, num_vag);
                PromNatHist pnh = pc.GetNatHist(natur, id_stat_kis, dt_amkr.Day, dt_amkr.Month, dt_amkr.Year, num_vag);
                if (pv == null & pnh == null) return (int)errorTransfer.no_wagon_is_list;   // Ошибка нет вагонов в списке
                if (pv == null) 
                {
                    pv = new PromVagon()
                    {
                        N_VAG = pnh.N_VAG,
                        NPP = pnh.NPP,
                        GODN = pnh.GODN,
                        K_ST = pnh.K_ST,
                        N_NATUR = pnh.N_NATUR,
                        D_PR_DD = pnh.D_PR_DD,
                        D_PR_MM = pnh.D_PR_MM,
                        D_PR_YY = pnh.D_PR_YY,
                        T_PR_HH = pnh.T_PR_HH,
                        T_PR_MI = pnh.T_PR_MI,
                        KOD_STRAN = pnh.KOD_STRAN,
                        WES_GR = pnh.WES_GR,
                        K_GR = pnh.K_GR
                    };
                }
                MTList mt_list = mtcont.GetListToNatur(natur, num_vag, dt_amkr, 15);
                if (mt_list != null)
                {
                    idsostav = mt_list.IDMTSostav;
                }
                // Проверим есть строка в справочнеке САП поставки
                if (!sap_tr.IsWagonToSAPSupply(idsostav, num_vag))
                {
                    // Определим код груза
                    int IDCargo = 0;
                    if (pv.K_GR != null)
                    {
                        PromGruzSP pg = pc.GetGruzSP((int)pv.K_GR);
                        IDCargo = pg != null ? pg.TAR_GR != null ? (int)pg.TAR_GR : 0 : 0;
                    }
                    // Создадим строку в САП (id состава)
                    trWagon wag = new trWagon()
                    {
                        Position = pv != null ? pv.NPP : 0,
                        CarriageNumber = num_vag,
                        CountryCode = pv != null ? pv.KOD_STRAN != null ? ((int)pv.KOD_STRAN * 10) + 1 : 0 : 0,
                        Weight = pv != null ? pv.WES_GR != null ? (float)pv.WES_GR : 0 : 0,
                        IDCargo = IDCargo,
                        Cargo = null,
                        IDStation = 0,
                        Station = null,
                        Consignee = 0,
                        Operation = null,
                        CompositionIndex = "-",
                        DateOperation = DateTime.Now,
                        TrainNumber = 0,
                        Conditions = 0,
                    };

                    sap_tr.SetWagonToSAPSupply(wag, idsostav);
                }
                int id_wagon = ref_kis.DefinitionSetIDVagon(num_vag, dt_amkr, -1, null, natur, false); // определить id вагона (если нет создать новый id? локоматив -1)
                //if (!rc_vo.IsVagonOperationKIS(natur, dt_amkr, (int)id_wagon))
                if (!rc_vo.IsVagonOperationKIS(natur, dt_amkr, (int)num_vag))
                {

                    int res = rc_vo.InsertVagon(natur, dt_amkr, id_wagon, num_vag, idsostav, (mt_list != null ? mt_list.DateOperation as DateTime? : null), id_stations, id_ways, id_stat_kis);
                    if (res < 0)
                    {
                        LogRW.LogError(String.Format("[KIS_RC_Transfer.SetCarKISToStation] : Ошибка переноса вагона на путь станции, натурный лист: {0}, дата: {1}, № вагона: {2}, код станции системы RailCars: {3}, код пути системы RailCars: {4}.",
                            natur, dt_amkr.ToString("dd-MM-yyyy HH:mm:ss"), num_vag, id_stations, id_ways), eventID);
                    }
                    return res;
                }
                else return 0; // Вагон уже стоит
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.SetCarKISToStation] : Ошибка переноса вагона на путь станции, натурный лист: {0}, дата: {1}, № вагона: {2}, код станции системы RailCars: {3}, код пути системы RailCars: {4}. Подробно: (источник: {5}, № {6}, описание: {7})",
                    natur, dt_amkr.ToString("dd-MM-yyyy HH:mm:ss"), num_vag, id_stations, id_ways, e.Source, e.HResult, e.Message), eventID);
                return (int)errorTransfer.global;
            }
        }
        /// <summary>
        /// Поставить вагон МТ на станцию АМКР
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="num_vag"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_stations"></param>
        /// <param name="id_ways"></param>
        /// <param name="id_stat_kis"></param>
        /// <returns></returns>
        protected int SetCarMTToStation(int natur, int num_vag, DateTime dt_amkr, int id_stations, int id_ways, int id_stat_kis)
        {
            try
            {
                int idsostav = natur * -1;
                // Получим информацию для заполнения вагона с учетом отсутствия данных в PromVagon
                PromVagon pv = pc.GetVagon(natur, id_stat_kis, dt_amkr.Day, dt_amkr.Month, dt_amkr.Year, num_vag);
                PromNatHist pnh = pc.GetNatHist(natur, id_stat_kis, dt_amkr.Day, dt_amkr.Month, dt_amkr.Year, num_vag);
                if (pv == null & pnh == null) return (int)errorTransfer.no_wagon_is_list;   // Ошибка нет вагонов в списке
                if (pv == null)
                {
                    pv = new PromVagon()
                    {
                        N_VAG = pnh.N_VAG,
                        NPP = pnh.NPP,
                        GODN = pnh.GODN,
                        K_ST = pnh.K_ST,
                        N_NATUR = pnh.N_NATUR,
                        D_PR_DD = pnh.D_PR_DD,
                        D_PR_MM = pnh.D_PR_MM,
                        D_PR_YY = pnh.D_PR_YY,
                        T_PR_HH = pnh.T_PR_HH,
                        T_PR_MI = pnh.T_PR_MI,
                        KOD_STRAN = pnh.KOD_STRAN,
                        WES_GR = pnh.WES_GR,
                        K_GR = pnh.K_GR
                    };
                }
                MTList mt_list = mtcont.GetListToNatur(natur, num_vag, dt_amkr, 15);
                if (mt_list != null)
                {
                    idsostav = mt_list.IDMTSostav;
                }
                // Проверим есть строка в справочнеке САП поставки
                if (!sap_tr.IsWagonToSAPSupply(idsostav, num_vag))
                {
                    // Определим код груза
                    int IDCargo = 0;
                    if (pv.K_GR != null)
                    {
                        PromGruzSP pg = pc.GetGruzSP((int)pv.K_GR);
                        IDCargo = pg != null ? pg.TAR_GR != null ? (int)pg.TAR_GR : 0 : 0;
                    }
                    // Создадим строку в САП (id состава)
                    trWagon wag = new trWagon()
                            {
                                Position = pv != null ? pv.NPP : 0,
                                CarriageNumber = num_vag,
                                CountryCode = pv != null ? pv.KOD_STRAN != null ? ((int)pv.KOD_STRAN * 10) + 1 : 0 : 0,
                                Weight = pv != null ? pv.WES_GR != null ? (float)pv.WES_GR : 0 : 0,
                                IDCargo = IDCargo,
                                Cargo = null,
                                IDStation = 0,
                                Station = null,
                                Consignee = 0,
                                Operation = null,
                                CompositionIndex = "-",
                                DateOperation = DateTime.Now,
                                TrainNumber = 0,
                                Conditions = 0,
                            };

                    sap_tr.SetWagonToSAPSupply(wag, idsostav);
                }
                // Проверим в наличии в прибытия из любой станции УЗ на станцию id_stations
                VAGON_OPERATIONS vagon = rc_vo.GetVagonsOfArrivalUZ(idsostav, num_vag, rc_st.GetUZStationsToID().ToArray(), id_stations);
                if (vagon != null)
                {
                    // Поставить на путь станции
                    // Обновим из КИС необходимости
                    // Удалим остальные
                    rc_vo.TakeVagonOfUZ(idsostav, num_vag, rc_st.GetUZStationsToID().ToArray(), natur, dt_amkr, id_stations, id_ways);
                }
                else 
                {
                    // Проверим в наличии в прибытия из любой станции УЗ на любую станцию АМКР
                    IQueryable<VAGON_OPERATIONS> vagons_uz = rc_vo.GetVagonsOfArrival(idsostav, num_vag, rc_st.GetUZStationsToID().ToArray());
                    if (vagons_uz.Count()>0)
                    {
                        //возмем первый 
                        // Поставить на путь станции
                        // Обновим из КИС необходимости
                        // Удалим остальные
                        rc_vo.TakeVagonOfAllUZ(idsostav, num_vag, rc_st.GetUZStationsToID().ToArray(), natur, dt_amkr, id_stations, id_ways);
                    }
                    else { 
                        // Проверим в наличии записи по данному составу и вагону на станциях АМКР
                        IQueryable<VAGON_OPERATIONS> vagons_amkr = rc_vo.GetVagonsOfArrival(idsostav, num_vag, rc_st.GetAMKRStationsToID().ToArray());
                        if (vagons_amkr != null)
                        {
                            // Обновим из КИС необходимости
                        }
                        else 
                        { 
                            //создадим по данным КИС
                            int id_wagon = ref_kis.DefinitionSetIDVagon(num_vag, dt_amkr, -1, null, natur, false); // определить id вагона (если нет создать новый id? локоматив -1)
                            if (!rc_vo.IsVagonOperationKIS(natur, dt_amkr, (int)num_vag))
                            {

                                int res = rc_vo.InsertVagon(natur, dt_amkr, id_wagon, num_vag, idsostav, (mt_list != null ? mt_list.DateOperation as DateTime? : null), id_stations, id_ways, id_stat_kis);
                                if (res < 0)
                                {
                                    LogRW.LogError(String.Format("[KIS_RC_Transfer.SetCarMTToStation] : Ошибка переноса вагона на путь станции, натурный лист: {0}, дата: {1}, № вагона: {2}, код станции системы RailCars: {3}, код пути системы RailCars: {4}.",
                                        natur, dt_amkr.ToString("dd-MM-yyyy HH:mm:ss"), num_vag, id_stations, id_ways), eventID);
                                }
                                return res;
                            }
                            else return 0; // Вагон уже стоит
                        }

                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.SetCarMTToStation] : Ошибка переноса вагона на путь станции, натурный лист: {0}, дата: {1}, № вагона: {2}, код станции системы RailCars: {3}, код пути системы RailCars: {4}. Подробно: (источник: {5}, № {6}, описание: {7})",
                    natur, dt_amkr.ToString("dd-MM-yyyy HH:mm:ss"), num_vag, id_stations, id_ways, e.Source, e.HResult, e.Message), eventID);
                return (int)errorTransfer.global;
            }
        }

        /// <summary>
        /// Обновить информацию по вагону перенесеному по данным КИС
        /// </summary>
        /// <param name="natur"></param>
        /// <param name="num_vag"></param>
        /// <param name="dt_amkr"></param>
        /// <param name="id_stations"></param>
        /// <param name="id_ways"></param>
        /// <param name="id_stat_kis"></param>
        /// <returns></returns>
        protected int UpdCarToStation(int natur, int num_vag, DateTime dt_amkr, int id_stations, int id_ways, int id_stat_kis) 
        {
            try
            {
                PromNatHist pnh = pc.GetNatHist(natur, id_stat_kis, dt_amkr.Day, dt_amkr.Month, dt_amkr.Year, num_vag);
                if (pnh == null)
                {
                    LogRW.LogError(String.Format("[KIS_RC_Transfer.UpdCarToStation] : Ошибка определения вагона (PromNatHist) натурный лист: {0}, дата: {1}, № вагона: {2}, код станции системы КИС: {3}.",
                        natur, dt_amkr.ToString("dd-MM-yyyy HH:mm:ss"), num_vag, id_stat_kis), eventID);
                    return (int)errorTransfer.no_owner_country;
                }
                //TODO: Сделать получение станции отправителя отдельным полем и получать из NatHist при переносе (скорректровав  ХП GetWagons)
                //TODO: Отключил обновление собственников будет происходить в синхронизации справочника Wagons
                ////Определим страну собственника
                //int? id_owner_country = null;
                //if (pnh.KOD_STRAN > 0)
                //{
                //    id_owner_country = ref_kis.DefinitionIDOwnersContries((int)pnh.KOD_STRAN);
                //}
                //if (id_owner_country == null) 
                //{
                //    LogRW.LogError(String.Format("[KIS_RC_Transfer.UpdCarToStation] : Ошибка определения страны собственника вагона (PromNatHist.KOD_STRAN) натурный лист: {0}, дата: {1}, № вагона: {2}, код страны собственника: {3}.",
                //        natur, dt_amkr.ToString("dd-MM-yyyy HH:mm:ss"), num_vag, pnh.KOD_STRAN), eventID);
                //    return (int)errorTransfer.no_owner_country;
                //}
                //// Определим собственника вагона
                //int? id_owner = null;
                //if (pnh.K_FRONT != null) 
                //{
                //    id_owner = ref_kis.DefinitionIDOwner((int)pnh.K_FRONT, id_owner_country);
                //}
                //if (id_owner == null) 
                //{
                //    LogRW.LogError(String.Format("[KIS_RC_Transfer.UpdCarToStation] : Ошибка определения собственника вагона (PromNatHist.K_FRONT) натурный лист: {0}, дата: {1}, № вагона: {2}, код собственника: {3}.",
                //        natur, dt_amkr.ToString("dd-MM-yyyy HH:mm:ss"), num_vag, pnh.K_FRONT), eventID);
                //    return (int)errorTransfer.no_owner;
                //}
                //TODO: !! ОТКЛЮЧИТЬ (ОБНОВЛЕНИЕ ВАГОНОВ ПО КИСУ) цех получатель и груз, данные будут братся из справочника вх. поставок САП по id sostav и номкру вагона
                // Определим цех получатель груза
                int? id_shop = null;
                if (pnh.K_POL_GR != null) 
                {
                    id_shop = ref_kis.DefinitionIDShop((int)pnh.K_POL_GR);
                }
                if (id_shop == null) 
                {
                    LogRW.LogError(String.Format("[KIS_RC_Transfer.UpdCarToStation] : Ошибка определения получателя груза (PromNatHist.K_POL_GR) натурный лист: {0}, дата: {1}, № вагона: {2}, код цеха получателя груза: {3}.",
                        natur, dt_amkr.ToString("dd-MM-yyyy HH:mm:ss"), num_vag, pnh.K_POL_GR), eventID);                    
                    return (int)errorTransfer.no_shop;
                }
                // определяем название груза
                int? id_gruz = null;
                if (pnh.K_GR != null)
                {
                    id_gruz = ref_kis.DefinitionIDGruzs((int)pnh.K_GR, null);
                }
                if (id_gruz == null)
                {
                    LogRW.LogError(String.Format("[KIS_RC_Transfer.UpdCarToStation] : Ошибка определения типа груза (PromNatHist.K_GR) натурный лист: {0}, дата: {1}, № вагона: {2}, код груза: {3}.",
                        natur, dt_amkr.ToString("dd-MM-yyyy HH:mm:ss"), num_vag, pnh.K_GR), eventID);
                    return (int)errorTransfer.no_gruz;
                }
                //TODO: !! ДОРАБОТАТЬ (ОБНОВЛЕНИЕ ВАГОНОВ ПО КИСУ) обновлять готовность по прибытию и дату зачисления на АМКР
                int res = rc_vo.UpdateVagon(dt_amkr, num_vag, id_ways, (int)id_gruz, (int)id_shop, pnh.WES_GR, pnh.GODN);
                if (res < 0)
                {
                    LogRW.LogError(String.Format("[KIS_RC_Transfer.UpdCarToStation] : Ошибка обновления информации ранее принятого на путь вагона, натурный лист: {0}, дата: {1}, № вагона: {2}, код станции системы RailCars: {3}, код пути системы RailCars: {4}.",
                        natur, dt_amkr.ToString("dd-MM-yyyy HH:mm:ss"), num_vag, id_stations, id_ways), eventID);
                }
                return res;
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.UpdCarToStation] : Ошибка обновления вагона на путь станции, натурный лист: {0}, дата: {1}, № вагона: {2}, код станции системы RailCars: {3}, код пути системы RailCars: {4}. Подробно: (источник: {5}, № {6}, описание: {7})",
                    natur, dt_amkr.ToString("dd-MM-yyyy HH:mm:ss"), num_vag, id_stations, id_ways, e.Source, e.HResult, e.Message), eventID);
                return (int)errorTransfer.global;
            }
        }
        /// <summary>
        /// Поставить вагоны на путь станции АМКР
        /// </summary>
        /// <param name="orc_sostav"></param>
        /// <param name="id_stations"></param>
        /// <param name="id_ways"></param>
        /// <returns></returns>
        public int SetCarsToStation(ref Oracle_ArrivalSostav orc_sostav, int id_stations, int id_ways, int mode) 
        {
            
            if (orc_sostav == null) return 0;
            if (orc_sostav.ListNoSetWagons == null & orc_sostav.ListWagons == null) return 0;
            try
            {
                List<int> set_wagons = new List<int>();
                // Обнавляем вагоны
                if (orc_sostav.CountSetWagons != null & orc_sostav.ListNoSetWagons != null)
                {
                    set_wagons = GetWagonsToListInt(orc_sostav.ListNoSetWagons); // доствавим вагоны
                }
                // Ставим вагоны в первый раз
                if (orc_sostav.CountSetWagons == null & orc_sostav.ListNoSetWagons == null & orc_sostav.ListWagons != null)
                {
                    set_wagons = GetWagonsToListInt(orc_sostav.ListWagons); // поставим занаво
                }
                if (set_wagons.Count() == 0) return 0;
                ResultTransfers result = new ResultTransfers(set_wagons.Count(), 0, null, null, 0, 0);
                // Ставим вагоны на путь станции
                orc_sostav.ListNoSetWagons = null;
                foreach (int wag in set_wagons)
                {
                    mtcont.SetNaturToMTList(orc_sostav.NaturNum, wag, orc_sostav.DateTime, 15); // Поставим натурку на прибывший вагон по МТ

                    if (result.SetResultInsert(mode == 0 ? 
                        SetCarKISToStation(orc_sostav.NaturNum, wag, orc_sostav.DateTime, id_stations, id_ways, orc_sostav.IDOrcStation):
                        SetCarMTToStation(orc_sostav.NaturNum, wag, orc_sostav.DateTime, id_stations, id_ways, orc_sostav.IDOrcStation)))
                    {
                        // Ошибка
                        orc_sostav.ListNoSetWagons += wag.ToString() + ";";
                    }
                }
                orc_sostav.CountSetWagons = result.ResultInsert;
                LogRW.LogWarning(String.Format("Определено для переноса из системы КИС (натурный лист: {0}, дата: {1}) – {2} вагонов, поставлено на станцию АМКР (станция: {3}, путь: {4}) – {5} вагонов, ранее перенесено: {6} вагонов, ошибок переноса: {7}.",
                    orc_sostav.NaturNum,orc_sostav.DateTime,set_wagons.Count(),id_stations,id_ways,result.inserts,result.skippeds,result.errors),  eventID);
                // Сохранить результат и вернуть код
                if (oas.SaveOracle_ArrivalSostav(orc_sostav)<0) return (int)errorTransfer.global; else return result.ResultInsert;
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.SetCarsToStation] : Ошибка переноса вагонов состава IDOrcSostav: {0} на путь: {1} станции: {2}. Подробно: (источник: {3}, № {4}, описание: {5})",
                    orc_sostav.IDOrcSostav,id_stations,id_ways, e.Source, e.HResult, e.Message),  eventID);
                return (int)errorTransfer.global;
            }
            
        }
        /// <summary>
        /// Обновить информацию по вагонам перенесеным по данным КИС
        /// </summary>
        /// <param name="orc_sostav"></param>
        /// <param name="id_stations"></param>
        /// <param name="id_ways"></param>
        /// <returns></returns>
        public int UpdCarsToStation(ref Oracle_ArrivalSostav orc_sostav, int id_stations, int id_ways) 
        {
            
            if (orc_sostav == null) return 0;
            if (orc_sostav.CountSetWagons == null) return 0; // вагоны не поставлены на путь станции

            try
            {
                List<int> set_wagons = new List<int>();
                // Обнавляем вагоны
                if (orc_sostav.CountSetNatHIist != null & orc_sostav.ListNoUpdateWagons != null & orc_sostav.CountSetWagons != null)
                {
                    set_wagons = GetWagonsToListInt(orc_sostav.ListNoUpdateWagons); // до обновим вагоны
                }
                if (orc_sostav.CountSetNatHIist != null & orc_sostav.ListNoUpdateWagons == null & orc_sostav.CountSetWagons != null & orc_sostav.CountSetWagons!=orc_sostav.CountSetNatHIist)
                {
                    set_wagons = GetWagonsToListInt(orc_sostav.ListWagons); // поставим занаво
                }
                // Обновляем вагоны в первый раз
                if ((orc_sostav.CountSetNatHIist == null | orc_sostav.CountSetNatHIist == 0) & orc_sostav.ListNoUpdateWagons == null & orc_sostav.CountSetWagons != null)
                {
                    set_wagons = GetWagonsToListInt(orc_sostav.ListWagons); // поставим занаво
                }


                if (set_wagons.Count() == 0) return 0;
                
                ResultTransfers result = new ResultTransfers(set_wagons.Count(), 0, null, null, 0, 0);
                orc_sostav.ListNoUpdateWagons = null;
                // Ставим вагоны на путь станции
                foreach (int wag in set_wagons)
                {
                    if (result.SetResultInsert(UpdCarToStation(orc_sostav.NaturNum, wag, orc_sostav.DateTime, id_stations, id_ways, orc_sostav.IDOrcStation)))
                    {
                        // Ошибка
                        orc_sostav.ListNoUpdateWagons += wag.ToString() + ";";
                    }
                }
                orc_sostav.CountSetNatHIist = result.ResultInsert;
                LogRW.LogWarning(String.Format("Определено для обновления из системы КИС (натурный лист: {0}, дата: {1}) – {2} вагонов, поставлено на станцию АМКР (станция: {3}, путь: {4}) – {5} вагонов, ранее перенесено: {6} вагонов, ошибок переноса: {7}.",
                    orc_sostav.NaturNum,orc_sostav.DateTime,set_wagons.Count(),id_stations,id_ways,result.inserts,result.skippeds,result.errors),  eventID);
                // Сохранить результат и вернуть код
                if (oas.SaveOracle_ArrivalSostav(orc_sostav)<0) return (int)errorTransfer.global; else return result.ResultInsert;
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.SetCarsToStation] : Ошибка переноса вагонов состава IDOrcSostav: {0} на путь: {1} станции: {2}. Подробно: (источник: {3}, № {4}, описание: {5})",
                    orc_sostav.IDOrcSostav,id_stations,id_ways, e.Source, e.HResult, e.Message),  eventID);
                return (int)errorTransfer.global;
            }
            
        }
        /// <summary>
        /// Поставить вагоны состава на путь станции 
        /// </summary>
        /// <param name="orc_sostav"></param>
        /// <returns></returns>
        public int PutCarsToStation(ref Oracle_ArrivalSostav orc_sostav, int mode) 
        {
            // Определим станцию назначения
            int? id_stations = ref_kis.DefinitionIDStations(orc_sostav.IDOrcStation, orc_sostav.WayNum);
            if (id_stations == null)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.PutCarsToStation] :Ошибка получения id станции из справочника RailCars, натурный лист: {0}, дата :{1}, код станции системы КИС: {2}", orc_sostav.NaturNum, orc_sostav.DateTime.ToString("dd-MM-yyyy HH:mm:ss"), orc_sostav.IDOrcStation), eventID);
                return (int)errorTransfer.no_stations;
            }
            // Определим путь на станции
            int? id_ways = ref_kis.DefinitionIDWays((int)id_stations, orc_sostav.WayNum);
            if (id_ways == null)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.PutCarsToStation] :Ошибка получения id пути из справочника RailCars, натурный лист: {0}, дата :{1}, код станции системы RailCars: {2}, номер пути: {3}", orc_sostav.NaturNum, orc_sostav.DateTime.ToString("dd-MM-yyyy HH:mm:ss"), id_stations, orc_sostav.WayNum), eventID);
                return (int)errorTransfer.no_ways;
            }
            
            // Формирование общего списка вагонов и постановка их на путь станции прибытия
            List<PromVagon> list_pv = pc.GetVagon(orc_sostav.NaturNum, orc_sostav.IDOrcStation, orc_sostav.Day, orc_sostav.Month, orc_sostav.Year, orc_sostav.Napr == 2 ? true : false).ToList();
            List<PromNatHist> list_nh = pc.GetNatHist(orc_sostav.NaturNum, orc_sostav.IDOrcStation, orc_sostav.Day, orc_sostav.Month, orc_sostav.Year, orc_sostav.Napr == 2 ? true : false).ToList();
            int res_set_list = SetListWagon(ref orc_sostav, list_pv, list_nh);
            if (res_set_list >= 0)
            {
                return SetCarsToStation(ref orc_sostav, (int)id_stations, (int)id_ways, mode);
            }
            return res_set_list;
        }
        /// <summary>
        /// Обновить информацию о вагонах состава
        /// </summary>
        /// <param name="orc_sostav"></param>
        /// <returns></returns>
        public int UpdateCarsToStation(ref Oracle_ArrivalSostav orc_sostav, int mode) 
        {
            // Определим станцию назначения
            int? id_stations = ref_kis.DefinitionIDStations(orc_sostav.IDOrcStation, orc_sostav.WayNum);
            if (id_stations == null)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.UpdateCarsToStation] :Ошибка получения id станции из справочника RailCars, натурный лист: {0}, дата :{1}, код станции системы КИС: {2}", orc_sostav.NaturNum, orc_sostav.DateTime.ToString("dd-MM-yyyy HH:mm:ss"), orc_sostav.IDOrcStation), eventID);
                return (int)errorTransfer.no_stations;
            }
            // Определим путь на станции
            int? id_ways = ref_kis.DefinitionIDWays((int)id_stations, orc_sostav.WayNum);
            if (id_ways == null)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.UpdateCarsToStation] :Ошибка получения id пути из справочника RailCars, натурный лист: {0}, дата :{1}, код станции системы RailCars: {2}, номер пути: {3}", orc_sostav.NaturNum, orc_sostav.DateTime.ToString("dd-MM-yyyy HH:mm:ss"), id_stations, orc_sostav.WayNum), eventID);
                return (int)errorTransfer.no_ways;
            }
            // Обновим информацию по количеству вагонов в таблице NatHist
            List<PromNatHist> list_nh = pc.GetNatHist(orc_sostav.NaturNum, orc_sostav.IDOrcStation, orc_sostav.Day, orc_sostav.Month, orc_sostav.Year, orc_sostav.Napr == 2 ? true : false).ToList();
            orc_sostav.CountNatHIist = list_nh.Count() > 0 ?  list_nh.Count() as int? : null;
            int res_upd = UpdCarsToStation(ref orc_sostav, (int)id_stations, (int)id_ways);
            return res_upd;
        }


        public int SetInputSostavToStation(ref Oracle_InputSostav orc_sostav, int id_stations_from, int id_stations_on) 
        {
            return 0;//TODO: исправить возврат
        }
        /// <summary>
        /// Поставим состав с прибытия КИС в системе RailCars
        /// </summary>
        /// <param name="orc_sostav"></param>
        /// <returns></returns>
        public int PutInputSostavToStation(ref Oracle_InputSostav orc_sostav) 
        {
            // Определим станцию отправитель
            int? id_stations_from = ref_kis.DefinitionIDStations(orc_sostav.IDOrcStationFrom, orc_sostav.WayNumFrom);
            if (id_stations_from == null)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.PutInputSostavToStation] :Ошибка получения id станции из справочника RailCars, № документа: {0}, дата :{1}, код станции системы КИС: {2}", orc_sostav.DocNum, orc_sostav.DateTime.ToString("dd-MM-yyyy HH:mm:ss"), orc_sostav.IDOrcStationFrom), eventID);
                return (int)errorTransfer.no_stations;
            }
            // Определим станцию получатель
            int? id_stations_on = ref_kis.DefinitionIDStations(orc_sostav.IDOrcStationFrom, null);
            if (id_stations_on == null)
            {
                LogRW.LogError(String.Format("[KIS_RC_Transfer.PutInputSostavToStation] :Ошибка получения id станции из справочника RailCars, № документа: {0}, дата :{1}, код станции системы КИС: {2}", orc_sostav.DocNum, orc_sostav.DateTime.ToString("dd-MM-yyyy HH:mm:ss"), orc_sostav.IDOrcStationOn), eventID);
                return (int)errorTransfer.no_stations;
            }

            //// Определим путь на станции
            //int? id_ways = ref_kis.DefinitionIDWays((int)id_stations, orc_sostav.WayNum);
            //if (id_ways == null)
            //{
            //    LogRW.LogError(String.Format("[KIS_RC_Transfer.PutCarsToStation] :Ошибка получения id пути из справочника RailCars, натурный лист: {0}, дата :{1}, код станции системы RailCars: {2}, номер пути: {3}", orc_sostav.NaturNum, orc_sostav.DateTime.ToString("dd-MM-yyyy HH:mm:ss"), id_stations, orc_sostav.WayNum), eventID);
            //    return (int)errorTransfer.no_ways;
            //}
            
            // Формирование общего списка вагонов и постановка их на путь станции прибытия
            List<NumVagStanStpr1InStVag> list_pv = vc.GetSTPR1InStVag(orc_sostav.DocNum, orc_sostav.NaprFrom == 2 ? true : false).ToList();
            return SetInputSostavToStation(ref orc_sostav, (int)id_stations_from , (int)id_stations_on);

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
            return ref_kis.SynchronizeWagons(day);
        }
        #endregion

        #region Чистка старых запесей прибытие и зачисление вагонов на станцию
        /// <summary>
        /// Очистить записи прибытия вагонов за day от текущей даты
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public int ClearArrivingWagons(int[] stations , int day)
        {
            int result=0;
            foreach (int id_station in stations) 
            {
                result += rc_vo.ClearArrivingWagons(id_station, DateTime.Now.AddDays(-1 * day));
            }
            return result;
        }
        /// <summary>
        /// Очистить записи ожидания зачисления на путь вагонов за day от текущей даты
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public int ClearPendingWagons(int[] stations, int day)
        {
            //TODO: ВЫПОЛНИТЬ код очистки из ожидания если надо
            return 0;
        }
        #endregion

        #region Убрать вагоны из прибытия или перенести на станцию
        /// <summary>
        /// Убрать вагоны из прибытия УЗ по данным МТ, принятые по КИС
        /// </summary>
        /// <param name="natur_list"></param>
        /// <param name="dt_amkr"></param>
        /// <returns></returns>
        public int DeleteInArrival(int natur_list, DateTime dt_amkr) 
        {
            List<MTList> list = mtcont.GetListToNatur(natur_list, dt_amkr, 2).ToList();
            if (list.Count() == 0) return 0;
            ResultTransfers result = new ResultTransfers(list.Count(), null, null, 0, 0, 0);
            foreach (MTList mt in list) 
            {
                if (result.SetResultDelete(rc_vo.DeleteVagonsToInsertMT(mt.IDMTSostav, mt.CarriageNumber))) 
                {
                    // Ошибка
                }
            }
            if (result.errors > 0) { LogRW.LogError(String.Format("[KIS_RC_Transfer.DeleteInArrival] :Ошибка удаления вагонов из прибытия с УЗ после переноса по данным КИС, натурный лист: {0}, дата: {1}, количество ошибок: {2}",
                natur_list,dt_amkr,result.errors)
                , eventID); }
            return result.ResultDelete;
        }
        #endregion

    }
}
