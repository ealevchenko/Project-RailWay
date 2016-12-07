using EFRailWay.Abstract;
using EFRailWay.Entities;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    public class EFReferenceCountryRepository : EFRepository, IReferenceCountryRepository
    {
        private eventID eventID = eventID.EFRailWay_RailWay_EFReferenceCountryRepository;

        public IQueryable<ReferenceCountry> ReferenceCountry
        {
            get { return context.ReferenceCountry; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="ReferenceCountry"></param>
        /// <returns></returns>
        public int SaveReferenceCountry(ReferenceCountry ReferenceCountry)
        {
            ReferenceCountry dbEntry;
            if (ReferenceCountry.IDCountry == 0)
            {
                dbEntry = new ReferenceCountry()
                {
                    IDCountry = 0,
                    Country = ReferenceCountry.Country,
                    Code = ReferenceCountry.Code
                };
                context.ReferenceCountry.Add(dbEntry);
            }
            else
            {
                dbEntry = context.ReferenceCountry.Find(ReferenceCountry.IDCountry);
                if (dbEntry != null)
                {
                    dbEntry.Country = ReferenceCountry.Country;
                    dbEntry.Code = ReferenceCountry.Code;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveReferenceCountry", eventID);
                return -1;
            }
            return dbEntry.IDCountry;

        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="IDCountry"></param>
        /// <returns></returns>
        public ReferenceCountry DeleteReferenceCountry(int IDCountry)
        {
            ReferenceCountry dbEntry = context.ReferenceCountry.Find(IDCountry);
            if (dbEntry != null)
            {
                context.ReferenceCountry.Remove(dbEntry);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "DeleteReferenceCountry", eventID);
                    return null;
                }
            }
            return dbEntry;
        }
    }
}
