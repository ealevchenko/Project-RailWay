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
    public class RC_Owners
    {
        IOwnersRepository rep_ow;

        public RC_Owners() 
        {
            this.rep_ow = new EFOwnersRepository();
        }

        public RC_Owners(IOwnersRepository rep_ow) 
        {
            this.rep_ow = rep_ow;
        }
        /// <summary>
        /// Получить всех владельцев
        /// </summary>
        /// <returns></returns>
        public IQueryable<OWNERS> GetOwners()
        {
            return rep_ow.OWNERS;
        }
        /// <summary>
        /// Получить владельца по id системы КИС
        /// </summary>
        /// <param name="num_vag"></param>
        /// <returns></returns>
        public OWNERS GetOwnersOfKis(int id_sob_kis) 
        {
            return GetOwners().Where(o => o.id_ora == id_sob_kis).FirstOrDefault();
        }
        /// <summary>
        /// Получить владельца по id системы RailCars
        /// </summary>
        /// <param name="id_owner"></param>
        /// <returns></returns>
        public OWNERS GetOwners(int id_owner) 
        {
            return GetOwners().Where(o => o.id_owner == id_owner).FirstOrDefault();
        }
        /// <summary>
        /// Получить Id владельца по id системы КИС
        /// </summary>
        /// <param name="id_sob_kis"></param>
        /// <returns></returns>
        public int? GetIDOwnersOfKis(int id_sob_kis) 
        {
            OWNERS ow = GetOwnersOfKis(id_sob_kis);
            if (ow != null) return ow.id_owner;
            return null;
        }
        /// <summary>
        /// Добавить удалить
        /// </summary>
        /// <param name="vagons"></param>
        /// <returns></returns>
        public int SaveOwners(OWNERS owners)
        {
            return rep_ow.SaveOWNERS(owners);
        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        public OWNERS DeleteOwners(int id_owner)
        {
            return rep_ow.DeleteOWNERS(id_owner);
        }
    }
}
