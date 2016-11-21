using EFRailWay.Entities.KIS;
using EFRailWay.Entities.Railcars;
using EFRailWay.KIS;
using EFRailWay.Railcars;
using EFWagons.Entities;
using EFWagons.KIS;
using KIS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferWagons.RailCars;
using TransferWagons.Transfers;

namespace conLibrary
{
    public class Test_TrasferKIS
    {
        ArrivalSostav oas = new ArrivalSostav();
        PromContent pc = new PromContent();
        
        public Test_TrasferKIS() { }

        public void Test_ArrivalKIS() 
        {
            ArrivalKIS akis = new ArrivalKIS();
            akis.Transfer();        
        }

        public void Test_References_Owner() 
        {
            References refer = new References();
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
            References refer = new References();
            Console.WriteLine("вагон id (917664) = {0}", refer.DefinitionIDVagon(67166710, DateTime.Now));
            //Console.WriteLine("Новый вагон #67669887 = {0}", refer.DefinitionIDVagon(67669887, DateTime.Now));
            Console.WriteLine("Null = {0}", refer.DefinitionIDVagon(0, DateTime.Now));
            int? new_vag = refer.DefinitionIDVagon(67669887, DateTime.Now);
            Console.WriteLine("Новыйвагон #67669887 ID = {0}", new_vag);
            if (new_vag != null)
            {
                RC_Vagons vag = new RC_Vagons();
                VAGONS vag_del = vag.DeleteVAGONS((int)new_vag);

            }
        }

        public void Test_References_OwnersContries() 
        { 
            References refer = new References(); 
            Console.WriteLine("Украина 1= {0}", refer.DefinitionIDOwnersContries(22));
            Console.WriteLine("Null = {0}", refer.DefinitionIDOwnersContries(0));
            //int? new_oc = refer.DefinitionIDOwnersContries(100);
            //Console.WriteLine("США ID = {0}", new_oc);
        }

        public void Test_References_Gruzs() 
        { 
            References refer = new References(); 


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
            kist.DayControllingAddNatur = 2;
                //kist.UpdateSostavs();
                Console.WriteLine("Обновлено {0}", kist.CopyArrivalSostavToRailway());
        }

        public void Test_KIS_RC_Transfer_SetListWagon() {


            Oracle_ArrivalSostav oras = oas.Get_ArrivalSostav(542);
            List<PromVagon> list_pv = pc.GetVagon(oras.NaturNum, oras.IDOrcStation, oras.Day, oras.Month, oras.Year, oras.Napr == 2 ? true : false).ToList();

            KIS_RC_Transfer kisrs = new KIS_RC_Transfer();
            int res = kisrs.SetListWagon(ref oras, list_pv);

            Console.WriteLine("Обновлено {0}", res);
        }

    }

}
