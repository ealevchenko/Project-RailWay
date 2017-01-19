using EFRailWay.Entities;
using EFRailWay.Entities.KIS;
using EFRailWay.Entities.Railcars;
using EFRailWay.KIS;
using EFRailWay.MT;
using EFRailWay.Railcars;
using EFRailWay.References;
using EFRailWay.SAP;
using EFRailWay.Statics;
using EFWagons.Entities;
using EFWagons.Statics;
using KIS.Service;
using MetallurgTrans.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferWagons.Railcars;
using TransferWagons.RailWay;
using TransferWagons.Transfers;

namespace conLibrary
{
    public class Test_TrasferKIS
    {
        ArrivalSostav oas = new ArrivalSostav();
        InputSostav ois = new InputSostav();
        PromContent pc = new PromContent();
        RulesCopy rc = new RulesCopy();
        
        public Test_TrasferKIS() { }

        public void Test_ArrivalKIS() 
        {
            ArrivalKIS akis = new ArrivalKIS();
            akis.Transfer();        
        }

        public void Test_References_Owner() 
        {
            TransferWagons.Railcars.ReferencesKIS refer = new TransferWagons.Railcars.ReferencesKIS();
            //refer.DefinitionIDOwner(53,null);
            Console.WriteLine("Новая перевозочная компания 123 = {0}", refer.DefinitionIDOwner(53, null));
            Console.WriteLine("Null = {0}", refer.DefinitionIDOwner(0, null));
            int? new_ow = refer.DefinitionIDOwner(-1, null);
            Console.WriteLine("Новый (-1) = {0}", new_ow);
            if (new_ow != null) 
            {
                RC_Owners ow = new RC_Owners();
                OWNERS ow_del = ow.DeleteOwners((int)new_ow);

            }
        }

        public void Test_References_Vagon()
        {
            TransferWagons.Railcars.ReferencesKIS refer = new TransferWagons.Railcars.ReferencesKIS();
            //Console.WriteLine("вагон id (917664) = {0}", refer.DefinitionIDVagon(67166710, DateTime.Now));
            ////Console.WriteLine("Новый вагон #67669887 = {0}", refer.DefinitionIDVagon(67669887, DateTime.Now));
            //Console.WriteLine("Null = {0}", refer.DefinitionIDVagon(0, DateTime.Now));
            //int? new_vag = refer.DefinitionIDVagon(67669887, DateTime.Now);
            //Console.WriteLine("Новыйвагон #67669887 ID = {0}", new_vag);
            //if (new_vag != null)
            //{
            //    RC_Vagons vag = new RC_Vagons();
            //    VAGONS vag_del = vag.DeleteVAGONS((int)new_vag);

            //}
        }

        public void Test_References_OwnersContries() 
        {
            TransferWagons.Railcars.ReferencesKIS refer = new TransferWagons.Railcars.ReferencesKIS(); 
            Console.WriteLine("Украина 1= {0}", refer.DefinitionIDOwnersContries(22));
            Console.WriteLine("Null = {0}", refer.DefinitionIDOwnersContries(0));
            //int? new_oc = refer.DefinitionIDOwnersContries(100);
            //Console.WriteLine("США ID = {0}", new_oc);
        }

        public void Test_References_Gruzs() 
        {
            TransferWagons.Railcars.ReferencesKIS refer = new TransferWagons.Railcars.ReferencesKIS(); 


            //Console.WriteLine("Уголь марки Ж 1= {0}", refer.DefinitionIDGruzs(285, null) );
            //Console.WriteLine("порожний  111= {0}", refer.DefinitionIDGruzs(null,9));
            //Console.WriteLine("null= {0}", refer.DefinitionIDGruzs(null, null));
            //Console.WriteLine("null= {0}", refer.DefinitionIDGruzs(285, 9));
            //Console.WriteLine("Уголь каменный = {0}", refer.DefinitionIDGruzs(161005));
            //Console.WriteLine("Уголь каменный = {0}", refer.DefinitionIDGruzs(16100));
            Console.WriteLine("Зерно кукурузы = {0}", refer.DefinitionIDGruzs(15006));
            
        }

