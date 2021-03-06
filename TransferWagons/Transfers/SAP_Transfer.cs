﻿using EFRailWay.Entities.SAP;
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
        /// <summary>
        /// Получить строку SAPIncSupply из trWagon
        /// </summary>
        /// <param name="wagon"></param>
        /// <param name="idsostav"></param>
        /// <returns></returns>
        public SAPIncSupply ConvertWagonToSAPSupply(trWagon wagon, int idsostav)
        {
            if (wagon == null) return null;
            //Определим страну по общему справочнику
            int id_country = 0;
            if (wagon.CountryCode > 0)
            {
                int country = 0;
                country = int.Parse(wagon.CountryCode.ToString().Substring(0, 2));
                id_country = trans_ref.DefinitionIDCountrySNG(country);
            }
            //Определим груз по общему справочнику
            int id_cargo = trans_ref.DefinitionIDCargo(wagon.IDCargo);

            SAPIncSupply sap_Supply = new SAPIncSupply()
             {
                 ID = 0,
                 DateTime = wagon.DateOperation,
                 CompositionIndex = wagon.CompositionIndex,
                 IDMTSostav = idsostav,
                 CarriageNumber = wagon.CarriageNumber,
                 Position = wagon.Position,
                 NumNakl = null,
                 CountryCode = wagon.CountryCode,
                 IDCountry = id_country,
                 WeightDoc = (decimal?)wagon.Weight,
                 DocNumReweighing = null,
                 DocDataReweighing = null,
                 WeightReweighing = null,
                 DateTimeReweighing = null,
                 PostReweighing = null,
                 CodeCargo = wagon.IDCargo,
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
         return sap_Supply;

        }
        /// <summary>
        /// Записать строку с вагоном в справочник САП
        /// </summary>
        /// <param name="wagon"></param>
        /// <param name="idsostav"></param>
        /// <returns></returns>
        public int SetWagonToSAPSupply(trWagon wagon, int idsostav)
        {
            SAPIncSupply saps = ConvertWagonToSAPSupply(wagon, idsostav);
            if (saps!=null) return sapis.SaveSAPIncSupply(saps);
            return 0;
        }
        /// <summary>
        /// Проверка наличия вагона в справочнике САП
        /// </summary>
        /// <param name="idsostav"></param>
        /// <param name="vagon"></param>
        /// <returns></returns>
        public bool IsWagonToSAPSupply(int idsostav, int vagon) 
        {
            SAPIncSupply sap = sapis.GetSAPIncSupply(idsostav, vagon);
            return sap != null ? true : false;
        }

        /// <summary>
        /// Перенести состав в справочник САП входящие поставки
        /// </summary>
        /// <param name="sostav"></param>
        /// <returns></returns>
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
                result.SetResultInsert(sapis.SaveSAPIncSupply(ConvertWagonToSAPSupply(new_wag, sostav.id)));
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

        /// <summary>
        /// Получить IDSostav по умолчанию (если в таблицах MT нет данного состава тогда ему присваивается id по умолчанию (отрицательное)
        /// </summary>
        /// <returns></returns>
        public int GetDefaultIDSostav()
        {
            SAPIncSupply sap_s = sapis.GetSAPIncSupply().Where(s => s.IDMTSostav < 0).OrderBy(s => s.IDMTSostav).FirstOrDefault();
            if (sap_s != null)
            {
                return sap_s.IDMTSostav - 1;
            } return -1;
        }

    }
}
