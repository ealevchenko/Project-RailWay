using EFRailWay.Abstract;
using EFRailWay.Concrete;
using EFRailWay.Entities;
using Logs;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.References
{
    
    public class ReferenceRailway
    {
        private eventID eventID = eventID.EFRailWay_References_ReferenceRailway;

        public IReferenceRailwayRepository RRRrepository;
        
        public ReferenceRailway(IReferenceRailwayRepository RRRrepository)
        {
            this.RRRrepository = RRRrepository;
        }

        public ReferenceRailway()
        {
            this.RRRrepository = new EFReferenceRailwayRepository();
        }


        #region СТРАНЫ Ж.Д (Code_State)
        /// <summary>
        /// Получить список стран
        /// </summary>
        /// <returns></returns>
        public IQueryable<Code_State> GetStates() 
        {
            return RRRrepository.Code_State.OrderBy(s => s.IDState);
        }
        /// <summary>
        /// Получить страну по коду
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IQueryable<Code_State> GetStates(int code)
        {
            return RRRrepository.Code_State.Where(s => s.IDState == code);
        }
        /// <summary>
        /// Вернуть список стран и кодов по ISO3166
        /// </summary>
        /// <returns></returns>
        public IQueryable<Code_Country> GetCountry() 
        {
            try
            {
                return RRRrepository.Code_Country;
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetCountry", eventID);
                return null;
            }
        }
        /// <summary>
        /// Вернуть строку справочника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Code_Country GetCountry(int id) 
        {
            return GetCountry().Where(c => c.ID == id).FirstOrDefault();
        }
        /// <summary>
        /// Вернуть строку справочника по коду СНГ и стран балтии
        /// </summary>
        /// <param name="code_sng"></param>
        /// <returns></returns>
        public Code_Country GetCountryOfCodeSNG(int code_sng) 
        {
            return GetCountry().Where(c => c.IDState == code_sng).FirstOrDefault();
        }

        #endregion

        #region ВНУТРЕНИЕ Ж.Д (Code_InternalRailroad)

        public IQueryable<Code_InternalRailroad> GetInternalRailroads() 
        {
            return RRRrepository.Code_InternalRailroad.OrderBy(s => s.IDInternalRailroad);
        }

        public IQueryable<Code_InternalRailroad> GetInternalRailroads(int code)
        {
            return RRRrepository.Code_InternalRailroad.Where(s => s.IDInternalRailroad == code);
        }

        public IQueryable<Code_InternalRailroad> GetInternalRailroads(int code, int code_state)
        {
            return RRRrepository.Code_InternalRailroad.Where(s => s.IDInternalRailroad == code && s.IDState==code_state).OrderBy(s=>s.IDInternalRailroad);
        }

        public IQueryable<Code_InternalRailroad> GetInternalRailroadsToState(int code_state)
        {
            return RRRrepository.Code_InternalRailroad.Where(s => s.IDState==code_state).OrderBy(s=>s.IDInternalRailroad);
        }


        #endregion

        #region СТАНЦИИ Ж.Д (Code_Station)

        public IQueryable<Code_Station> GetStations() 
        {
            return RRRrepository.Code_Station.OrderBy(s => s.IDStation);
        }

        public Code_Station GetStations(int id)
        {
            return RRRrepository.Code_Station.Where(s => s.IDStation == id).SingleOrDefault();
        }

        public Code_Station GetStationsToCode(int code)
        {
            return RRRrepository.Code_Station.Where(s => s.Code == code).FirstOrDefault();
        }

        public int? GetCodeCSStations(int code)
        {
            Code_Station cs = RRRrepository.Code_Station.Where(s => s.Code == code).FirstOrDefault();
            if (cs != null) { return cs.CodeCS; }
            return null;
        }

        public void SaveStation(Code_Station cs) 
        {
            RRRrepository.SaveStation(cs);
        }

        /// <summary>
        /// Записать код ж.д. в указаную станцию
        /// </summary>
        /// <param name="id_ir"></param>
        /// <param name="station"></param>
        private void SetIRToStation(int id_ir, int code_station ) 
        {
            IQueryable<Code_Station> Stations = RRRrepository.Code_Station.Where(s => s.CodeCS == code_station | s.Code * 10 == code_station);
            foreach (Code_Station stn in Stations)
            {
                stn.IDInternalRailroad = id_ir;
                RRRrepository.SaveStation(stn);
            }
        }
        /// <summary>
        /// Записать код ж.д. в указаный диапазон станций (>= and <=)
        /// </summary>
        /// <param name="id_ir"></param>
        /// <param name="start_station"></param>
        /// <param name="stop_station"></param>
        private void SetIRToStation(int id_ir, int start_station, int stop_station ) 
        {
            IQueryable<Code_Station> Stations = RRRrepository.Code_Station.Where(s => (s.CodeCS >= start_station | s.Code * 10 >= start_station) & (s.CodeCS <= stop_station | s.Code * 10 <= stop_station));
            foreach (Code_Station stn in Stations)
            {
                stn.IDInternalRailroad = id_ir;
                RRRrepository.SaveStation(stn);
            }
        }
        /// <summary>
        /// Записать код ж.д. в указаный диапазон станций Code_InternalRailroad.StationsCodes
        /// </summary>
        /// <param name="cir"></param>
        private void SetInternalRailroad(Code_InternalRailroad cir) 
        { 
                string[] arrayCodes = cir.StationsCodes.Split(';');
                foreach (string Codes in arrayCodes)
                {
                    if (!String.IsNullOrWhiteSpace(Codes))
                    {

                        string[] arraywhere = Codes.Replace("–", "-").Split('-');

                        switch (arraywhere.Count())
                        {
                            case 0: break;
                            case 1: SetIRToStation(cir.IDInternalRailroad, int.Parse(arraywhere[0])); break;
                            case 2: SetIRToStation(cir.IDInternalRailroad, int.Parse(arraywhere[0]),int.Parse(arraywhere[1])); break;                                
                            default: break;
                        }
                    }
                }        
        }
        /// <summary>
        /// Установить код ж.д. для всех станций
        /// </summary>
        public void SetInternalRailroadToStation() 
        {
            foreach (Code_InternalRailroad cir in GetInternalRailroads())
            {
                SetInternalRailroad(cir);
            }
        }
        /// <summary>
        /// Установить код ж.д. для всех станций указаной ж.д.
        /// </summary>
        /// <param name="code_InternalRailroad"></param>
        public void SetInternalRailroadToStation(int code_InternalRailroad) 
        {
            foreach (Code_InternalRailroad cir in GetInternalRailroads(code_InternalRailroad))
            {
                SetInternalRailroad(cir);
            }
        }

        #endregion

        #region ГРУЗЫ Ж.Д (Code_Cargo)

        public IQueryable<Code_Cargo> GetCargos_ETSNG() 
        {
            try
            {
                return RRRrepository.Code_Cargo.OrderBy(s => s.IDETSNG);
            }
            catch (Exception e)
            {
                LogRW.LogError(e, "GetCargos_ETSNG", eventID);
                return null;
            }
            
        }

        public Code_Cargo GetCargos_ETSNG(int code)
        {
            return RRRrepository.Code_Cargo.Where(s => s.IDETSNG == code).FirstOrDefault();
        }
        /// <summary>
        /// Получить строку из справочника ЕТСНГ по коду ЕТСНГ (corect-false - чистый код без коррекции, corect-true - коррекция кода диапазон code0...code9)
        /// </summary>
        /// <param name="code"></param>
        /// <param name="corect"></param>
        /// <returns></returns>
        public Code_Cargo GetCargos_ETSNG(int code, bool corect)
        {
            if (!corect)
            {
                return GetCargos_ETSNG().Where(g => g.IDETSNG == code).FirstOrDefault();
            }
            else
            {
                return GetCargos_ETSNG().Where(g => g.IDETSNG >= code * 10 & g.IDETSNG <= (code * 10) + 9).FirstOrDefault();
            }
        }


        public IQueryable<Code_Cargo> GetCargos_GNG() 
        {
            return RRRrepository.Code_Cargo.OrderBy(s => s.IDGNG);
        }

        public Code_Cargo GetCargos_GNG(int code)
        {
            return RRRrepository.Code_Cargo.Where(s => s.IDGNG == code).FirstOrDefault();
        }

        #endregion
    }
    
    
}