        public void Test_References_Shop() 
        {
            TransferWagons.Railcars.ReferencesKIS refer = new TransferWagons.Railcars.ReferencesKIS();

            int? ch = refer.DefinitionIDShop(20);
            //Console.WriteLine("Уголь марки Ж 1= {0}", refer.DefinitionIDGruzs(285, null) );
            //Console.WriteLine("порожний  111= {0}", refer.DefinitionIDGruzs(null,9));
            //Console.WriteLine("null= {0}", refer.DefinitionIDGruzs(null, null));
            //Console.WriteLine("null= {0}", refer.DefinitionIDGruzs(285, 9));
            //Console.WriteLine("Уголь каменный = {0}", refer.DefinitionIDGruzs(161005));
            //Console.WriteLine("Уголь каменный = {0}", refer.DefinitionIDGruzs(16100));
            Console.WriteLine("Зерно кукурузы = {0}", refer.DefinitionIDGruzs(15006));
            
        }

        public void Test_TrasferKIS_KISTransfer() { 
            KIS_Transfer kist = new KIS_Transfer();

            //kist.DayControllingAddNatur = 2;
            kist.PutCarsToStations(0);
            //kist.UpdateSostavs();
                Console.WriteLine("Обновлено {0}", kist.CopyArrivalSostavToRailway(2));
        }

        public void Test_KIS_RC_Transfer_SetListWagon() {


            Oracle_ArrivalSostav oras = oas.Get_ArrivalSostav(1364);
            List<PromVagon> list_pv = pc.GetVagon(oras.NaturNum, oras.IDOrcStation, oras.Day, oras.Month, oras.Year, oras.Napr == 2 ? true : false).ToList();
            List<PromNatHist> list_nh = pc.GetNatHist(oras.NaturNum, oras.IDOrcStation, oras.Day, oras.Month, oras.Year, oras.Napr == 2 ? true : false).ToList();

            KIS_RC_Transfer kisrs = new KIS_RC_Transfer();
            int res = kisrs.SetListWagon(ref oras, list_pv, list_nh);

            Console.WriteLine("Обновлено {0}", res);
        }

        public void Test_References_SynchronizeWagons(int day) 
        {
            TransferWagons.Railcars.ReferencesKIS refer = new TransferWagons.Railcars.ReferencesKIS();
            refer.SynchronizeWagons(day);
            
        }

        public void Test_KIS_RC_Transfer_ClearArrivingWagons() 
        { 
            KIS_RC_Transfer kisrs = new KIS_RC_Transfer();
            int res = kisrs.ClearArrivingWagons(new int[] { 3, 9, 10, 11, 14, 18, 19, 21, 22, 25, 26 }, 2);        
        }

        public void Test_KIS_RC_Transfer_DeleteInArrival() 
        { 
            KIS_RC_Transfer kisrs = new KIS_RC_Transfer();
            int res = kisrs.DeleteInArrival(4066,DateTime.Parse("2016-11-28 10:30:00"));        
        }

        #region тест справочник САП входящие поставки

        public void Test_SAP_IncomingSupply() 
        {
            //ArrivalToRailWay(4753);
            //ArrivalToRailWay(4754);
            //ArrivalToRailWay(4762);
            //ArrivalToRailWay(4764);
            ArrivalToRailWay(4766);
        }

        private bool IsConsignee(int code, int[] codes_consignee)
        {
            foreach (int c in codes_consignee)
            {
                if (c == code) return true;
            }
            return false;
        }

