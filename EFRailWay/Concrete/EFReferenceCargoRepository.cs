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
    public class EFReferenceCargoRepository : EFRepository, IReferenceCargoRepository
    {
        private eventID eventID = eventID.EFRailWay_RailWay_EFReferenceCargoRepository;

        public IQueryable<ReferenceCargo> ReferenceCargo
        {
            get { return context.ReferenceCargo; }
        }
        /// <summary>
        /// Добавить или править
        /// </summary>
        /// <param name="ReferenceCargo"></param>
        /// <returns></returns>
        public int SaveReferenceCargo(ReferenceCargo ReferenceCargo)
        {
            ReferenceCargo dbEntry;
            if (ReferenceCargo.IDCargo == 0)
            {
                dbEntry = new ReferenceCargo()
                {
                    IDCargo = 0,
                    Name = ReferenceCargo.Name,
                    NameFull = ReferenceCargo.NameFull,
                    ETSNG = ReferenceCargo.ETSNG
                };
                context.ReferenceCargo.Add(dbEntry);
            }
            else
            {
                dbEntry = context.ReferenceCargo.Find(ReferenceCargo.IDCargo);
                if (dbEntry != null)
                {
                    dbEntry.Name = ReferenceCargo.Name;
                    dbEntry.NameFull = ReferenceCargo.NameFull;
                    dbEntry.ETSNG = ReferenceCargo.ETSNG;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveReferenceCargo", eventID);
                return -1;
            }
            return dbEntry.IDCargo;

        }
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="IDCargo"></param>
        /// <returns></returns>
        public ReferenceCargo DeleteReferenceCargo(int IDCargo)
        {
            ReferenceCargo dbEntry = context.ReferenceCargo.Find(IDCargo);
            if (dbEntry != null)
            {
                context.ReferenceCargo.Remove(dbEntry);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "DeleteReferenceCargo", eventID);
                    return null;
                }
            }
            return dbEntry;
        }
    }
}
