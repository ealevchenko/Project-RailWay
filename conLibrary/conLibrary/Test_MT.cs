using EFRailWay.Entities;
using EFRailWay.Entities.KIS;
using EFRailWay.KIS;
using EFRailWay.MT;
using MetallurgTrans.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conLibrary
{
    public class Test_MT
    {
        public Test_MT() { }

        public void Test_MT_Helpers_MT_ArrivalToRailWay()
        {
            MT mts = new MT();

            int v1 = mts.ArrivalToRailWay(4230);                
            int v2 = mts.ArrivalToRailWay(4231);
                if (v1>0){
                Console.WriteLine("Состав {0} - вагонов {1}", "d", v1);}
 

        }

        public void Test_MT_Helpers_MT_ArrivalToRailWayAll()
        {
            MT mts = new MT();
            MTContent mt = new MTContent();

            foreach (MTSostav sos in mt.Get_MTSostav())
            {
                int v = mts.ArrivalToRailWay(sos.IDMTSostav);
                if (v>0){
                Console.WriteLine("Состав {0} - вагонов {1}", sos.CompositionIndex, v);}
 
            }


        }

        public void Test_SUD_MTContent_MTConsignee()
        {
            MTContent mt = new MTContent();
            MTConsignee mtc3437 = new MTConsignee() {
                Code = 3437,
                CodeDescription = "основной код ПАО АМКР",
                Consignee = (int)tMTConsignee.AMKR
            };
            mt.SaveMTConsignee(mtc3437);
            MTConsignee mtc6302 = new MTConsignee()
            {
                Code = 6302,
                CodeDescription = "вспомогательный код при отправке досылочных грузов (добавлен в регламент)",
                Consignee = (int)tMTConsignee.AMKR
            };
            mt.SaveMTConsignee(mtc6302);
            MTConsignee mtc9999 = new MTConsignee()
            {
                Code = 9999,
                CodeDescription = "вспомогательный код при отправке досылочных грузов (добавлен в регламент)",
                Consignee = (int)tMTConsignee.AMKR
            };
            mt.SaveMTConsignee(mtc9999);
            MTConsignee mtc0 = new MTConsignee()
            {
                Code = 0,
                CodeDescription = "0",
                Consignee = (int)tMTConsignee.AMKR
            };
            int mtc0_add = mt.SaveMTConsignee(mtc0);
            MTConsignee mtc1 = new MTConsignee()
            {
                Code = mtc0_add,
                CodeDescription = "1",
                Consignee = (int)tMTConsignee.AMKR
            };
            int del = mt.SaveMTConsignee(mtc1);

            MTConsignee mtc_del = mt.DeleteMTConsignee(del);
        }

        public void Test_MTContent_MTList()
        {
            ArrivalSostav aso = new ArrivalSostav();
            MTContent mt = new MTContent();            
            foreach (Oracle_ArrivalSostav oas in aso.Get_ArrivalSostavNoClose().Where(a => a.ListWagons != null))
            {
                Console.WriteLine("natur {0}, count = {1}",oas.NaturNum, mt.GetIDSostavToWagons(oas.ListWagons,oas.DateTime).Count());
            }

            //string sw = "60662830;65383853;67660423;74021965;65046567;56560055;24534372;50546159;57517948;55977870;55763296;55320519;63627558;65206062;67751354;67794347;64083868;66522079;65009771;66495458;67288738;65125502;65710154;66399379;65469769;62823661;65438236;55063119;53777959;60832383;56765530;56138001;53436556;52749769;56936644;56969702;52876307;53551669;60091303;55119531;55118251;61246328;59717553;57411928;60267812;52965274;55139851;52733540;53569752;66680547;65483307;60261211;56982416;54122338;61243358;60806072;62034137;59785881;";
            //Console.WriteLine("id sostav = {0}", mt.GetIDSostavToWagons(sw));
        }

    }
    ////**************** ТЕСТ MT ***********************************************************
    //MTContent mt = new MTContent();
    //MTSostav mtsostav = new MTSostav()
    //{
    //    IDMTSostav = 0,
    //    FileName = "regl_8701-058-4670_2016082212254.xml",
    //    CompositionIndex = "8701-058-4670",
    //    DateTime = DateTime.Parse("2016-08-22 12:25:00.000"),
    //    Operation = 1,
    //    Create = DateTime.Parse("2016-09-07 15:07:45.000"),
    //    Close = DateTime.Parse("2016-09-07 15:07:45.697"),
    //    ParentID = null
    //};
    // int sostav = mt.SaveMTSostav(mtsostav);
    //MTSostav mtsostavnew = new MTSostav()
    //{
    //    IDMTSostav = sostav,
    //    FileName = ".xml",
    //    CompositionIndex = "8701-058-4670",
    //    DateTime = DateTime.Parse("2016-08-22 12:25:00.000"),
    //    Operation = 1,
    //    Create = DateTime.Parse("2016-09-07 15:07:45.000"),
    //    Close = DateTime.Parse("2016-09-07 15:07:45.697"),
    //    ParentID = null
    //};            
    //int sostavnew = mt.SaveMTSostav(mtsostavnew);

    //MTList mtl = new MTList()
    //{
    //    IDMTList = 0,
    //    IDMTSostav = sostav,
    //    Position = 1,
    //    CarriageNumber = 60490943,
    //    CountryCode = 221,
    //    Weight = 57,
    //    IDCargo = 17101,
    //    Cargo = "sss",
    //    IDStation = 46600,
    //    Station = "НИКОПОЛЬ",
    //    Consignee = 3925,
    //    Operation = "ТСП",
    //    CompositionIndex = "3540-097-4670",
    //    DateOperation = DateTime.Parse("2016-08-22 11:40:00.000"),
    //    TrainNumber = 1402,
    //    NaturList = null
    //};
    //int list = mt.SaveMTList(mtl);
    //MTList mtl_ch = new MTList()
    //{
    //    IDMTList = list,
    //    IDMTSostav = sostav,
    //    Position = 1,
    //    CarriageNumber = 0,
    //    CountryCode = 221,
    //    Weight = 57,
    //    IDCargo = 17101,
    //    Cargo = "d",
    //    IDStation = 46600,
    //    Station = "d",
    //    Consignee = 3925,
    //    Operation = "ТСП",
    //    CompositionIndex = "3540-097-4670",
    //    DateOperation = DateTime.Parse("2016-08-22 11:40:00.000"),
    //    TrainNumber = 1402,
    //    NaturList = null
    //};
    //int list_ch = mt.SaveMTList(mtl_ch);
    //MTList mtl_del = mt.DeleteMTList(list);

    //MTSostav mts = mt.DeleteMTSostav(sostav);
    //{ }
}
