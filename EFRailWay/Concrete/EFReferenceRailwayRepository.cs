using EFRailWay.Abstract;
using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete
{
    public class EFReferenceRailwayRepository : EFRepository, IReferenceRailwayRepository
    {
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
    }
}
