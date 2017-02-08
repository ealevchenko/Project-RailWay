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
        INumVagStpr1InStDocRepository rep_nvs1isd;
        INumVagStpr1InStVagRepository rep_nvs1isv;
        INumVagStpr1OutStDocRepository rep_nvs1osd;
        INumVagStpr1OutStVagRepository rep_nvs1osv;
        INumVagStpr1TupikRepository rep_nvs1t;


        public VagonsContent() 
        {
            this.rep_nvs = new EFNumVagStanRepository();
            this.rep_nvstpr = new EFNumVagStpr1GrRepository();
            this.rep_nvs1isd = new EFNumVagStpr1InStDocRepository();
            this.rep_nvs1isv = new EFNumVagStpr1InStVagRepository();
            this.rep_nvs1osd = new EFNumVagStpr1OutStDocRepository();
            this.rep_nvs1osv = new EFNumVagStpr1OutStVagRepository();
            this.rep_nvs1t = new EFNumVagStpr1TupikRepository();

        }

        public VagonsContent(INumVagStanRepository rep_nvs, 
            INumVagStpr1GrRepository rep_nvstpr, 
            INumVagStpr1InStDocRepository rep_nvs1isd,
            INumVagStpr1InStVagRepository rep_nvs1isv,
            INumVagStpr1OutStDocRepository rep_nvs1osd,
            INumVagStpr1OutStVagRepository rep_nvs1osv,
            INumVagStpr1TupikRepository rep_nvs1t) 
        {
            this.rep_nvs = rep_nvs;
            this.rep_nvstpr = rep_nvstpr;
            this.rep_nvs1isd = rep_nvs1isd;
            this.rep_nvs1isv = rep_nvs1isv;
            this.rep_nvs1osd = rep_nvs1osd;
            this.rep_nvs1osv = rep_nvs1osv;
            this.rep_nvs1t = rep_nvs1t;
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
        public IQueryable<NumVagStpr1InStDoc> GetSTPR1InStDoc() 
        {
            try
            {
                return rep_nvs1isd.NumVagStpr1InStDoc;
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
        public IQueryable<NumVagStpr1InStDoc> GetSTPR1InStDocOfAmkr(string where)
        {
            try
            {
                string sql = "select a.* from NUM_VAG.STPR1_IN_ST_DOC a inner join NUM_VAG.STAN b on a.ST_IN_ST=b.K_STAN and b.MPS=0 " + (!String.IsNullOrWhiteSpace(where) ? " WHERE " + where : "");
                return rep_nvs1isd.db.SqlQuery<NumVagStpr1InStDoc>(sql).AsQueryable();
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
        public IQueryable<NumVagStpr1InStDoc> GetSTPR1InStDocOfAmkr() 
        { return GetSTPR1InStDocOfAmkr(null); }
        /// <summary>
        /// Получить список вагонов перемещений по внутреним станциям по прибытию
        /// </summary>
        /// <returns></returns>
        public IQueryable<NumVagStpr1InStVag> GetSTPR1InStVag() 
        {
            try
            {
                return rep_nvs1isv.NumVagStpr1InStVag;
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
        public IQueryable<NumVagStpr1InStVag> GetSTPR1InStVag(int doc) 
        {
            return GetSTPR1InStVag().Where(v => v.ID_DOC == doc);
        }
        /// <summary>
        /// Получить список вагонов перемещений по внутреним станциям по прибытию по указаномку документу c сортировкой вагонов по порядку прибывания
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public IQueryable<NumVagStpr1InStVag> GetSTPR1InStVag(int doc, bool sort) 
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

        #region Составы по отправке
        /// <summary>
        /// Показать все составы
        /// </summary>
        /// <returns></returns>
        public IQueryable<NumVagStpr1OutStDoc> GetSTPR1OutStDoc()
        {
            try
            {
                return rep_nvs1osd.NumVagStpr1OutStDoc;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetSTPR1OutStDoc", eventID);
                return null;
            }
        }
        /// <summary>
        /// Показать все вагоны
        /// </summary>
        /// <returns></returns>
        public IQueryable<NumVagStpr1OutStVag> GetSTPR1OutStVag() 
        {
            try
            {
                return rep_nvs1osv.NumVagStpr1OutStVag;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetSTPR1OutStVag", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить список операций перемещений по отправке с других станций c дополнительной выборкой
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<NumVagStpr1OutStDoc> GetSTPR1OutStDocOfAmkr(string where)
        {
            try
            {
                string sql = "select a.* from NUM_VAG.STPR1_OUT_ST_DOC a inner join NUM_VAG.STAN b on a.st_out_st=b.K_STAN and b.MPS=0 " + (!String.IsNullOrWhiteSpace(where) ? " WHERE " + where : "");
                return rep_nvs1isd.db.SqlQuery<NumVagStpr1OutStDoc>(sql).AsQueryable();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetSTPR1OutStDocOfAmkr", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить список операций перемещений по отправке с других станций на станции АМКР
        /// </summary>
        /// <returns></returns>
        public IQueryable<NumVagStpr1OutStDoc> GetSTPR1OutStDocOfAmkr()
        { return GetSTPR1OutStDocOfAmkr(null); }
        /// <summary>
        /// Вернуть список вагонов по номеру документа
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public IQueryable<NumVagStpr1OutStVag> GetSTPR1OutStVag(int doc) 
        {
            return GetSTPR1OutStVag().Where(v => v.ID_DOC == doc);
        }
        /// <summary>
        /// Вернуть ко вагонов по номеру документа
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public int GetCountSTPR1OutStVag(int doc) 
        {
            return GetSTPR1InStVag(doc).Count();
        }

        #endregion

        #region Тупики
        /// <summary>
        /// Показать тупики
        /// </summary>
        /// <returns></returns>
        public IQueryable<NumVagStpr1Tupik> GetStpr1Tupik() 
        {
            try
            {
                return rep_nvs1t.NumVagStpr1Tupik;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetStpr1Tupik", eventID);
                return null;
            }
        }
        #endregion
    }
}
