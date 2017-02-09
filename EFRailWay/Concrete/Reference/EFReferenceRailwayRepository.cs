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
    public class EFReferenceRailwayRepository : EFRepository, IReferenceRailwayRepository
    {
        private eventID eventID = eventID.EFRailWay_RailWay_EFReferenceRailwayRepository;
        
        public IQueryable<Code_Cargo> Code_Cargo
        {
            get { return context.Code_Cargo; }
        }

        public IQueryable<Code_InternalRailroad> Code_InternalRailroad
        {
            get { return context.Code_InternalRailroad; }
        }

        public IQueryable<Code_State> Code_State
        {
            get { return context.Code_State; }
        }

        public IQueryable<Code_Station> Code_Station
        {
            get { return context.Code_Station; }
        }

        public bool SaveStation(Code_Station station)
        {
            if (station.IDStation == 0)
            {
                context_edit.Code_Station.Add(station);
            }
            else
            {
                Code_Station dbEntry = context_edit.Code_Station.Find(station.IDStation);
                if (dbEntry != null)
                {
                    dbEntry.Code = station.Code;
                    dbEntry.CodeCS = station.CodeCS;
                    dbEntry.Station = station.Station;
                    dbEntry.IDInternalRailroad = station.IDInternalRailroad;
                }
            }
            try
            {
                context_edit.SaveChanges();
            }
            catch (Exception e) 
            {
                return false;
            }
            return true;
        }

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