        private List<trWagon> GetListWagonInArrival(IQueryable<MTList> list, int? id_stat_receiving, int[] code_consignee)
        {
            //bool bOk = false;
            if (list == null | id_stat_receiving == null | code_consignee == null) return null;
            List<trWagon> list_wag = new List<trWagon>();
            try
            {
                int position = 1;
                foreach (MTList wag in list)
                {
                    // состояние вагонов
                    int id_conditions = 17; // ожидает прибытие с УЗ                        
                    // червоная
                    if (id_stat_receiving == 467201)
                    {
                        if (wag.IDStation == id_stat_receiving & IsConsignee(wag.Consignee, code_consignee))
                        {
                            //bOk = true; 
                            list_wag.Add(new trWagon()
                            {
                                Position = position++,
                                CarriageNumber = wag.CarriageNumber,
                                CountryCode = wag.CountryCode,
                                Weight = wag.Weight,
                                IDCargo = wag.IDCargo,
                                Cargo = wag.Cargo,
                                IDStation = wag.IDStation,
                                Station = wag.Station,
                                Consignee = wag.Consignee,
                                Operation = wag.Operation,
                                CompositionIndex = wag.CompositionIndex,
                                DateOperation = wag.DateOperation,
                                TrainNumber = wag.TrainNumber,
                                Conditions = id_conditions,
                            });
                        }
                        //else { id_conditions = 18; } // маневры на УЗ
                        // если есть хоть один вагон АМКР и конечная станция червонная
                    }
                    // главн
                    if (id_stat_receiving == 467004)
                    {
                        //bOk = true;
                        if (wag.IDStation != id_stat_receiving | !IsConsignee(wag.Consignee, code_consignee))
                            return null; // есть вагон недошедший до станции назанчения или с кодом грузополучателя не АМКР 
                        list_wag.Add(new trWagon()
                        {
                            Position = wag.Position,
                            CarriageNumber = wag.CarriageNumber,
                            CountryCode = wag.CountryCode,
                            Weight = wag.Weight,
                            IDCargo = wag.IDCargo,
                            Cargo = wag.Cargo,
                            IDStation = wag.IDStation,
                            Station = wag.Station,
                            Consignee = wag.Consignee,
                            Operation = wag.Operation,
                            CompositionIndex = wag.CompositionIndex,
                            DateOperation = wag.DateOperation,
                            TrainNumber = wag.TrainNumber,
                            Conditions = id_conditions,
                        });
                    }

                }
            }
            catch (Exception e)
            {
                //LogRW.LogError(String.Format("[MT.GetListWagonInArrival] :Ошибка формирования перечня вагонов List<trWagon> (источник: {0}, № {1}, описание:  {2})", e.Source, e.HResult, e.Message), this.eventID);
                return null;
            }
            return list_wag;
        }

