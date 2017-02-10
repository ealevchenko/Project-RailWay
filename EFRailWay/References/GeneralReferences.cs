using EFRailWay.Abstract;
using EFRailWay.Concrete;
using EFRailWay.Entities;
using Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.References
{
    /// <summary>
    /// Справочник системы RailCars
    /// </summary>
    public class GeneralReferences
    {
        private eventID eventID = eventID.EFRailWay_References_GeneralReferences;

        private IReferenceCountryRepository rep_country;
        private IReferenceCargoRepository rep_cargo;
        private IReferenceStationRepository rep_station;

        public GeneralReferences(IReferenceCountryRepository rep_country, IReferenceCargoRepository rep_cargo, IReferenceStationRepository rep_station)
        {
            this.rep_country = rep_country;
            this.rep_cargo = rep_cargo;
            this.rep_station = rep_station;
        }

        public GeneralReferences()
        {
            this.rep_country = new EFReferenceCountryRepository();
            this.rep_cargo = new EFReferenceCargoRepository();
            this.rep_station = new EFReferenceStationRepository();
        }

        #region Справочник стран
        /// <summary>
        /// Получить перечень всех записей справочника
        /// </summary>
        /// <returns></returns>
        public IQueryable<ReferenceCountry> GetReferenceCountry() 
        {
            try
            {
                return rep_country.ReferenceCountry;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetReferenceCountry", eventID);
                return null;
            }
        }
        /// <summary>
        /// Получить запись справочника по id
        /// </summary>
        /// <param name="id_country"></param>
        /// <returns></returns>
        public ReferenceCountry GetReferenceCountry(int id_country) 
        {
            return GetReferenceCountry().Where(r => r.IDCountry == id_country).FirstOrDefault();
        }
        /// <summary>
        /// Получить запись справочника по коду страны ISO3166
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ReferenceCountry GetReferenceCountryOfCode(int code) 
        {
            return GetReferenceCountry().Where(r => r.Code == code).FirstOrDefault();
        }
        /// <summary>
        /// Сохранить 
        /// </summary>
        /// <param name="reference_country"></param>
        /// <returns></returns>
        public int SaveReferenceCountry(ReferenceCountry reference_country) {
            return this.rep_country.SaveReferenceCountry(reference_country);
        }


        #endregion

        #region Справочник грузов
        /// <summary>
        /// Получить все строки справочника грузов
        /// </summary>
        /// <returns></returns>
        public IQueryable<ReferenceCargo> GetReferenceCargo()
        {
            try
            {
                return rep_cargo.ReferenceCargo;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetReferenceCargo", eventID);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_cargo"></param>
        /// <returns></returns>
        public ReferenceCargo GetReferenceCargo(int id_cargo) 
        {
            return GetReferenceCargo().Where(r => r.IDCargo == id_cargo).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code_etsng"></param>
        /// <returns></returns>
        public ReferenceCargo GetReferenceCargoOfCodeETSNG(int code_etsng) 
        {
            return GetReferenceCargo().Where(r => r.ETSNG == code_etsng).FirstOrDefault();
        }

        public int SaveReferenceCargo(ReferenceCargo reference_cargo)
        {
            return rep_cargo.SaveReferenceCargo(reference_cargo);
        }

        #endregion

        #region Справочник станций
        /// <summary>
        /// Показать все станции
        /// </summary>
        /// <returns></returns>
        public IQueryable<ReferenceStation> GetReferenceStation()
        {
            try
            {
                return rep_station.ReferenceStation;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetReferenceStation", eventID);
                return null;
            }
        }
        /// <summary>
        /// Показать станцию по указаному id
        /// </summary>
        /// <param name="IDStation"></param>
        /// <returns></returns>
        public ReferenceStation GetReferenceStation(int IDStation)
        {
            return GetReferenceStation().Where(s => s.IDStation == IDStation).FirstOrDefault();
        }
        /// <summary>
        /// Показать станцию по указаному коду
        /// </summary>
        /// <param name="codecs"></param>
        /// <returns></returns>
        public ReferenceStation GetReferenceStationOfCode(int codecs)
        {
            return GetReferenceStation().Where(s => s.CodeCS == codecs).FirstOrDefault();
        }
        /// <summary>
        /// Добавить или изменить строку станции
        /// </summary>
        /// <param name="ReferenceStation"></param>
        /// <returns></returns>
        public int SaveReferenceStation(ReferenceStation ReferenceStation)
        {
            return rep_station.SaveReferenceStation(ReferenceStation);
        }
        #endregion

    }
}
