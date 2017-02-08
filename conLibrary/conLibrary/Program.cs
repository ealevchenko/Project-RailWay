using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using EFRailWay.References;
using EFRailWay.Concrete;
using EFRailWay.Abstract;
using EFISA95.Concrete;
using EFISA95.Entities;
using EFISA95;
using EFWagons.Concrete;
using EFWagons.Entities;
using EFRailWay.Entities;
using EFISA95.Backup;
using EFRailWay.Settings;
using EFRailWay.MT;
using EFWagons.Statics;
using KIS.Service;

namespace conLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            //string s = "abc---def";
            ////
            //Console.WriteLine("Index: 012345678");
            //Console.WriteLine("1)     {0}", s);
            //Console.WriteLine("2)     {0}", s.Remove(3));
            //Console.WriteLine("2)     {0}", s.Remove(199));
            //Console.WriteLine("3)     {0}", s.Remove(3, 3));
            
            #region Test_MT
            //Test_MT test = new Test_MT();
             //test.Test_MT_Helpers_MT_ArrivalToRailWay();
            //test.Test_MT_Helpers_MT_ArrivalToRailWayAll();
            //test.Test_SUD_MTContent_MTConsignee();
            //test.Test_MTContent_MTList();
            //test.Test_MTContent_GetListToNatur();
            //test.Test_MT_CompareMT_SAP();
            //test.Test_MT_CorrectionMT_SAP();
            //test.Test_MTContent_GetOperationMTSostavDestinct();
            #endregion

            #region Test_TrasferKIS
            Test_TrasferKIS test = new Test_TrasferKIS();
            //test.Test_ArrivalKIS();
            //test.Test_References_Owner();
            //test.Test_References_Vagon();
            //test.Test_References_OwnersContries();
            //test.Test_References_Gruzs();
            //test.Test_References_Shop();
            //test.Test_TrasferKIS_KISTransfer();

            //test.Test_KIS_RC_Transfer_SetListWagon();
            //test.Test_References_SynchronizeWagons(2);
            //test.Test_KIS_RC_Transfer_ClearArrivingWagons();
            //test.Test_KIS_RC_Transfer_DeleteInArrival();
            //test.Test_SAP_IncomingSupply();
            //test.Test_KIS_RulesCopy_GetRulesCopyToOracleRules();
            //test.Test_KIS_Transfer_CopyInputSostavToRailway();
            test.Test_KIS_Transfer_CopyOutputSostavToRailway();     //Тест переноса данных по отправке
            //test.Test_KIS_Transfer_PutInputSostavToStation();
            //test.Test_KIS_RC_Transfer_PutInputSostavToStation();  // тест поставить строку (копирование внутрених станций) на путь
            //test.Test_KIS_RC_Transfer_PutCarsToStation_UpdateCarsToStation();

            //test.Test_SAPIncomingSupply_GetDefaultIDSostav();
            //test.Test_KIS_RC_Transfer_CorrectionArrivalSostav();
            //test.Test_TrasferKIS_KISTransfer_PutCarsToStations(); // Проверить полный перенос вагона из КИС в RailCars
            #endregion

            #region Test_Wagons
            //Test_Wagons test = new Test_Wagons();
            //test.Test_KometaContent_KometaVagonSob();
            //test.Test_KometaContent_KometaVagonSob(67666503);
            //test.Test_KometaContent_KometaVagonSob(67666503,DateTime.Now);
            //test.Test_KometaContent_KometaVagonSob(0,DateTime.Now);

            //test.Test_KometaContent_KometaSobstvForNakl();
            //test.Test_KometaContent_KometaSobstvForNakl(83);

            //test.Test_KometaContent_GetKometaStrana();
            //int[] ints = new int[] { 4, 11, 3, 40, 10, 36, 43, 46 };
            //foreach (int i in ints) {
            //    Console.WriteLine("int = {0}", i);
            //    test.Test_KometaContent_GetKometaStrana(42);
            //}

            //test.Test_PromContent_GetGruzSP();
            //test.Test_PromContent_GetGruzSP(290);
            //test.Test_PromContent_GetGruzSPToTarGR(161012,false);
            //test.Test_PromContent_GetGruzSPToTarGR(null,false);
            //test.Test_PromContent_GetGruzSPToTarGR(16100,true);

            //test.Test_PromContent_GetNatHist();
            //test.Test_PromContent_GetNatHist(3920,1,21,11,2016,67251868);
            //test.Test_PromContent_GetCex();
            //test.Test_PromContent_GetCex(148);
             //test.Test_VagonsContent_GetSTPR1GR();
            //test.Test_VagonsContent_GetSTPR1GR(26);
            //test.Test_VagonsContent_GetSTPR1InStDoc();
            //test.Test_VagonsContent_GetSTPR1InStDocOfAmkr();
            //test.Test_VagonsContent_GetSTPR1InStDocOfAmkrWhere();
            //test.Test_VagonsContent_GetSTPR1InStVag();
            //test.Test_VagonsContent_GetCountSTPR1InStVag();
            //test.Test_PromContent_TestFilter();
            //test.Test_VagonsContent_GetSTPR1OutStDoc(); // Получить все составы по отправке
            //test.Test_VagonsContent_GetSTPR1OutStVag(); // Получить все вагоны по отправке
            //test.Test_VagonsContent_GetStpr1Tupik(); // Список тупиков
            #endregion

            #region Test_RailCars
            //Test_RailCars test = new Test_RailCars();
            //test.Test_RC_Vagons();
            //test.Test_RC_Vagons(53075479);
            //test.Test_SUD_RC_Vagons();
            //test.Test_RC_Owners();
            //test.Test_RC_Owners_GetOwnersToKis(42);
            //test.Test_RC_Owners(47);
            //test.Test_SUD_RC_Owners();
            //test.Test_RC_VagonsOperations();
            //test.Test_RC_VagonsOperations(278155);
            //test.Test_RC_VagonsOperationsToNatur(7623, DateTime.Parse("2016-03-09 05:25:00.0000000"));
            //test.Test_RC_VagonsOperationsToNatur(7623, DateTime.Parse("2016-03-09 05:25:00.0000000"), 238310);
            //test.Test_RC_IsVagonOperation(7623, DateTime.Parse("2016-03-09 05:25:00.0000000"), 238310);
            //test.Test_RC_IsVagonOperation(7623, DateTime.Parse("2016-03-09 05:25:00.0000000"), 0);
            //test.Test_RC_VagonsOperations_DelseteIDSostav();
            //test.Test_RC_MaxPositionWay(162);
            //test.Test_RC_MaxPositionWay(100);
            //test.Test_RC_VagonsOperationsToInsertMT(3166);
            //test.Test_SUD_RC_VagonsOperations();
            //test.Test_DeleteVagonsToInsertMT();
            //test.Test_DeleteVagonsToNaturList();
            //test.Test_InsertVagon();
            //test.Test_RC_Ways();
            //test.Test_RC_WaysToStations(4);
           // test.Test_RC_WaysToStations(4, "1");
            //test.Test_SUD_RC_Ways();
            //test.Test_RC_OwnersContries();
            //test.Test_RC_OwnersContries(1);
            //test.Test_RC_OwnersContriesToKis(22);
            //test.Test_RC_Gruzs();
            //test.Test_RC_Gruzs(285,null);
            //test.Test_RC_Gruzs(null,211);
            //test.Test_RC_Gruzs(285, 211);
            //test.Test_RC_Gruzs(null, null);
            //test.Test_SUD_RC_Gruzs();
            //test.Test_RC_Shops();
            //test.Test_RC_Shops(85);
            //test.Test_RC_ShopsOfKis(54);
            //test.Test_SUD_RC_Shops();
            //test.Test_Maneuvers_ManeuverCars(); // Тест маневр вагонов на станции
            //test.Test_RC_VagonsOperations_UpdateVagon(); // тест обновления вагона после ручного принятия вагона на станцию АМКР
            #endregion

            #region Test_Reference_RW
            //Test_Reference_RW test = new Test_Reference_RW();
            //test.Test_GeneralReferencesCargo();
            //test.Test_GeneralReferencesCountry();
            #endregion

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(); 

        }
    }
}
            //int[] ints = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            //foreach (int i in ints)
            //{
            //    Console.WriteLine("int: {0}, result % 2: {1}", i, i % 2);
            //}