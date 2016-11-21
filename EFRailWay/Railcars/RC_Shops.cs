using EFRailWay.Abstract.Railcars;
using EFRailWay.Concrete.Railcars;
using EFRailWay.Entities.Railcars;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Railcars
{
    public class RC_Shops
    {
        private eventID eventID = eventID.EFRailWay_RailCars_RC_Shops;
        IShopsRepository rep_sh;

        public RC_Shops() 
        {
            this.rep_sh = new EFShopsRepository();
        }

        public RC_Shops(IShopsRepository rep_sh) 
        {
            this.rep_sh = rep_sh;
        }
        /// <summary>
        /// Получить список цехов
        /// </summary>
        /// <returns></returns>
        public IQueryable<SHOPS> GetShops() 
        {
            try { 
            return rep_sh.SHOPS;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetShops", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить цех по ID
        /// </summary>
        /// <param name="id_shop"></param>
        /// <returns></returns>
        public SHOPS GetShops(int id_shop) 
        {
            return GetShops().Where(s => s.id_shop == id_shop).FirstOrDefault();
        }



        public SHOPS GetShopsOfKis(int id_shop_kis) 
        {
            return GetShops().Where(s => s.id_ora == id_shop_kis).FirstOrDefault();
        }
        /// <summary>
        /// Получить ID цеха системы RailCars по id системы КИС
        /// </summary>
        /// <param name="id_shop_kis"></param>
        /// <returns></returns>
        public int? GetIDShopsOfKis(int id_shop_kis) 
        {
            SHOPS shop = GetShopsOfKis(id_shop_kis);
            if (shop != null) return shop.id_stat;
            return null;
        }
        /// <summary>
        /// Добавить или удалить
        /// </summary>
        /// <param name="gruzs"></param>
        /// <returns></returns>
        public int SaveShop(SHOPS shop)
        {
            return rep_sh.SaveSHOPS(shop);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_gruz"></param>
        /// <returns></returns>
        public SHOPS DeleteShops(int id_shop)
        {
            return rep_sh.DeleteSHOPS(id_shop);
        }
    }
}
