﻿using EFRailCars.Abstract;
using EFRailCars.Concrete;
using EFRailCars.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailCars.Railcars
{
    public class RC_OwnersContries
    {
        IOwnersContriesRepository rep_oc;
 
        public RC_OwnersContries() 
        {
            this.rep_oc = new EFOwnersContriesRepository();
        }
 
        public RC_OwnersContries(IOwnersContriesRepository rep_oc) 
        {
            this.rep_oc = rep_oc;
        }
        /// <summary>
        /// Получить все страны 
        /// </summary>
        /// <returns></returns>
        public IQueryable<OWNERS_COUNTRIES> GetOwnersContries() 
        {
            return rep_oc.OWNERSCOUNTRIES;
        }
        /// <summary>
        /// Получить стану по ID
        /// </summary>
        /// <param name="id_own_country"></param>
        /// <returns></returns>
        public OWNERS_COUNTRIES GetOwnersContries(int id_own_country) 
        {
            return GetOwnersContries().Where(o=>o.id_own_country == id_own_country).FirstOrDefault();
        }
        /// <summary>
        /// Получить стану по ID системы КИС
        /// </summary>
        /// <param name="id_ora"></param>
        /// <returns></returns>
        public OWNERS_COUNTRIES GetOwnersContriesOfKis(int id_ora) 
        {
            return GetOwnersContries().Where(o => o.id_ora == id_ora).FirstOrDefault();
        }
        /// <summary>
        /// Получить id станы в системе RailCars по ID системы КИС
        /// </summary>
        /// <param name="id_ora"></param>
        /// <returns></returns>
        public int? GetIDOwnersContriesOfKis(int id_ora) 
        {
            OWNERS_COUNTRIES oc = GetOwnersContriesOfKis(id_ora);
            if (oc != null) { return oc.id_own_country; }
            return null;
        }
        /// <summary>
        /// Добавить или править страну
        /// </summary>
        /// <param name="owner_contries"></param>
        /// <returns></returns>
        public int SaveOwnersContries(OWNERS_COUNTRIES owner_contries) 
        {
            return rep_oc.SaveOWNERSCOUNTRIES(owner_contries);
        }
        /// <summary>
        /// Удалить страну
        /// </summary>
        /// <param name="id_own_country"></param>
        /// <returns></returns>
        public OWNERS_COUNTRIES DeleteOwnersContries(int id_own_country) 
        {
            return rep_oc.DeleteOWNERSCOUNTRIES(id_own_country);
        }

    }
}
