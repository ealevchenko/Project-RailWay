using EFWagons.Abstarct;
using EFWagons.Concrete;
using EFWagons.Entities;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Statics
{
    public class VagonsContent
    {
        private eventID eventID = eventID.EFWagons_KIS_VagonsContent;
        
        INumVagStanRepository rep_nvs;
        INumVagStpr1GrRepository rep_nvstpr;
        INumVagStanStpr1InStDocRepository rep_nvss1isd;
        INumVagStanStpr1InStVagRepository rep_nvss1isv;

        public VagonsContent() 
        {
            this.rep_nvs = new EFNumVagStanRepository();
            this.rep_nvstpr = new EFNumVagStpr1GrRepository();
            this.rep_nvss1isd = new EFNumVagStanStpr1InStDocRepository();
            this.rep_nvss1isv = new EFNumVagStanStpr1InStVagRepository();
        }

        public VagonsContent(INumVagStanRepository rep_nvs, INumVagStpr1GrRepository rep_nvstpr, INumVagStanStpr1InStDocRepository rep_nvss1isd, INumVagStanStpr1InStVagRepository rep_nvss1isv) 
        {
            this.rep_nvs = rep_nvs;
            this.rep_nvstpr = rep_nvstpr;
            this.rep_nvss1isd = rep_nvss1isd;
            this.rep_nvss1isv = rep_nvss1isv;
        }

        #region Станции
        /// <summary>
        /// Получить все станции КИС
        /// </summary>
        /// <returns></returns>
        public IQueryable<NumVagStan> GetStations() 
        {
            return rep_nvs.NumVagStan;
        }
        /// <summary>
        /// Получить информацию по станции по  id
        /// </summary>
        /// <param name="id_stan"></param>
        /// <returns></returns>
        public NumVagStan GetStations(int id_stan) 
        {
            return GetStations().Where(s => s.K_STAN == id_stan).FirstOrDefault();
        }
        #endregion

        #region Грузы
        /// <summary>
        /// список грузов
        /// </summary>
        /// <returns></returns>
        public IQueryable<NumVagStpr1Gr> GetSTPR1GR() 
        {
            return rep_nvstpr.NumVagStpr1Gr;
        }
        /// <summary>
        /// Получить груз по kod_gr
        /// </summary>
        /// <param name="kod_gr"></param>
        /// <returns></returns>
        public NumVagStpr1Gr GetSTPR1GR(int kod_gr) 
        {
            return GetSTPR1GR().Where(g=>g.KOD_GR==kod_gr).FirstOrDefault();
        }
        #endregion

        #region Составы по прибытию
        /// <summary>
        /// Получить список операций перемещений по прибытию с других станций
        /// </summary>
        /// <returns></returns>
        public IQueryable<NumVagStanStpr1InStDoc> GetSTPR1InStDoc() 
        {
            try
            {
                return rep_nvss1isd.NumVagStanStpr1InStDoc;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetSTPR1InStDoc", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить список операций перемещений по прибытию с других станций c дополнительной выборкой
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<NumVagStanStpr1InStDoc> GetSTPR1InStDocOfAmkr(string where)
        {
            try
            {
                string sql = "select a.* from NUM_VAG.STPR1_IN_ST_DOC a inner join NUM_VAG.STAN b on a.ST_IN_ST=b.K_STAN and b.MPS=0 " + (!String.IsNullOrWhiteSpace(where) ? " WHERE " + where : "");
                return rep_nvss1isd.db.SqlQuery<NumVagStanStpr1InStDoc>(sql).AsQueryable();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetSTPR1InStDocOfAmkr", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить список операций перемещений по прибытию с других станций на станции АМКР
        /// </summary>
        /// <returns></returns>
        public IQueryable<NumVagStanStpr1InStDoc> GetSTPR1InStDocOfAmkr() 
        { return GetSTPR1InStDocOfAmkr(null); }
        /// <summary>
        /// Получить список вагонов перемещений по внутреним станциям по прибытию
        /// </summary>
        /// <returns></returns>
        public IQueryable<NumVagStanStpr1InStVag> GetSTPR1InStVag() 
        {
            try
            {
                return rep_nvss1isv.NumVagStanStpr1InStVag;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetSTPR1InStVag", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить список вагонов перемещений по внутреним станциям по прибытию по указаномку документу
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public IQueryable<NumVagStanStpr1InStVag> GetSTPR1InStVag(int doc) 
        {
            return GetSTPR1InStVag().Where(v => v.ID_DOC == doc);
        }
        /// <summary>
        /// Получить список вагонов перемещений по внутреним станциям по прибытию по указаномку документу c сортировкой вагонов по порядку прибывания
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public IQueryable<NumVagStanStpr1InStVag> GetSTPR1InStVag(int doc, bool sort) 
        {
            if (sort) { return GetSTPR1InStVag(doc).OrderByDescending(v => v.N_IN_ST); }
            else { return GetSTPR1InStVag(doc).OrderBy(v => v.N_IN_ST);}
        }

        /// <summary>
        /// Получить количество вагонов
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public int GetCountSTPR1InStVag(int doc) 
        {
            return GetSTPR1InStVag(doc).Count();
        }
        #endregion
    }
}
