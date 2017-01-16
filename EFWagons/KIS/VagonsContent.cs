using EFWagons.Abstarct;
using EFWagons.Concrete;
using EFWagons.Entities;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.KIS
{
    public class VagonsContent
    {
        private eventID eventID = eventID.EFWagons_KIS_VagonsContent;
        
        INumVagStanRepository rep_nvs;
        INumVagStpr1GrRepository rep_nvstpr;
        INumVagStanStpr1InStDocRepository rep_nvss1isd;

        public VagonsContent() 
        {
            this.rep_nvs = new EFNumVagStanRepository();
            this.rep_nvstpr = new EFNumVagStpr1GrRepository();
            this.rep_nvss1isd = new EFNumVagStanStpr1InStDocRepository();

        }

        public VagonsContent(INumVagStanRepository rep_nvs, INumVagStpr1GrRepository rep_nvstpr, INumVagStanStpr1InStDocRepository rep_nvss1isd) 
        {
            this.rep_nvs = rep_nvs;
            this.rep_nvstpr = rep_nvstpr;
            this.rep_nvss1isd = rep_nvss1isd;
        }

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

        public IQueryable<NumVagStanStpr1InStDoc> GetSTPR1InStDocOfAmkr() 
        { return GetSTPR1InStDocOfAmkr(null); }

    }
}