        private int ArrivalToRailWay(int id_sostav)
        {
            try
            {
                KIS_RC_Transfer rc_transfer = new KIS_RC_Transfer(); // Перенос в системе RailCars
                KIS_RW_Transfer rw_transfer = new KIS_RW_Transfer(); // Перенос в системе RailWay
                MTContent mtc = new MTContent();
                ReferenceRailway refRW = new ReferenceRailway();
                SAP_Transfer saptr = new SAP_Transfer();
                // Определим класс данных состав
                MTSostav sost = mtc.Get_MTSostav(id_sostav);
                // Определим код станции по справочникам
                int? codecs_in = refRW.GetCodeCSStations(int.Parse(sost.CompositionIndex.Substring(9, 4)) * 10);
                int? codecs_from = refRW.GetCodeCSStations(int.Parse(sost.CompositionIndex.Substring(0, 4)) * 10);
                // Определим класс данных вагоны
                List<trWagon> list_wag = new List<trWagon>();
                list_wag = GetListWagonInArrival(mtc.Get_MTListToSostav(id_sostav), codecs_in, mtc.GetMTConsignee(tMTConsignee.AMKR));
                trSostav sostav = new trSostav()
                {
                    id = sost.IDMTSostav,
                    codecs_in_station = codecs_in,
                    codecs_from_station = codecs_from,
                    //FileName = sost.FileName,
                    //CompositionIndex = sost.CompositionIndex,
                    DateTime = sost.DateTime,
                    //Operation = sost.Operation,
                    //Create = sost.Create,
                    //Close = sost.Close,
                    ParentID = sost.ParentID,
                    Wagons = list_wag != null ? list_wag.ToArray() : null,
                };
                // Поставим вагоны в систему RailCars
                int res_arc;
                try
                {
                    //res_arc = rc_transfer.PutInArrival(sostav);
                    res_arc = saptr.PutInSapIncomingSupply(sostav);
                    if (res_arc < 0)
                    {
                        //LogRW.LogError(String.Format("[MT.ArrivalToRailWay] :Ошибка переноса состава в прибытие системы RailCars, состав: {0}, код ошибки: {1}.", sostav.id, res_arc), this.eventID);
                    }
                }
                catch (Exception e)
                {
                    //LogRW.LogError(String.Format("[MT.ArrivalToRailWay] :Ошибка переноса состава в прибытие системы RailCars, состав: {0}. Подробно: (источник: {1}, № {2}, описание:  {3})", sostav.id, e.Source, e.HResult, e.Message), this.eventID);
                    res_arc = -1;
                }
                // Поставим вагоны в систему RailWay            
                // TODO: Выполнить код постановки вагонов в систему RailWay (прибытие из КР)
                // ..................
            }
            catch (AggregateException agex)
            {
                agex.Handle(ex =>
                {
                    //LogRW.LogError(String.Format("[MT.ArrivalToRailWay]: Общая ошибка переноса состава в прибытие системы RailWay (источник: {0}, № {1}, описание:  {2})", ex.Source, ex.HResult, ex.Message), this.eventID);
                    return true;
                });
                return -1;
            }
            return 0;//TODO: исправить возврат
        }



        public void Test_SAPIncomingSupply_GetDefaultIDSostav()
        {
            SAP_Transfer mt = new SAP_Transfer();
            Console.WriteLine("natur {0}", mt.GetDefaultIDSostav());
        }
        

        #endregion  
   
        public void Test_KIS_RulesCopy_GetRulesCopyToOracleRules()
        {
            List<OracleRules> list = rc.GetRulesCopyToOracleRules(typeOracleRules.Input);
            List<OracleRules> list1 = rc.GetRulesCopyToOracleRulesOfKis(typeOracleRules.Input);
            string wh1 = "";
            wh1 = wh1.ConvertWhere(list, "k_stan", "st_in_st ", "OR");
            string wh2 = "";
            wh2 = wh2.ConvertWhere(list1, "a.k_stan", "st_in_st ", "OR");

        }
   
        public void Test_KIS_Transfer_CopyInputSostavToRailway() { 
            KIS_Transfer kist = new KIS_Transfer();
            Console.WriteLine("Обновлено {0}", kist.CopyInputSostavToRailway(1));  
        }

        public void Test_KIS_Transfer_PutInputSostavToStation()
        { 
            KIS_Transfer kist = new KIS_Transfer();
            Console.WriteLine("Обновлено {0}", kist.PutInputSostavToStation());  
        }      

        public void Test_KIS_RC_Transfer_PutInputSostavToStation() {


            Oracle_InputSostav oris = ois.GetInputSostav(27);

            KIS_RC_Transfer kisrs = new KIS_RC_Transfer();
            int res = kisrs.PutInputSostavToStation(ref oris);

            Console.WriteLine("Обновлено {0}", res);
        }

        public void Test_KIS_RC_Transfer_PutCarsToStation_UpdateCarsToStation()
        {

            KIS_RC_Transfer transfer_rc = new KIS_RC_Transfer();
            Oracle_ArrivalSostav oras = oas.Get_ArrivalSostav(4172);
            //52928280
            int res_put = transfer_rc.PutCarsToStation(ref oras, 1);
            //TODO: ВКЛЮЧИТЬ КОД: Обновление составов на станции АМКР системы RailCars
            int res_upd = transfer_rc.UpdateCarsToStation(ref oras, 1);

            Console.WriteLine("Обновлено {0},{1}", res_put, res_upd);
        }

    }

}
