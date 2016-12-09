using EFRailWay.Entities.SAP;
using EFRailWay.SAP;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferWagons.Transfers
{
    public class SAP_Transfer: Transfer
    {
        private eventID eventID = eventID.TransferWagons_Transfers_SAP_Transfer;
        private SAPIncomingSupply sapis = new SAPIncomingSupply();
        private References trans_ref = new References();

        public SAP_Transfer():base() 
        { 
        
        }

        public int PutInSapIncomingSupply(trSostav sostav) 
        {
            if (sostav == null) return 0;
            List<int> list_new_wag = new List<int>();
            List<int> list_old_wag = new List<int>();
            //if (sostav.Wagons != null)
            //{
                list_new_wag = GetWagonsToListInt(sostav.Wagons);
            //}
            ResultTransfers result = new ResultTransfers( list_new_wag.Count(), 0, 0, 0, 0, 0);
            if (sostav.ParentID != null)
            {
                list_old_wag = sapis.GetSAPIncSupplyToNumWagons((int)sostav.ParentID);
            }
            DeleteExistWagon(ref list_new_wag, ref list_old_wag);
            // Удалим вагоны которых нет в новом составе
            foreach (int wag in list_old_wag) 
            {
                result.SetResultDelete(sapis.DeleteSAPIncSupply((int)sostav.ParentID, wag));
            }
            // Добавим вагоны которых нет в старом составе
            foreach (int wag in list_new_wag) 
            {
                trWagon new_wag = GetWagons(sostav.Wagons, wag);
                //Определим страну по общему справочнику
                int id_country=0;
                if (new_wag.CountryCode > 0)
                {
                    int country = 0;
                    country = int.Parse(new_wag.CountryCode.ToString().Substring(0, 2));
                    id_country = trans_ref.DefinitionIDCountrySNG(country);
                }
                //Определим груз по общему справочнику
                int id_cargo = trans_ref.DefinitionIDCargo(new_wag.IDCargo); 

                if (new_wag != null) 
                {
                    SAPIncSupply sap_Supply = new SAPIncSupply() {
                        ID = 0,
                        CompositionIndex = new_wag.CompositionIndex,
                        IDMTSostav = sostav.id,
                        CarriageNumber = new_wag.CarriageNumber,
                        Position = new_wag.Position,
                        NumNakl = null,
                        CountryCode = new_wag.CountryCode,
                        IDCountry = id_country, 
                        WeightDoc = (decimal?)new_wag.Weight,
                        DocNumReweighing = null,
                        DocDataReweighing = null,
                        WeightReweighing = null,
                        DateTimeReweighing = null,
                        PostReweighing = null,
                        CodeCargo = new_wag.IDCargo,
                        IDCargo = id_cargo,
                        CodeMaterial = null,
                        NameMaterial = null,
                        CodeStationShipment = null,
                        NameStationShipment = null,
                        CodeShop = null,
                        NameShop = null,
                        CodeNewShop = null,
                        NameNewShop = null,
                        PermissionUnload = null,
                        Step1 = null,
                        Step2 = null
                    
                    };
                    result.SetResultInsert(sapis.SaveSAPIncSupply(sap_Supply));
                }
            }
            // если есть старый состав обновим id и исправим нумерацию вагонов
            if (sostav.ParentID != null)
            {

                if (sostav.Wagons != null)
                {
                    int res_upd = sapis.UpdateSAPIncSupplyIDSostav(sostav.id, (int)sostav.ParentID);                    
                    foreach (trWagon wag in sostav.Wagons)
                    {
                        result.SetResultUpdate(sapis.UpdateSAPIncSupplyPosition(sostav.id, wag.CarriageNumber, wag.Position));
                    }
                }
                else { int res_del = sapis.DeleteSAPIncSupplySostav((int)sostav.ParentID); }
            }
            LogRW.LogWarning(String.Format("Определено для переноса в справочник САП входящие поставки {0} вагонов, удалено предыдущих вагонов: {1}, добавлено новых вагонов:  {2}, обновлено позиций вагонов : {3}, общее количество ошибок: {4}.",
            result.counts, result.deletes, result.inserts, result.updates, result.errors), eventID);
            return result.ResultInsert;
        }
    }
}
