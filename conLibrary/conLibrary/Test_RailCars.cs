using EFRailWay.Entities.Railcars;
using EFRailWay.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conLibrary
{
    public class Test_RailCars
    {
        RC_Shops shops = new RC_Shops();

        public Test_RailCars() 
        { 
        
        }

        #region RC_Vagons

        public void Test_RC_Vagons() 
        {
            RC_Vagons vag = new RC_Vagons();
            foreach (VAGONS t in vag.GetVagons())
            {
                WL(t);
            }
        }

        public void Test_RC_Vagons(int num_vag) 
        {
            RC_Vagons vag = new RC_Vagons();
            foreach (VAGONS t in vag.GetVagons(num_vag))
            {
                WL(t);
            }
        }

        public void Test_RC_Vagons(int num_vag, DateTime dt) 
        {
            RC_Vagons vag = new RC_Vagons();   
            WL(vag.GetVagons(num_vag, dt));

            int? res = vag.GetIDVagons(num_vag, dt);
            Console.WriteLine(" res id: {0}", res);
        }

        public void Test_SUD_RC_Vagons() 
        {
            RC_Vagons vag = new RC_Vagons();
            VAGONS w1 = new VAGONS()
            {
                id_vag = 0,
                num = 0,
                date_ar = DateTime.Now
            };
            int id_new = vag.SaveVAGONS(w1);
            Test_RC_Vagons(0);
            VAGONS w2 = new VAGONS()
            {
                id_vag = id_new,
                num = 1,
                date_ar = DateTime.Now
            };            
            int id_ch = vag.SaveVAGONS(w2);
            Test_RC_Vagons(1);
            VAGONS del = vag.DeleteVAGONS(id_ch);
            WL(del);
        }

        public void WL(VAGONS t) 
        { 
                Console.WriteLine("id_vag: {0}, num: {1}, id_ora: {2}, id_owner: {3}, id_stat: {4}, is_locom: {5}, locom_seria: {6}, rod: {7}, st_otpr: {8}, date_ar: {9}, :date_end {10}",
                    t.id_vag, t.num, t.id_ora, t.id_owner, t.id_stat, t.is_locom, t.locom_seria, t.rod, t.st_otpr, t.date_ar, t.date_end);        
        }
        #endregion

        #region RC_Owners

        public void Test_RC_Owners() 
        {
            RC_Owners ow = new RC_Owners();
            foreach (OWNERS t in ow.GetOwners())
            {
                WL(t);
            }
        }

        public void Test_RC_Owners_GetOwnersToKis(int id_sob_kis) 
        {
            RC_Owners ow = new RC_Owners();
            WL(ow.GetOwnersOfKis(id_sob_kis));
            Console.WriteLine("id : {0}", ow.GetIDOwnersOfKis(id_sob_kis));

        }

        public void Test_RC_Owners(int id_owner) 
        {
            RC_Owners ow = new RC_Owners();
            WL(ow.GetOwners(id_owner));
        }

        public void Test_SUD_RC_Owners() 
        {
            RC_Owners ow = new RC_Owners();            
            OWNERS ow1 = new OWNERS() 
            {
                id_owner = 0,
                name = "Тест",
                abr = "ТСТ",
                id_country = null,
                id_ora = null,
                id_ora_temp = null,
            };
            int res_ow1 = ow.SaveOwners(ow1);
            Test_RC_Owners(res_ow1); 
            OWNERS ow2 = new OWNERS() 
            {
                id_owner = res_ow1,
                name = "Тест1111",
                abr = "ТСТ111",
                id_country = null,
                id_ora = null,
                id_ora_temp = null,
            };
            int res_ow2 = ow.SaveOwners(ow2);
            Test_RC_Owners(res_ow2);
            OWNERS ow_del = ow.DeleteOwners(res_ow2);
            WL(ow_del);
        } 

        public void WL(OWNERS t) 
        { 
                Console.WriteLine("id_owner: {0}, name: {1}, abr: {2}, id_country: {3}, id_ora: {4}, id_ora_temp: {5}",
                    t.id_owner, t.name, t.abr, t.id_country, t.id_ora, t.id_ora_temp);   

        }
        #endregion

        #region RC_VagonsOperations
        public void Test_RC_VagonsOperations_DelseteIDSostav()
        {
            RC_VagonsOperations vo = new RC_VagonsOperations();
            Console.WriteLine("Количество строк: {0}",vo.DeleteVagonsToInsertMT(4231));
        }

        public void Test_RC_VagonsOperations()
        {
            RC_VagonsOperations vo = new RC_VagonsOperations();
            foreach (VAGON_OPERATIONS t in vo.GetVagonsOperations())
            {
                WL(t);
            }
        }

        public void Test_RC_VagonsOperations(int id_oper)
        {
            RC_VagonsOperations vo = new RC_VagonsOperations();
            WL(vo.GetVagonsOperations(id_oper));
        }

        public void Test_RC_VagonsOperationsToNatur(int natur, DateTime dt_amkr)
        {
            RC_VagonsOperations vo = new RC_VagonsOperations();
            int count = 0;
            foreach (VAGON_OPERATIONS t in vo.GetVagonsOperationsToNatur(natur, dt_amkr))
            {
                WL(t);
                count++;
            }
            Console.WriteLine("Количество строк: {0}",count);
        }

        public void Test_RC_VagonsOperationsToNatur(int natur, DateTime dt_amkr, int id_vagon)
        {
            RC_VagonsOperations vo = new RC_VagonsOperations();
            WL(vo.GetVagonsOperationsToNatur(natur,dt_amkr,id_vagon));
        }

        public void Test_RC_IsVagonOperation(int natur, DateTime dt_amkr, int id_vagon)
        {
            RC_VagonsOperations vo = new RC_VagonsOperations();
            Console.WriteLine("IsVagonOperation : {0}",vo.IsVagonOperationKIS(natur, dt_amkr, id_vagon));
        }

        public void Test_RC_MaxPositionWay(int id_way) 
        { 
            RC_VagonsOperations vo = new RC_VagonsOperations();
            Console.WriteLine("id_way : {0}", vo.MaxPositionWay(id_way));        
        }

        public void Test_RC_VagonsOperationsToInsertMT(int id_sostav)
        {
            RC_VagonsOperations vo = new RC_VagonsOperations();
            int count = 0;
            foreach (VAGON_OPERATIONS t in vo.GetVagonsOperationsToInsertMT(id_sostav))
            {
                WL(t);
                count++;
            }
            Console.WriteLine("Количество строк: {0}",count);
        }

        public void Test_SUD_RC_VagonsOperations()
        {
            RC_VagonsOperations vo = new RC_VagonsOperations();
            VAGON_OPERATIONS vo1 = new VAGON_OPERATIONS()
            {
                id_oper = 0,
                dt_amkr = DateTime.Now,
                n_natur = -777,
                id_vagon = 889213,
                id_stat = 4,
                dt_from_stat = null,
                dt_on_stat = DateTime.Now,
                id_way = 51,
                dt_from_way = null,
                dt_on_way = DateTime.Now,
                num_vag_on_way = 0,
                is_present = 0,
                id_locom = null,
                id_locom2 = null,
                id_cond2 = null,
                id_gruz = null,
                id_gruz_amkr = null,
                id_shop_gruz_for = null,
                weight_gruz = null,
                id_tupik = null,
                id_nazn_country = null,
                id_gdstait = null,
                id_cond = null,
                note = null,
                is_hist = 0,
                id_oracle = null,
                lock_id_way = null,
                lock_order = null,
                lock_side = null,
                lock_id_locom = null,
                st_lock_id_stat = null,
                st_lock_order = null,
                st_lock_train = null,
                st_lock_side = null,
                st_gruz_front = null,
                st_shop = null,
                oracle_k_st = null,
                st_lock_locom1 = null,
                st_lock_locom2 = null,
                id_oper_parent = null,
                grvu_SAP = null,
                ngru_SAP = null,
                id_ora_23_temp = null,
                edit_user = null,
                edit_dt = null,
                IDSostav = 0,
            };
            int res_ov1 = vo.SaveVagonsOperations(vo1);
            Test_RC_VagonsOperations(res_ov1);
            VAGON_OPERATIONS vo2 = new VAGON_OPERATIONS()
            {
                id_oper = res_ov1,
                dt_amkr = DateTime.Now,
                n_natur = -888,
                id_vagon = 889213,
                id_stat = 4,
                dt_from_stat = null,
                dt_on_stat = DateTime.Now,
                id_way = 51,
                dt_from_way = null,
                dt_on_way = DateTime.Now,
                num_vag_on_way = 0,
                is_present = 0,
                id_locom = null,
                id_locom2 = null,
                id_cond2 = null,
                id_gruz = null,
                id_gruz_amkr = null,
                id_shop_gruz_for = null,
                weight_gruz = null,
                id_tupik = null,
                id_nazn_country = null,
                id_gdstait = null,
                id_cond = null,
                note = null,
                is_hist = 0,
                id_oracle = null,
                lock_id_way = null,
                lock_order = null,
                lock_side = null,
                lock_id_locom = null,
                st_lock_id_stat = null,
                st_lock_order = null,
                st_lock_train = null,
                st_lock_side = null,
                st_gruz_front = null,
                st_shop = null,
                oracle_k_st = null,
                st_lock_locom1 = null,
                st_lock_locom2 = null,
                id_oper_parent = null,
                grvu_SAP = null,
                ngru_SAP = null,
                id_ora_23_temp = null,
                edit_user = null,
                edit_dt = null,
                IDSostav = 0,
            };
            int res_ov2 = vo.SaveVagonsOperations(vo2);
            Test_RC_VagonsOperations(res_ov2);

            VAGON_OPERATIONS del = vo.DeleteVagonsOperations(res_ov2);
            WL(del);
        }

        public void Test_DeleteVagonsToInsertMT()
        {
            RC_VagonsOperations vo = new RC_VagonsOperations();
            VAGON_OPERATIONS vo1 = new VAGON_OPERATIONS()
            {
                id_oper = 0,
                dt_amkr = DateTime.Now,
                n_natur = -777,
                id_vagon = 889213,
                id_stat = 4,
                dt_from_stat = null,
                dt_on_stat = DateTime.Now,
                id_way = 51,
                dt_from_way = null,
                dt_on_way = DateTime.Now,
                num_vag_on_way = 0,
                is_present = 0,
                id_locom = null,
                id_locom2 = null,
                id_cond2 = null,
                id_gruz = null,
                id_gruz_amkr = null,
                id_shop_gruz_for = null,
                weight_gruz = null,
                id_tupik = null,
                id_nazn_country = null,
                id_gdstait = null,
                id_cond = null,
                note = null,
                is_hist = 0,
                id_oracle = null,
                lock_id_way = null,
                lock_order = null,
                lock_side = null,
                lock_id_locom = null,
                st_lock_id_stat = null,
                st_lock_order = null,
                st_lock_train = null,
                st_lock_side = null,
                st_gruz_front = null,
                st_shop = null,
                oracle_k_st = null,
                st_lock_locom1 = null,
                st_lock_locom2 = null,
                id_oper_parent = null,
                grvu_SAP = null,
                ngru_SAP = null,
                id_ora_23_temp = null,
                edit_user = null,
                edit_dt = null,
                IDSostav = 0,
            };
            int res_ov1 = vo.SaveVagonsOperations(vo1);
            Test_RC_VagonsOperations(res_ov1);
            VAGON_OPERATIONS vo2 = new VAGON_OPERATIONS()
            {
                id_oper = 0,
                dt_amkr = DateTime.Now,
                n_natur = -888,
                id_vagon = 889213,
                id_stat = 4,
                dt_from_stat = null,
                dt_on_stat = DateTime.Now,
                id_way = 51,
                dt_from_way = null,
                dt_on_way = DateTime.Now,
                num_vag_on_way = 0,
                is_present = 0,
                id_locom = null,
                id_locom2 = null,
                id_cond2 = null,
                id_gruz = null,
                id_gruz_amkr = null,
                id_shop_gruz_for = null,
                weight_gruz = null,
                id_tupik = null,
                id_nazn_country = null,
                id_gdstait = null,
                id_cond = null,
                note = null,
                is_hist = 0,
                id_oracle = null,
                lock_id_way = null,
                lock_order = null,
                lock_side = null,
                lock_id_locom = null,
                st_lock_id_stat = null,
                st_lock_order = null,
                st_lock_train = null,
                st_lock_side = null,
                st_gruz_front = null,
                st_shop = null,
                oracle_k_st = null,
                st_lock_locom1 = null,
                st_lock_locom2 = null,
                id_oper_parent = null,
                grvu_SAP = null,
                ngru_SAP = null,
                id_ora_23_temp = null,
                edit_user = null,
                edit_dt = null,
                IDSostav = 0,
            };
            int res_ov2 = vo.SaveVagonsOperations(vo2);
            Test_RC_VagonsOperations(res_ov2);

            Console.WriteLine("Удалено : {0}",vo.DeleteVagonsToInsertMT(0));
        }

        public void Test_DeleteVagonsToNaturList()
        {
            RC_VagonsOperations vo = new RC_VagonsOperations();
            int nat = -777;
            DateTime dt_amkr = DateTime.Now;
            VAGON_OPERATIONS vo1 = new VAGON_OPERATIONS()
            {
                id_oper = 0,
                dt_amkr = dt_amkr,
                n_natur = nat,
                id_vagon = 889213,
                id_stat = 4,
                dt_from_stat = null,
                dt_on_stat = DateTime.Now,
                id_way = 51,
                dt_from_way = null,
                dt_on_way = DateTime.Now,
                num_vag_on_way = 0,
                is_present = 0,
                id_locom = null,
                id_locom2 = null,
                id_cond2 = null,
                id_gruz = null,
                id_gruz_amkr = null,
                id_shop_gruz_for = null,
                weight_gruz = null,
                id_tupik = null,
                id_nazn_country = null,
                id_gdstait = null,
                id_cond = null,
                note = null,
                is_hist = 0,
                id_oracle = null,
                lock_id_way = null,
                lock_order = null,
                lock_side = null,
                lock_id_locom = null,
                st_lock_id_stat = null,
                st_lock_order = null,
                st_lock_train = null,
                st_lock_side = null,
                st_gruz_front = null,
                st_shop = null,
                oracle_k_st = null,
                st_lock_locom1 = null,
                st_lock_locom2 = null,
                id_oper_parent = null,
                grvu_SAP = null,
                ngru_SAP = null,
                id_ora_23_temp = null,
                edit_user = null,
                edit_dt = null,
                IDSostav = 0,
            };
            int res_ov1 = vo.SaveVagonsOperations(vo1);
            Test_RC_VagonsOperations(res_ov1);

            Console.WriteLine("Удалено : {0}", vo.DeleteVagonsToNaturList(nat, dt_amkr));
        }

        public void Test_InsertVagon() 
        {
            //RC_VagonsOperations vo = new RC_VagonsOperations();
            //int ivag = vo.InsertVagon(-777, DateTime.Now, 238191,33, 4, 51, 0);
            //Console.WriteLine("id_oper нового вагона: {0}", ivag);
            //VAGON_OPERATIONS del = vo.DeleteVagonsOperations(ivag);
            //WL(del);
        }

        public void WL(VAGON_OPERATIONS t) 
        { 
                Console.WriteLine("id_oper: {0}",t.id_oper);
                Console.WriteLine("dt_amkr: {0}",t.dt_amkr);
                Console.WriteLine("n_natur: {0}",t.n_natur);
                Console.WriteLine("id_vagon: {0}",t.id_vagon);
                Console.WriteLine("id_stat: {0}",t.id_stat);
                Console.WriteLine("dt_from_stat: {0}",t.dt_from_stat);
                Console.WriteLine("dt_on_stat: {0}",t.dt_on_stat);
                Console.WriteLine("id_way: {0}",t.id_way);
                Console.WriteLine("dt_from_way: {0}",t.dt_from_way);
                Console.WriteLine("dt_on_way: {0}",t.dt_on_way);
                Console.WriteLine("num_vag_on_way: {0}",t.num_vag_on_way);
                Console.WriteLine("is_present: {0}",t.is_present);
                Console.WriteLine("id_locom: {0}",t.id_locom);
                Console.WriteLine("id_locom2: {0}",t.id_locom2);
                Console.WriteLine("id_cond2: {0}",t.id_cond2);
                Console.WriteLine("id_gruz: {0}",t.id_gruz);
                Console.WriteLine("id_gruz_amkr: {0}",t.id_gruz_amkr);
                Console.WriteLine("id_shop_gruz_for: {0}",t.id_shop_gruz_for);
                Console.WriteLine("weight_gruz: {0}",t.weight_gruz);
                Console.WriteLine("id_tupik: {0}",t.id_tupik);
                Console.WriteLine("id_nazn_country: {0}",t.id_nazn_country);
                Console.WriteLine("id_gdstait: {0}",t.id_gdstait);
                Console.WriteLine("id_cond: {0}",t.id_cond);
                Console.WriteLine("note: {0}",t.note);
                Console.WriteLine("is_hist: {0}",t.is_hist);
                Console.WriteLine("id_oracle: {0}",t.id_oracle);
                Console.WriteLine("lock_id_way: {0}",t.lock_id_way);
                Console.WriteLine("lock_order: {0}",t.lock_order);
                Console.WriteLine("lock_side: {0}",t.lock_side);
                Console.WriteLine("lock_id_locom: {0}",t.lock_id_locom);
                Console.WriteLine("st_lock_id_stat: {0}",t.st_lock_id_stat);
                Console.WriteLine("st_lock_order: {0}",t.st_lock_order);
                Console.WriteLine("st_lock_train: {0}",t.st_lock_train);
                Console.WriteLine("st_lock_side: {0}",t.st_lock_side);
                Console.WriteLine("st_gruz_front: {0}",t.st_gruz_front);
                Console.WriteLine("st_shop: {0}",t.st_shop);
                Console.WriteLine("oracle_k_st: {0}",t.oracle_k_st);
                Console.WriteLine("st_lock_locom1: {0}",t.st_lock_locom1);
                Console.WriteLine("st_lock_locom2: {0}",t.st_lock_locom2);
                Console.WriteLine("id_oper_parent: {0}",t.id_oper_parent);
                Console.WriteLine("grvu_SAP: {0}",t.grvu_SAP);
                Console.WriteLine("ngru_SAP: {0}",t.ngru_SAP);
                Console.WriteLine("id_ora_23_temp: {0}",t.id_ora_23_temp);
                Console.WriteLine("edit_user: {0}",t.edit_user);
                Console.WriteLine("edit_dt: {0}",t.edit_dt);
                Console.WriteLine("IDSostav: {0}",t.IDSostav);
                Console.WriteLine("num_vagon: {0}", t.num_vagon);
        }
        #endregion

        #region RC_Ways

        public void Test_RC_Ways() 
        {
            RC_Ways ws = new RC_Ways();
            foreach (WAYS t in ws.GetWays())
            {
                WL(t);
            }
        }

        public void Test_RC_Ways(int id_way) 
        {
            RC_Ways ws = new RC_Ways();
            WAYS way = ws.GetWays(id_way);
            WL(way);
        }

        public void Test_RC_WaysToStations(int id_station) 
        {
            RC_Ways ws = new RC_Ways();
            IQueryable<WAYS> list = ws.GetWaysOfStations(id_station);
            foreach (WAYS t in list)
            {
                WL(t);
            }
            Console.WriteLine(" количество: {0}", list.Count());
        }

        public void Test_RC_WaysToStations(int id_station, string num) 
        {
            RC_Ways ws = new RC_Ways();
            WAYS w = ws.GetWaysOfStations(id_station, num);
            int? nw = ws.GetIDWaysToStations(id_station, num);
            WL(w);
            Console.WriteLine(" id: {0}", nw);
        }

        public void Test_SUD_RC_Ways()
        {
            RC_Ways ws = new RC_Ways();
            WAYS w1 = new WAYS()
            {
                id_way = 0,
                id_stat = 4,
                id_park = null,
                num = "0",
                name = "Тест",
                vag_capacity =7,
                order = null,
                bind_id_cond = null,
                for_rospusk = null,
            };
            int id_new = ws.SaveWays(w1);
            Test_RC_Ways(id_new);
            WAYS w2 = new WAYS()
            {
                id_way = id_new,
                id_stat = 4,
                id_park = null,
                num = "0",
                name = "Тест11111",
                vag_capacity =9,
                order = null,
                bind_id_cond = null,
                for_rospusk = null,
            };
            int id_ch = ws.SaveWays(w2);
            Test_RC_Ways(id_ch);
            WAYS del = ws.DeleteWays(id_ch);
            WL(del);
        }

        public void WL(WAYS t) 
        { 
                Console.WriteLine("id_way: {0}, id_stat: {1}, id_park: {2}, num: {3}, name: {4}, vag_capacity: {5}, order: {6}, bind_id_cond: {7}, for_rospusk: {8}",
                    t.id_way, t.id_stat, t.id_park, t.num, t.name, t.vag_capacity, t.order, t.bind_id_cond, t.for_rospusk);      
        }
        #endregion

        #region RC_OwnersContries

        public void Test_RC_OwnersContries() 
        {
            RC_OwnersContries oс = new RC_OwnersContries();
            foreach (OWNERS_COUNTRIES t in oс.GetOwnersContries())
            {
                WL(t);
            }
        }

        public void Test_RC_OwnersContries(int id_own_country) 
        {
            RC_OwnersContries oс = new RC_OwnersContries();
                WL(oс.GetOwnersContries(id_own_country));
        }

        public void Test_RC_OwnersContriesToKis(int id_ora) 
        {
            RC_OwnersContries oс = new RC_OwnersContries();
            WL(oс.GetOwnersContriesOfKis(id_ora));
        }       

        public void WL(OWNERS_COUNTRIES t) 
        { 
                Console.WriteLine("id_own_country: {0}, name: {1}, id_ora: {2}",
                    t.id_own_country, t.name, t.id_ora);   
        }
        #endregion

        #region RC_Gruzs

        public void Test_RC_Gruzs() 
        {
            RC_Gruzs gruz = new RC_Gruzs();
            foreach (GRUZS t in gruz.GetGruzs())
            {
                WL(t);
            }
        }
        public void Test_RC_Gruzs(int id_gruz) 
        {
            RC_Gruzs gruz = new RC_Gruzs();

            WL(gruz.GetGruzs(id_gruz));
        }

        public void Test_RC_Gruzs(int? id_gruz_prom_kis, int? id_gruz_vag_kis) 
        {
            RC_Gruzs gruz = new RC_Gruzs();

                WL(gruz.GetGruzs(id_gruz_prom_kis,id_gruz_vag_kis));
        }

        public void Test_SUD_RC_Gruzs()
        {
            RC_Gruzs gruz = new RC_Gruzs();
            GRUZS w1 = new GRUZS()
            {
                 id_gruz=0, name="тест1", name_full="full_test1", id_ora = 0, id_ora2=0,
            };
            int id_new = gruz.SaveGruzs(w1);
            Test_RC_Gruzs(id_new);
            GRUZS w2 = new GRUZS()
            {
                id_gruz = id_new,
                name = "тест2",
                name_full = "full_test2",
                id_ora = null,
                id_ora2 = null,
            };
            int id_ch = gruz.SaveGruzs(w2);
            Test_RC_Gruzs(id_ch);
            GRUZS del = gruz.DeleteGruzs(id_ch);
            WL(del);
        }
        //public void Test_RC_OwnersContries(int id_own_country) 
        //{
        //    RC_OwnersContries oс = new RC_OwnersContries();
        //        WL(oс.GetOwnersContries(id_own_country));
        //}

        //public void Test_RC_OwnersContriesToKis(int id_ora) 
        //{
        //    RC_OwnersContries oс = new RC_OwnersContries();
        //    WL(oс.GetOwnersContriesToKis(id_ora));
        //}       

        public void WL(GRUZS t) 
        {
            if (t != null)
            {
                Console.WriteLine("id_gruz: {0}, name: {1}, name_full: {2}, id_ora: {3}, id_ora2: {4}",
                        t.id_gruz, t.name, t.name_full, t.id_ora, t.id_ora2);
            }
            else { Console.WriteLine("= NULL"); }
        }
        #endregion

        #region RC_Shops

        public void Test_RC_Shops() 
        {
            foreach (SHOPS t in shops.GetShops())
            {
                WL(t);
            }
        }

        public void Test_RC_Shops(int id_shop)
        {
            WL(shops.GetShops(id_shop));
        }

        public void Test_RC_ShopsOfKis(int id_shop_kis)
        {
            WL(shops.GetShopsOfKis(id_shop_kis));
        }

        public void Test_SUD_RC_Shops()
        {
            SHOPS s1 = new SHOPS()
            {
                id_shop = 0,
                name = "Тест",
                name_full = "Тест_name_full",
                id_stat = null,
                id_ora = 0,
            };
            int id_new = shops.SaveShop(s1);
            Test_RC_Shops(id_new);
            SHOPS s2 = new SHOPS()
            {
                id_shop = id_new,
                name = "Тест1",
                name_full = "Тест_name_full1",
                id_stat = null,
                id_ora = 0,
            };
            int id_ch = shops.SaveShop(s2);
            Test_RC_Shops(id_ch);
            SHOPS del = shops.DeleteShops(id_ch);
            WL(del);
        }

        public void WL(SHOPS t) 
        {
            if (t != null)
            {
                Console.WriteLine("id_shop: {0}, name: {1}, name_full: {2}, id_stat: {3}, id_ora: {4}",
                        t.id_shop, t.name, t.name_full, t.id_stat, t.id_ora);
            }
            else { Console.WriteLine("= NULL"); }
        }
        #endregion
    }
}
