using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferWagons.Transfers;

namespace conLibrary
{
    public class Test_Reference_RW
    {
        private References trans_ref = new References();

        public Test_Reference_RW() { }
        
        public void Test_GeneralReferencesCargo()
        {
            Console.WriteLine("ГЛИНА КИСЛОТОУПОРНАЯ И ОГНЕУПОРНАЯ, НЕ ПОИМЕНОВАНН = {0}", trans_ref.DefinitionIDCargo(301010));
            Console.WriteLine("Оксиды магния природные = {0}", trans_ref.DefinitionIDCargo(241604));
            Console.WriteLine("Оксиды магния природные = {0}", trans_ref.DefinitionIDCargo(300));
        }

        public void Test_GeneralReferencesCountry()
        {
            //Console.WriteLine("221 = {0}", trans_ref.DefinitionIDCountrySNG(221));
            Console.WriteLine("20 = {0}", trans_ref.DefinitionIDCountrySNG(20));
            Console.WriteLine("21 = {0}", trans_ref.DefinitionIDCountrySNG(21));
            Console.WriteLine("22 = {0}", trans_ref.DefinitionIDCountrySNG(22));
        }

        public void Test_GeneralReferencesStation()
        {
            //Console.WriteLine("Одеса порт = {0}", trans_ref.DefinitionIDStation(400409));
            Console.WriteLine("0 = {0}", trans_ref.DefinitionIDStation(676));
        }
    }


    //IReferenceRailwayRepository rep_rr = new EFReferenceRailwayRepository();
    //ReferenceRailway Reference = new ReferenceRailway(rep_rr);


    //foreach (Code_Station station in Reference.GetStations())
    //{

    //    Console.WriteLine("station.Code: {0}, station.CodeCS: {1}, station.Name: {2}", station.Code, station.CodeCS, station.Station);
    //}

    //Reference.SetInternalRailroadToStation();

    //Code_Station cst = new Code_Station() { IDStation = 14153, Code = 70500, CodeCS = 705000, Station = "КУРМАНГАЗЫ", IDInternalRailroad = 16 };
    //
    //Reference.SaveStation();


    //foreach (Code_InternalRailroad cir in Reference.GetInternalRailroads()) 
    //{
    //    string[] arrayCodes = cir.StationsCodes.Split(';');
    //    foreach (string Codes in arrayCodes) 
    //    {
    //        if (!String.IsNullOrWhiteSpace(Codes))
    //        {
    //            string[] arraywhere = Codes.Split('–');
    //            switch (arraywhere.Count())
    //            {
    //                case 0: break;
    //                case 1: Console.WriteLine("Codes: {0}, arraywhere: {1}", Codes, arraywhere[0]); break;
    //                case 2: Console.WriteLine("Codes: {0}, arraywhere: {1} - {2}", Codes, arraywhere[0],arraywhere[1]); break;
    //                default: Console.WriteLine("Codes: {0}, ! количество arraywhere: {1}", Codes, arraywhere.Count()); break;
    //            }

    //        }
    //    }
    //}
}
