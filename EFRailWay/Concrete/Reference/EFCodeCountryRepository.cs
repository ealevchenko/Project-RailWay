using EFRailWay.Abstract.Reference;
using EFRailWay.Entities.Reference;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete.Reference
{
    public class EFCodeCountryRepository : EFRepository, ICodeCountryRepository
    {
        private eventID eventID = eventID.EFRailWay_RailWay_EFCodeCountryRepository;
        
        public IQueryable<Code_Country> Code_Country
        {
            get { return context.Code_Country; }
        }

        /// <summary>
        /// Добавить строку ISO3166
        /// </summary>
        /// <param name="Code_Country"></param>
        /// <returns></returns>
        public int SaveCode_Country(Code_Country Code_Country)
        {
            Code_Country dbEntry;
            if (Code_Country.ID == 0)
            {
                dbEntry = new Code_Country()
                {
                    ID = 0,
                    Country = Code_Country.Country,
                    Alpha_2 = Code_Country.Alpha_2,
                    Alpha_3 = Code_Country.Alpha_3,
                    Code = Code_Country.Code,
                    ISO3166_2 = Code_Country.ISO3166_2,
                    IDState = Code_Country.IDState,
                    CodeEurope = Code_Country.CodeEurope
                };
                context.Code_Country.Add(dbEntry);
            }
            else
            {
                dbEntry = context.Code_Country.Find(Code_Country.ID);
                if (dbEntry != null)
                {
                    dbEntry.Country = Code_Country.Country;
                    dbEntry.Alpha_2 = Code_Country.Alpha_2;
                    dbEntry.Alpha_3 = Code_Country.Alpha_3;
                    dbEntry.Code = Code_Country.Code;
                    dbEntry.ISO3166_2 = Code_Country.ISO3166_2;
                    dbEntry.IDState = Code_Country.IDState;
                    dbEntry.CodeEurope = Code_Country.CodeEurope;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveCode_Country", eventID);
                return -1;
            }
            return dbEntry.ID;
        }
        /// <summary>
        /// Удалить строку ISO3166
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Code_Country DeleteCode_Country(int ID)
        {
            Code_Country dbEntry = context.Code_Country.Find(ID);
            if (dbEntry != null)
            {
                context.Code_Country.Remove(dbEntry);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "DeleteCode_Country", eventID);
                    return null;
                }
            }
            return dbEntry;
        }
    }
}
