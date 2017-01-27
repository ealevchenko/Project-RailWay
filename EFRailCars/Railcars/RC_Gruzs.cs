using EFRailCars.Abstract;
using EFRailCars.Concrete;
using EFRailCars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Railcars
{
    public class RC_Gruzs
    {
        IGruzsRepository rep_gr;

        public RC_Gruzs() 
        {
            this.rep_gr = new EFGruzsRepository();
        }

        public RC_Gruzs(IGruzsRepository rep_ow) 
        {
            this.rep_gr = rep_ow;
        }

        public IQueryable<GRUZS> GetGruzs() 
        {
            return rep_gr.GRUZS;
        }
        /// <summary>
        /// Получить груз по id
        /// </summary>
        /// <param name="id_gruz"></param>
        /// <returns></returns>
        public GRUZS GetGruzs(int id_gruz) 
        {
            return GetGruzs().Where(g => g.id_gruz == id_gruz).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_gruz_prom_kis"></param>
        /// <param name="id_gruz_vag_kis"></param>
        /// <returns></returns>
        public GRUZS GetGruzs(int? id_gruz_prom_kis, int? id_gruz_vag_kis) 
        {
            if (id_gruz_prom_kis!=null & id_gruz_vag_kis==null){
                return GetGruzs().Where(g => g.id_ora == id_gruz_prom_kis).FirstOrDefault();
            }
            if (id_gruz_prom_kis==null & id_gruz_vag_kis!=null){
                return GetGruzs().Where(g => g.id_ora2 == id_gruz_vag_kis).FirstOrDefault();
            }
            if (id_gruz_prom_kis!=null & id_gruz_vag_kis!=null){
                return GetGruzs().Where(g => g.id_ora == id_gruz_prom_kis & g.id_ora2 == id_gruz_vag_kis).FirstOrDefault();
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_gruz_prom_kis"></param>
        /// <param name="id_gruz_vag_kis"></param>
        /// <returns></returns>
        public int? GetIDGruzs(int? id_gruz_prom_kis, int? id_gruz_vag_kis) 
        {
            GRUZS gr = GetGruzs(id_gruz_prom_kis, id_gruz_vag_kis);
            if (gr != null) {return gr.id_gruz;}
            return null;
        }
        /// <summary>
        /// Получить груз по коду ЕТ СНГ
        /// </summary>
        /// <param name="etsng"></param>
        /// <returns></returns>
        public GRUZS GetGruzsToETSNG(int? etsng) 
        {
            return GetGruzs().Where(g => g.ETSNG == etsng).FirstOrDefault();
        }
        /// <summary>
        /// Получить ID груза по коду ЕТ СНГ
        /// </summary>
        /// <param name="etsng"></param>
        /// <returns></returns>
        public int? GetIDGruzsToETSNG(int? etsng) 
        {
            GRUZS gr = GetGruzsToETSNG(etsng);
            if (gr != null) {return gr.id_gruz;}
            return null;
        }
        /// <summary>
        /// Добавить или удалить
        /// </summary>
        /// <param name="gruzs"></param>
        /// <returns></returns>
        public int SaveGruzs(GRUZS gruzs)
        {
            return rep_gr.SaveGRUZS(gruzs);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_gruz"></param>
        /// <returns></returns>
        public GRUZS DeleteGruzs(int id_gruz)
        {
            return rep_gr.DeleteGRUZS(id_gruz);
        }
    }
}
