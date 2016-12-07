using EFRailWay.Abstract.SAP;
using EFRailWay.Entities.SAP;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Concrete.SAP
{
    public class EFSAPIncSupplyRepository : EFRepository, ISAPIncSupplyRepository
    {
        private eventID eventID = eventID.EFRailWay_SAP_EFSAPIncSupplyRepository;


        public IQueryable<SAPIncSupply> SAPIncSupply
        {
            get { return context.SAPIncSupply; }
        }

        public int SaveSAPIncSupply(SAPIncSupply SAPIncSupply)
        {
            SAPIncSupply dbEntry;
            if (SAPIncSupply.ID == 0)
            {
                dbEntry = new SAPIncSupply()
                {
                    ID = SAPIncSupply.ID,
                    CompositionIndex = SAPIncSupply.CompositionIndex,
                    IDMTSostav = SAPIncSupply.IDMTSostav,
                    CarriageNumber = SAPIncSupply.CarriageNumber,
                    Position = SAPIncSupply.Position,
                    NumNakl = SAPIncSupply.NumNakl,
                    CountryCode = SAPIncSupply.CountryCode,
                    IDCountry = SAPIncSupply.IDCountry,
                    WeightDoc = SAPIncSupply.WeightDoc,
                    DocNumReweighing = SAPIncSupply.DocNumReweighing,
                    DocDataReweighing = SAPIncSupply.DocDataReweighing,
                    WeightReweighing = SAPIncSupply.WeightReweighing,
                    DateTimeReweighing = SAPIncSupply.DateTimeReweighing,
                    PostReweighing = SAPIncSupply.PostReweighing, 
                    CodeCargo = SAPIncSupply.CodeCargo,
                    IDCargo = SAPIncSupply.IDCargo,
                    CodeMaterial = SAPIncSupply.CodeMaterial,
                    NameMaterial = SAPIncSupply.NameMaterial,
                    CodeStationShipment = SAPIncSupply.CodeStationShipment,
                    NameStationShipment = SAPIncSupply.NameStationShipment,
                    CodeShop = SAPIncSupply.CodeShop,
                    NameShop = SAPIncSupply.NameShop,
                    CodeNewShop = SAPIncSupply.CodeNewShop,
                    NameNewShop = SAPIncSupply.NameNewShop,
                    PermissionUnload = SAPIncSupply.PermissionUnload,
                    Step1 = SAPIncSupply.Step1,
                    Step2 = SAPIncSupply.Step2
                };
                context.SAPIncSupply.Add(dbEntry);
            }
            else
            {
                dbEntry = context.SAPIncSupply.Find(SAPIncSupply.ID);
                if (dbEntry != null)
                {
                    dbEntry.CompositionIndex = SAPIncSupply.CompositionIndex;
                    dbEntry.IDMTSostav = SAPIncSupply.IDMTSostav;
                    dbEntry.CarriageNumber = SAPIncSupply.CarriageNumber;
                    dbEntry.Position = SAPIncSupply.Position;
                    dbEntry.NumNakl = SAPIncSupply.NumNakl;
                    dbEntry.CountryCode = SAPIncSupply.CountryCode;
                    dbEntry.IDCountry = SAPIncSupply.IDCountry;
                    dbEntry.WeightDoc = SAPIncSupply.WeightDoc;
                    dbEntry.DocNumReweighing = SAPIncSupply.DocNumReweighing;
                    dbEntry.DocDataReweighing = SAPIncSupply.DocDataReweighing;
                    dbEntry.WeightReweighing = SAPIncSupply.WeightReweighing;
                    dbEntry.DateTimeReweighing = SAPIncSupply.DateTimeReweighing;
                    dbEntry.PostReweighing = SAPIncSupply.PostReweighing;
                    dbEntry.CodeCargo = SAPIncSupply.CodeCargo;
                    dbEntry.IDCargo = SAPIncSupply.IDCargo;
                    dbEntry.CodeMaterial = SAPIncSupply.CodeMaterial;
                    dbEntry.NameMaterial = SAPIncSupply.NameMaterial;
                    dbEntry.CodeStationShipment = SAPIncSupply.CodeStationShipment;
                    dbEntry.NameStationShipment = SAPIncSupply.NameStationShipment;
                    dbEntry.CodeShop = SAPIncSupply.CodeShop;
                    dbEntry.NameShop = SAPIncSupply.NameShop;
                    dbEntry.CodeNewShop = SAPIncSupply.CodeNewShop;
                    dbEntry.NameNewShop = SAPIncSupply.NameNewShop;
                    dbEntry.PermissionUnload = SAPIncSupply.PermissionUnload;
                    dbEntry.Step1 = SAPIncSupply.Step1;
                    dbEntry.Step2 = SAPIncSupply.Step2;
                }
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "SaveSAPIncSupply", eventID);  
                return -1;
            }
            return dbEntry.ID;
        }

        public SAPIncSupply DeleteSAPIncSupply(int id)
        {
            SAPIncSupply dbEntry = context.SAPIncSupply.Find(id);
            if (dbEntry != null)
            {
                context.SAPIncSupply.Remove(dbEntry);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    LogRW.LogError(e, "DeleteSAPIncSupply", eventID);
                    return null;
                }
            }
            return dbEntry;
        }
    }
}
