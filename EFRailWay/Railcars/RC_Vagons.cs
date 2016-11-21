using EFRailWay.Abstract.Railcars;
using EFRailWay.Concrete.Railcars;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Railcars
{
    public class RC_Vagons
    {
        IVagonsRepository rep_v;

        public RC_Vagons() 
        {
            this.rep_v = new EFVagonsRepository();
        }

        public RC_Vagons(IVagonsRepository rep_v) 
        {
            this.rep_v = rep_v;
        }
        /// <summary>
        /// Получить все вагоны
        /// </summary>
        /// <returns></returns>
        public IQueryable<VAGONS> GetVagons()
        {
            return rep_v.VAGONS;
        }
        /// <summary>
        /// Получить информацию по вагону по указаному номеру
        /// </summary>
        /// <param name="num_vag"></param>
        /// <returns></returns>
        public IQueryable<VAGONS> GetVagons(int num_vag) 
        {
            return GetVagons().Where(v => v.num == num_vag).OrderByDescending(v => v.date_ar);
        }
        /// <summary>
        /// Получить вагон по номеру и дате захода на АМКР
        /// </summary>
        /// <param name="num_vag"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public VAGONS GetVagons(int num_vag, DateTime dt) 
        {
            return GetVagons(num_vag).Where(v => v.date_ar <= dt & v.date_end == null).OrderByDescending(v => v.date_ar).FirstOrDefault();
        }
        /// <summary>
        /// Получить времено созданый вагон по номеру и дате захода на АМКР
        /// </summary>
        /// <param name="num_vag"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public VAGONS GetNewVagons(int num_vag, DateTime dt) 
        {
            return GetVagons(num_vag).Where(v => v.date_in <= dt).OrderByDescending(v => v.date_in).FirstOrDefault();
        }
        /// <summary>
        /// Получить ID вагона по номеру и дате захода на АМКР
        /// </summary>
        /// <param name="num_vag"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int? GetIDVagons(int num_vag, DateTime dt) 
        {
            VAGONS vg = GetVagons(num_vag, dt);
            if (vg != null) return vg.id_vag;
            return null;
        }
        /// <summary>
        /// Получить ID временно созданого вагона по номеру и дате захода на АМКР
        /// </summary>
        /// <param name="num_vag"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int? GetIDNewVagons(int num_vag, DateTime dt) 
        {
            VAGONS vg = GetNewVagons(num_vag, dt);
            if (vg != null) return vg.id_vag;
            return null;
        }
        /// <summary>
        /// Добавить удалить
        /// </summary>
        /// <param name="vagons"></param>
        /// <returns></returns>
        public int SaveVAGONS(VAGONS vagons)
        {
            return rep_v.SaveVAGONS(vagons);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public VAGONS DeleteVAGONS(int id_vag)
        {
            return rep_v.DeleteVAGONS(id_vag);
        }
    }
}
