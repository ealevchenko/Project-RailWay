using EFRailWay.KIS;
using EFRailWay.Statics;
using EFWagons.Entities;
using EFWagons.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace conLibrary
{
    public class Test_Wagons
    {
        PromContent promsostav = new PromContent();
        VagonsContent vc = new VagonsContent();

        public Test_Wagons() 
        { 
        
        }

        public void Test_KometaContent_KometaVagonSob()
        {
            KometaContent kom_con = new KometaContent();
            foreach (KometaVagonSob t in kom_con.GetVagonsSob())
            {
                WL(t);
            }
        }

        public void Test_KometaContent_KometaVagonSob(int num)
        {
            KometaContent kom_con = new KometaContent();
            foreach (KometaVagonSob t in kom_con.GetVagonsSob(num))
            {
                WL(t);
            }
        }

        public void Test_KometaContent_KometaVagonSob(int num, DateTime dt)
        {
            KometaContent kom_con = new KometaContent();
            WL(kom_con.GetVagonsSob(num, dt));
        }

        public void WL(KometaVagonSob t) 
        { 
                Console.WriteLine("N_VAGON: {0},\t SOB: {1},\t DATE_AR: {2},\t DATE_END: {3},\t ROD: {4},\t DATE_REM: {5},\t PRIM: {6},\t CODE: {7}",
                    t.N_VAGON,t.SOB,t.DATE_AR, t.DATE_END, t.ROD,t.DATE_REM, t.PRIM,t.CODE);        
        }

        public void Test_KometaContent_KometaSobstvForNakl()
        {
            KometaContent kom_con = new KometaContent();
            foreach (KometaSobstvForNakl t in kom_con.GetSobstvForNakl())
            {
                WL(t);
            }
        }

        public void Test_KometaContent_KometaSobstvForNakl(int sob)
        {
            KometaContent kom_con = new KometaContent();            
            WL(kom_con.GetSobstvForNakl(sob));
        }
        public void WL(KometaSobstvForNakl t) 
        { 
                Console.WriteLine("NPLAT: {0},\t SOBSTV: {1},\t ABR: {2},\t SOD_PLAT: {3},\t ID: {4},\t ID2: {5}",
                    t.NPLAT,t.SOBSTV,t.ABR, t.SOD_PLAT, t.ID,t.ID2);        
        }

        public void Test_KometaContent_GetKometaStrana()
        {
            KometaContent kom_con = new KometaContent();
            foreach (KometaStrana t in kom_con.GetKometaStrana())
            {
                WL(t);
            }
        }

        public void Test_KometaContent_GetKometaStrana(int kod_stran)
        {
            KometaContent kom_con = new KometaContent();
                WL(kom_con.GetKometaStrana(kod_stran));
        }
        public void WL(KometaStrana t) 
        {
            if (t == null) { Console.WriteLine(" = Null"); return; }
            Console.WriteLine("KOD_STRAN: {0},\t NAME: {1},\t ABREV_STRAN: {2}",
                    t.KOD_STRAN,t.NAME,t.ABREV_STRAN);        
        }

        public void Test_PromContent_GetGruzSP()
        {
            foreach (PromGruzSP t in promsostav.GetGruzSP())
            {
                WL(t);
            }
        }

        public void Test_PromContent_GetGruzSP(int k_gruz)
        {
                WL(promsostav.GetGruzSP(k_gruz));
        }

        public void Test_PromContent_GetGruzSPToTarGR(int? tar_gr, bool corect)
        {
            WL(promsostav.GetGruzSPToTarGR(tar_gr, corect));
        }

        public void WL(PromGruzSP t) 
        {
            if (t == null) { Console.WriteLine(" = Null"); return; }
            Console.WriteLine("K_GRUZ: {0},\t NAME_GR: {1},\t ABREV_GR: {2},\t GRUP_P: {3},\t NGRUP_P: {4},\t GRUP_O: {5},\t GROUP_OSV: {6},\t NGRUP_O: {7},\t TAR_GR: {8},\t KOD1: {9},\t KOD2: {10},\t K_GR: {11}",
                    t.K_GRUZ,t.NAME_GR,t.ABREV_GR,t.GRUP_P,t.NGRUP_P,t.GRUP_O,t.GROUP_OSV,t.NGRUP_O,t.TAR_GR,t.KOD1,t.KOD2,t.K_GR);        
        }

        public void Test_VagonsContent_GetSTPR1GR()
        {
            foreach (NumVagStpr1Gr t in vc.GetSTPR1GR())
            {
                WL(t);
            }
        }

        public void Test_VagonsContent_GetSTPR1GR(int kod_gr)
        {
            WL(vc.GetSTPR1GR(kod_gr));
        }
        public void WL(NumVagStpr1Gr t) 
        {
            if (t == null) { Console.WriteLine(" = Null"); return; }
            Console.WriteLine("KOD_GR: {0},\t GR: {1},\t OLD: {2}",
                    t.KOD_GR,t.GR,t.OLD);        
        }

        public void Test_PromContent_GetNatHist()
        {
            foreach (PromNatHist t in promsostav.GetNatHist())
            {
                WL(t);
            }
        }

        public void Test_PromContent_GetNatHist(int natur, int station, int day, int month, int year, int wag)
        {
            WL(promsostav.GetNatHist(natur, station, day, month, year, wag));
        }
        public void WL(PromNatHist t) 
        {
            if (t == null) { Console.WriteLine(" = Null"); return; }
            Console.WriteLine("N_NATUR: {0},\t D_PR_DD: {1},\t D_PR_MM: {2},\t D_PR_YY: {3},\t T_PR_HH: {4},\t T_PR_MI: {5},\t N_VAG: {6},\t NPP: {7},\t GODN: {8},\t K_ST: {9},\t N_VED_PR: {10},\t N_NAK_MPS: {11},\t N_NAK_KMK: {12},\t WES_GR: {13},\t K_OP: {14},\t K_FRONT: {15},\t KOD_STRAN: {16},\t DAT_VVOD: {17}",
                    t.N_NATUR,t.D_PR_DD,t.D_PR_MM,t.D_PR_YY,t.T_PR_HH,t.T_PR_MI,t.N_VAG,t.NPP,t.GODN,t.K_ST,t.N_VED_PR,t.N_NAK_MPS,t.N_NAK_KMK,t.WES_GR,t.K_OP,t.K_FRONT,t.KOD_STRAN,t.DAT_VVOD);        
        }

        public void Test_PromContent_GetCex()
        {
            foreach (PromCex t in promsostav.GetCex())
            {
                WL(t);
            }
        }

        public void Test_PromContent_GetCex(int k_podr)
        {
            WL(promsostav.GetCex(k_podr));
        }

        public void WL(PromCex t) 
        {
            if (t == null) { Console.WriteLine(" = Null"); return; }
            Console.WriteLine("K_PODR: {0},\t NAME_P: {1},\t ABREV_P: {2}",
                    t.K_PODR,t.NAME_P,t.ABREV_P);        
        }

        #region NumVagStanStpr1InStDoc & NumVagStanStpr1InStVag

        public void Test_VagonsContent_GetSTPR1InStDoc()
        {
            foreach (NumVagStpr1InStDoc t in vc.GetSTPR1InStDoc())
            {
                WL(t);
            }
        }

        public void Test_VagonsContent_GetSTPR1InStDocOfAmkr()
        {
            foreach (NumVagStpr1InStDoc t in vc.GetSTPR1InStDocOfAmkr())
            {
                WL(t);
            }
        }

        public void Test_VagonsContent_GetSTPR1InStDocOfAmkrWhere()
        {
            RulesCopy rc = new RulesCopy();
            List<OracleRules> list = rc.GetRulesCopyToOracleRulesOfKis(typeOracleRules.Input);
            string wh = "";
            DateTime dt = DateTime.Now.AddDays(-2);
            foreach (NumVagStpr1InStDoc t in vc.GetSTPR1InStDocOfAmkr(wh.ConvertWhere(list, "a.k_stan", "st_in_st ", "OR")).Where(v=>v.DATE_IN_ST>dt))
            {
                WL(t);
            }
        }
        public void WL(NumVagStpr1InStDoc t) 
        {
            if (t == null) { Console.WriteLine(" = Null"); return; }
            Console.WriteLine("ID_DOC: {0},\t DATE_IN_ST: {1},\t ST_IN_ST: {2},\t N_PUT_IN_ST: {3},\t NAPR_IN_ST: {4},\t FIO_IN_ST: {5},\t CEX: {6},\t N_POST: {7},\t K_STAN: {8},\t OLD_N_NATUR: {9}",
                    t.ID_DOC,t.DATE_IN_ST,t.ST_IN_ST,t.N_PUT_IN_ST,t.NAPR_IN_ST,t.FIO_IN_ST,t.CEX,t.N_POST,t.K_STAN, t.OLD_N_NATUR);        
        }

        public void Test_VagonsContent_GetSTPR1InStVag()
        {
            foreach (NumVagStpr1InStVag t in vc.GetSTPR1InStVag())
            {
                WL(t);
            }
        }
        public void Test_VagonsContent_GetCountSTPR1InStVag()
        {
            Console.WriteLine("57 =  {0}",vc.GetCountSTPR1InStVag(227028));
            Console.WriteLine("0 =  {0}",vc.GetCountSTPR1InStVag(0));
        }
        public void WL(NumVagStpr1InStVag t) 
        {
            if (t == null) { Console.WriteLine(" = Null"); return; }
            Console.WriteLine("ID_DOC: {0},\t N_IN_ST: {1},\t N_VAG: {2},\t STRAN_SOBSTV: {3},\t GODN_IN_ST: {4},\t GR_IN_ST: {5},\t SOBSTV: {6},\t REM_IN_ST: {7},\t ID_VAG: {8},\t ST_NAZN_OUT_ST: {9},\t STRAN_OUT_ST: {10},\t SOBSTV_OLD: {11}",
                    t.ID_DOC,t.N_IN_ST,t.N_VAG,t.STRAN_SOBSTV,t.GODN_IN_ST,t.GR_IN_ST,t.SOBSTV,t.REM_IN_ST,t.ID_VAG, t.ST_NAZN_OUT_ST, t.STRAN_OUT_ST, t.SOBSTV_OLD);        
        }

        #endregion


        public void Test_PromContent_TestFilter()
        {
            
            List<PromSostav> list = promsostav.GetInputPromSostav(DateTime.Now.AddDays(-1), DateTime.Now).ToList();
            List<PromNatHist> list_pnh = promsostav.GetNatHistOfVagonLess(60226420, DateTime.Parse("2017-01-18 05:00:00"), true).ToList();

        }


        #region NumVagStanStpr1OutStDoc & NumVagStpr1OutStVag
        /// <summary>
        /// Получить все составы по отправке
        /// </summary>
        public void Test_VagonsContent_GetSTPR1OutStDoc()
        {
            foreach (NumVagStpr1OutStDoc t in vc.GetSTPR1OutStDoc())
            {
                WL(t);
            }
        }

        public void WL(NumVagStpr1OutStDoc t)
        {
            if (t == null) { Console.WriteLine(" = Null"); return; }
            Console.WriteLine("ID_DOC: {0},\t DATE_OUT_ST: {1},\t ST_OUT_ST: {2},\t N_PUT_OUT_ST: {3},\t NAPR_OUT_ST: {4},\t FIO_IN_ST: {5},\t CEX: {6},\t K_STAN: {7},\t DATE_ST: {8},\t STATUS: {9},\t NAME_ST: {10}",
                    t.ID_DOC, t.DATE_OUT_ST, t.ST_OUT_ST, t.N_PUT_OUT_ST, t.NAPR_OUT_ST, t.FIO_IN_ST, t.CEX, t.K_STAN ,t.DATE_ST ,t.STATUS ,t.NAME_ST);
        }
        /// <summary>
        /// Получить все вагоны по отправке
        /// </summary>
        public void Test_VagonsContent_GetSTPR1OutStVag()
        {
            foreach (NumVagStpr1OutStVag t in vc.GetSTPR1OutStVag())
            {
                WL(t);
            }
        }

        public void WL(NumVagStpr1OutStVag t)
        {
            if (t == null) { Console.WriteLine(" = Null"); return; }
            Console.WriteLine("ID_DOC: {0},\t N_OUT_ST: {1},\t GR_OUT_ST: {2},\t SOBSTV: {3},\t REM_IN_ST: {4},\t ID_VAG: {5},\t ST_NAZN_OUT_ST: {6},\t STRAN_OUT_ST: {7},\t SOBSTV_OLD: {8},\t N_TUP_OUT_ST: {9}",
                    t.ID_DOC, 
                    t.N_OUT_ST, 
                    t.GR_OUT_ST, 
                    t.SOBSTV, 
                    t.REM_IN_ST, 
                    t.ID_VAG, 
                    t.ST_NAZN_OUT_ST, 
                    t.STRAN_OUT_ST, 
                    t.SOBSTV_OLD,
                    t.N_TUP_OUT_ST);
        }

        #endregion

        #region NumVagStpr1Tupik
        /// <summary>
        /// Список тупиков
        /// </summary>
        public void Test_VagonsContent_GetStpr1Tupik()
        {
            foreach (NumVagStpr1Tupik t in vc.GetStpr1Tupik())
            {
                WL(t);
            }
        }

        public void WL(NumVagStpr1Tupik t) 
        {
            if (t == null) { Console.WriteLine(" = Null"); return; }
            Console.WriteLine("ID_CEX: {0},\t ID_CEX_TUPIK: {1},\t NAMETUPIK: {2}",
                    t.ID_CEX,t.ID_CEX_TUPIK,t.NAMETUPIK);        
        }
        #endregion

        #region NumVagStran
        /// <summary>
        /// Список стран
        /// </summary>
        public void Test_VagonsContent_GetStran()
        {
            foreach (NumVagStran t in vc.GetStran())
            {
                WL(t);
            }
        }

        public void Test_VagonsContent_GetStranOf()
        {
            WL(vc.GetStran(16));
            WL(vc.GetStranOfCodeEurope(8405));
            WL(vc.GetStranOfCodeStarn(840));
        }


        public void WL(NumVagStran t) 
        {
            if (t == null) { Console.WriteLine(" = Null"); return; }
            Console.WriteLine("NPP: {0},\t NAME: {1},\t ABREV_STRAN: {2},\t KOD_EUROP: {3},\t KOD_STRAN: {4},\t KOD_OLD: {5}",
                    t.NPP,t.NAME,t.ABREV_STRAN,t.KOD_EUROP,t.KOD_STRAN,t.KOD_OLD);        
        }
        #endregion

    }
}
