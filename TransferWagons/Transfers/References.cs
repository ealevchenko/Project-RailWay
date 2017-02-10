using EFRailWay.Entities;
using EFRailWay.Entities.Reference;
using EFRailWay.References;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferWagons.Transfers
{
    public class References
    {
        private eventID eventID = eventID.TransferWagons_Transfers_References;
        private GeneralReferences g_ref = new GeneralReferences(); // Общие справочники
        private ReferenceRailway rw_ref = new ReferenceRailway(); // Справочники железных дорог

        public References() { }

        #region Определение ID по справочникам стран
        /// <summary>
        /// Получить id страны по коду СНГ и стран балтии
        /// </summary>
        /// <param name="code_country_sng"></param>
        /// <returns></returns>
        public int DefinitionIDCountrySNG(int code_country_sng)
        {
            Code_Country code = rw_ref.GetCountryOfCodeSNG(code_country_sng);
            return DefinitionIDCountry(code);
            //if (code != null)
            //{
            //    if (code.Code > 0)
            //    {
            //        ReferenceCountry ref_code = g_ref.GetReferenceCountryOfCode(code.Code);
            //        if (ref_code == null) 
            //        {
            //            ReferenceCountry new_rc = new ReferenceCountry() {
            //                IDCountry = 0,
            //                Country = code.Country,
            //                Code = code.Code,
            //            };
            //            int res = g_ref.SaveReferenceCountry(new_rc);
            //            if (res > 0) { return res; }
            //            else return 0;
            //        }
            //        return ref_code.IDCountry;
            //    }
            //}
            //return 0;
        }
        /// <summary>
        /// Получить id страны по коду iso
        /// </summary>
        /// <param name="code_iso"></param>
        /// <returns></returns>
        public int DefinitionIDCountryCode(int code_iso)
        {
            Code_Country code = rw_ref.GetCountryOfCode(code_iso);
            return DefinitionIDCountry(code);
        }
        /// <summary>
        /// Получить id страны из справочника
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected int DefinitionIDCountry(Code_Country code)
        {
            if (code == null) return 0;
            if (code.Code<=0) return 0;
            ReferenceCountry ref_code = g_ref.GetReferenceCountryOfCode(code.Code);
            if (ref_code == null)
            {
                ReferenceCountry new_rc = new ReferenceCountry()
                {
                    IDCountry = 0,
                    Country = code.Country,
                    Code = code.Code,
                };
                int res = g_ref.SaveReferenceCountry(new_rc);
                if (res > 0) { return res; }
                else return 0;
            }
            return ref_code.IDCountry;
        }
        #endregion

        #region Определение ID по справочникам грузов
        /// <summary>
        /// Получить id груза по коду ЕТ СНГ 
        /// </summary>
        /// <param name="id_cargo"></param>
        /// <returns></returns>
        public int DefinitionIDCargo(int id_cargo)
        {
            ReferenceCargo ref_cargo = g_ref.GetReferenceCargoOfCodeETSNG(id_cargo);
            if (ref_cargo == null) 
            {
                Code_Cargo cargo = rw_ref.GetCargos_ETSNG(id_cargo, false);
                if (cargo == null) 
                {
                    cargo = rw_ref.GetCargos_ETSNG(id_cargo, true);
                    if (cargo == null) return 0;
                }
                ReferenceCargo new_cargo = new ReferenceCargo()
                {
                    IDCargo = 0,
                    Name = cargo.ETSNG.Length > 200 ? cargo.ETSNG.Remove(199).Trim() : cargo.ETSNG.Trim(),
                    NameFull = cargo.ETSNG.Length > 500 ? cargo.ETSNG.Remove(499).Trim() : cargo.ETSNG.Trim(),
                    ETSNG = id_cargo
                };
                int res = g_ref.SaveReferenceCargo(new_cargo);
                if (res > 0) { return res; }
                else return 0;
            }
            return ref_cargo.IDCargo;
        }
        #endregion

        #region Определение ID по справочникам станций
        /// <summary>
        /// Получить id станции по коду станции
        /// </summary>
        /// <param name="code_cs"></param>
        /// <returns></returns>
        public int DefinitionIDStation(int code_cs)
        {
            ReferenceStation station = g_ref.GetReferenceStationOfCode(code_cs);
            if (station != null) return station.IDStation;
            Code_Station code_station = rw_ref.GetStationsOfCodeCS(code_cs);
            if (code_station == null) return 0;
            Code_InternalRailroad code_ir = rw_ref.GetInternalRailroads(code_station.IDInternalRailroad!=null? (int)code_station.IDInternalRailroad:0);
            Code_State code_st = rw_ref.GetState(code_ir!=null ?  code_ir.IDState: 0);
            int res = g_ref.SaveReferenceStation(new ReferenceStation()
            {
                IDStation = 0,
                Name = code_station.Station + (code_ir != null ?  " " + code_ir.Abbr + " ж/д" : "") + (code_st != null ? " " + code_st.ABB_RUS : ""),
                Station = code_station.Station,
                InternalRailroad = code_ir != null ? code_ir.InternalRailroad : "-",
                IR_Abbr = code_ir != null ? code_ir.Abbr : "-",
                NameNetwork = code_st != null ? code_st.NameNetwork : "-",
                NN_Abbr = code_st != null ? code_st.ABB_RUS : "-",
                CodeCS = code_cs
            });
            if (res > 0) { return res; }
            return 0;
        }
        #endregion

    }
}
