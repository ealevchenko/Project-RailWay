using EFRailWay.Entities;
using EFRailWay.MT;
using EFRailWay.References;
using EFRailWay.SAP;
using Logs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TransferWagons;
using TransferWagons.Railcars;
using TransferWagons.RailWay;
using TransferWagons.Transfers;

namespace MetallurgTrans.Helpers
{
    [Serializable()]
    public class FileSostav
    {
        public string Index
        {
            get;
            set;
        }
        public DateTime Date
        {
            get;
            set;
        }
        public int Operation
        {
            get;
            set;
        }
        public string File
        {
            get;
            set;
        }

    }

    public class MT
    {
        private eventID eventID = eventID.MetallurgTrans_Helpers_MT;
        private MTContent mtc = new MTContent();
        private SAPIncomingSupply sapis = new SAPIncomingSupply();
        private ReferenceRailway refRW = new ReferenceRailway();
        private SAP_Transfer sap_transfer = new SAP_Transfer();
        private string fromPath;
        public string FromPath { get { return this.fromPath; } set { this.fromPath = value; } }
        private bool delete_file = false;
        public bool DeleteFile { get { return this.delete_file; } set { this.delete_file = value; } }
        private int dayMonitoringTrains;
        public int DayMonitoringTrains { get { return this.dayMonitoringTrains; } set { this.dayMonitoringTrains = value; } }

        public MT()
        {

        }
        /// <summary>
        /// Получить тип операции над составом
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected int GetOperationToXml(string file)
        {
            try
            {
                XDocument doc = XDocument.Load(file);
                foreach (XElement element in doc.Element("NewDataSet").Elements("Table"))
                {
                    string opr = (string)element.Element("Operation");
                    if (String.IsNullOrEmpty(opr)) return (int)tMTOperation.not;
                    if (opr.Trim().ToUpper() == "ПРИБ") return (int)tMTOperation.coming;
                    if (opr.Trim().ToUpper() == "ТСП") return (int)tMTOperation.tsp;
                }
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[MT.GetOperationToXml]: Ошибка определения операции файл:{0}. Подробно: (Источник:{1}, Код:{2}, Описание:{3})", file, e.Source, e.HResult, e.Message), this.eventID);
            }
            return (int)tMTOperation.not;

        }
        /// <summary>
        /// Получить номер вагона
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected int GetTrainNumberToXml(string file)
        {
            try
            {
                XDocument doc = XDocument.Load(file);
                foreach (XElement element in doc.Element("NewDataSet").Elements("Table"))
                {
                    return (int)element.Element("TrainNumber");
                }
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[MT.GetTrainNumberToXml] :Ошибка определения номера поезда файл:{0}. Подробно: (Источник:{1}, Код:{2}, Описание:{3})", file, e.Source, e.HResult, e.Message), this.eventID);
            }
            return (int)tMTOperation.not;
        }
        /// <summary>
        /// Получить список файлов составов
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        protected List<FileSostav> GetFileSostav(string[] files)
        {
            List<FileSostav> listfs = new List<FileSostav>();
            foreach (string file in files)
            {
                try
                {
                    if (!String.IsNullOrEmpty(file))
                    {
                        FileInfo fi = new FileInfo(file);
                        string index = fi.Name.Substring(5, 13);
                        DateTime date = DateTime.Parse(fi.Name.Substring(19, 4) + "-" + fi.Name.Substring(23, 2) + "-" + fi.Name.Substring(25, 2) + " " + fi.Name.Substring(27, 2) + ":" + fi.Name.Substring(29, 2) + ":00");
                        int operation = GetOperationToXml(file);
                        // Добавим строку
                        listfs.Add(new FileSostav()
                        {
                            Index = index,
                            Date = date,
                            Operation = operation,
                            File = file
                        });
                    }
                }
                catch (Exception e)
                {
                    LogRW.LogError(String.Format("[MT.GetFileSostav] :Ошибка формирования строки списка файлов состава List<FileSostav>, файл:{0}. Подробно: (Источник:{1}, Код:{2}, Описание:{3})", file, e.Source, e.HResult, e.Message), this.eventID);
                }
            }
            return listfs;
        }

        /// <summary>
        /// Переносим вагоны состава
        /// </summary>
        /// <param name="file"></param>
        /// <param name="id_sostav"></param>
        /// <returns></returns>
        protected int TransferXMLToMTlist(string file, int id_sostav)
        {
            int count = 0;
            int error = 0;
            int trans = 0;
            string trans_id = "";
            try
            {
                XDocument doc = XDocument.Load(file);
                foreach (XElement element in doc.Element("NewDataSet").Elements("Table"))
                {
                    MTList mtl = new MTList()
                    {
                        IDMTList = 0,
                        IDMTSostav = id_sostav,
                        Position = !String.IsNullOrWhiteSpace((string)element.Element("Position")) ? (int)element.Element("Position") : -1,
                        CarriageNumber = !String.IsNullOrWhiteSpace((string)element.Element("CarriageNumber")) ? (int)element.Element("CarriageNumber") : -1,
                        CountryCode = !String.IsNullOrWhiteSpace((string)element.Element("CountryCode")) ? (int)element.Element("CountryCode") : -1,
                        Weight = !String.IsNullOrWhiteSpace((string)element.Element("Weight")) ? (int)element.Element("Weight") : -1,
                        IDCargo = !String.IsNullOrWhiteSpace((string)element.Element("IDCargo")) ? (int)element.Element("IDCargo") : -1,
                        Cargo = !String.IsNullOrWhiteSpace((string)element.Element("Cargo")) ? (string)element.Element("Cargo") : "?",
                        IDStation = !String.IsNullOrWhiteSpace((string)element.Element("IDStation")) ? (int)element.Element("IDStation") : -1,
                        Station = !String.IsNullOrWhiteSpace((string)element.Element("Station")) ? (string)element.Element("Station") : "?",
                        Consignee = !String.IsNullOrWhiteSpace((string)element.Element("Consignee")) ? (int)element.Element("Consignee") : -1,
                        Operation = !String.IsNullOrWhiteSpace((string)element.Element("Operation")) ? (string)element.Element("Operation") : "?",
                        CompositionIndex = !String.IsNullOrWhiteSpace((string)element.Element("CompositionIndex")) ? (string)element.Element("CompositionIndex") : "?",
                        DateOperation = !String.IsNullOrWhiteSpace((string)element.Element("DateOperation")) ? DateTime.Parse((string)element.Element("DateOperation"), CultureInfo.CreateSpecificCulture("ru-RU")) : DateTime.Now,
                        TrainNumber = !String.IsNullOrWhiteSpace((string)element.Element("TrainNumber")) ? (int)element.Element("TrainNumber") : -1,
                    };
                    // Переносим вагон 
                    int keylist = mtc.SaveMTList(mtl);
                    if (keylist > 0) { trans++; }
                    if (keylist < 1) { error++; }
                    trans_id = trans_id + keylist.ToString() + "; ";
                    count++;
                }
                LogRW.LogInformation(String.Format("В файле {0} определенно: {1} вагонов, перенесено в БД RailWay : {2}, ошибок переноса : {3}, код возврата :{4}", file, count, trans, error, trans_id), this.eventID);
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[MT.TransferXMLToMTlist] :Ошибка переноса вагонов, файл:{0}. Подробно: (Источник:{1}, Код:{2}, Описание:{3})", file, e.Source, e.HResult, e.Message), this.eventID);
                return -1;
            }
            return count;
        }
        /// <summary>
        /// Добавим вагоны для состава
        /// </summary>
        /// <param name="new_id"></param>
        /// <param name="file"></param>
        /// <param name="countCopy"></param>
        /// <param name="countError"></param>
        /// <returns></returns>
        protected bool SaveWagons(int new_id, string file, ref int countCopy, ref int countError)
        {
            try
            {
                int count_wagons = 0;
                if (new_id > 0)
                {
                    // Переносим вагоны
                    count_wagons = TransferXMLToMTlist(file, new_id);
                    if (count_wagons > 0) { countCopy++; }
                    if (count_wagons == -1) { countError++; } // Счетчик ошибок при переносе
                }
                if (new_id == -1) { countError++; } // Счетчик ошибок при переносе
                // Поставим вагоны на путь
                if (count_wagons > 0 & new_id > 0)
                {
                    ArrivalToRailWay(new_id);
                }
                if (count_wagons > 0 & new_id > 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                LogRW.LogError(String.Format("[MT.SaveWagons] :Ошибка добавления вагонов в БД RailWay, сотав :{0}. Подробно: (Источник:{1}, Код:{2}, Описание:{3})", new_id, e.Source, e.HResult, e.Message), this.eventID);
            }
            return false;
        }

        /// <summary>
        /// Перенести xml-файлы из указанной папки в таблицы MT
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="delete_file"></param>
        /// <returns></returns>
        public int Transfer(string fromPath, bool delete_file)
        {
            if (!Directory.Exists(fromPath))
            {
                LogRW.LogError(String.Format("[MT.Transfer] :Указанного пути {0} с xml-файлами не существует.", fromPath), this.eventID);
                return 0;
            }
            int countCopy = 0;
            int countExist = 0;
            int countError = 0;
            int countDelete = 0;
            string[] files = Directory.GetFiles(fromPath, "*.xml");
            if (files == null | files.Count()==0) { return 0; }
            LogRW.LogInformation(String.Format("Определенно {0} xml-файлов для копирования", files.Count()), this.eventID);
            List<FileSostav> list_sostav = GetFileSostav(files);
            var listFileSostavs = from c in list_sostav.OrderBy(c => c.Date).ThenBy(c => c.Index).ThenBy(c => c.Operation)
                                  select new { c.Index, c.Date, c.Operation, c.File };
            // Пройдемся по списку
            foreach (var fs in listFileSostavs)
            {
                try
                {
                    // защита от записи повторов
                    FileInfo fi = new FileInfo(fs.File);
                    MTSostav exs_sostav = mtc.Get_MTSostavToFile(fi.Name);
                    if (exs_sostav == null)
                    {
                        int? ParentIDSostav;
                        // получить не закрытый состав
                        //MTSostav no_close_sostav = mtc.Get_NoCloseMTSostav(fs.Index, fs.Date); 
                        MTSostav no_close_sostav = mtc.Get_NoCloseMTSostav(fs.Index, fs.Date, this.dayMonitoringTrains); // Включил режим ограничения по времени dayMonitoringTrains
                        ParentIDSostav = null;
                        if (no_close_sostav != null)
                        {
                            //// TODO: !УБРАЛ дополнительная проверка совпадение по поездам и включил режим ограничения по времени dayMonitoringTrains(разные поезда двигают один состав) дополнительная проверка совпадение по поездам 
                            //int? TrainNumber = mtc.GetTrainNumberToSostav(no_close_sostav.IDMTSostav);
                            //int TrainNumber_xml = GetTrainNumberToXml(fs.File);
                            //if (TrainNumber == TrainNumber_xml) 
                            {
                                ParentIDSostav = no_close_sostav.IDMTSostav;
                                // Закрыть состав
                                no_close_sostav.Close = DateTime.Now;
                                mtc.SaveMTSostav(no_close_sostav);
                            }
                        }
                        MTSostav new_sostav = new MTSostav()
                        {
                            FileName = fi.Name,
                            CompositionIndex = fs.Index,
                            DateTime = fs.Date,
                            Operation = fs.Operation,
                            Create = DateTime.Now,
                            Close = null,
                            ParentID = ParentIDSostav
                        };

                        int new_id = mtc.SaveMTSostav(new_sostav);
                        if (delete_file & SaveWagons(new_id, fs.File, ref  countCopy, ref  countError))
                        {
                            File.Delete(fs.File);
                            countDelete++;
                        }
                    }
                    else
                    {
                        if (!mtc.IsMTListToMTSostsv(exs_sostav.IDMTSostav))
                        {
                            if (delete_file & SaveWagons(exs_sostav.IDMTSostav, fs.File, ref  countCopy, ref  countError))
                            {
                                File.Delete(fs.File);
                                countDelete++;
                            }
                        }
                        else
                        {

                            // Файл перенесен ранеее, удалим его если это требуется
                            if (delete_file)
                            {
                                string file = fs.File;
                                File.Delete(file);
                                countDelete++;
                            }
                        }
                        countExist++;
                    }
                }
                catch (Exception e)
                {
                    LogRW.LogError(String.Format("[MT.Transfer] :Ошибка переноса xml-файла в БД Railway, файл {0}. Подробно: (Источник:{1}, Код:{2}, Описание:{3})", fs.File, e.Source, e.HResult, e.Message), this.eventID);
                    countError++;
                }
            }
            LogRW.LogInformation(String.Format("Перенос xml-файлов в БД RailWay завершен, определено для переноса {0} xml-файлов, перенесено {1}, были перенесены ранее {2}, ошибки при переносе {3}, удаленно {4}.", files.Count(), countCopy, countExist, countError, countDelete), this.eventID);
            return files.Count();
        }
        /// <summary>
        /// Перенести xml-файлы из папки по умолчанию в таблицы MT
        /// </summary>
        /// <returns></returns>
        public int Transfer()
        {
            return Transfer(this.fromPath, this.delete_file);
        }
        /// <summary>
        /// Код грузополучателя пренадлежит списку кодов грузополучателя
        /// </summary>
        /// <param name="code"></param>
        /// <param name="codes_consignee"></param>
        /// <returns></returns>
        private bool IsConsignee(int code, int[] codes_consignee)
        {
            foreach (int c in codes_consignee)
            {
                if (c == code) return true;
            }
            return false;
        }
        /// <summary>
        /// Получить перечень вагонов List<trWagon> для постановки на прибытие (если хоть один вагон имеет код получателя не АМКР или вагон не дошел до станции назначения возвращаем null)
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id_stat_receiving"></param>
        /// <param name="code_consignee"></param>
        /// <returns></returns>
        private List<trWagon> GetListWagonInArrival(IQueryable<MTList> list, int? id_stat_receiving, int[] code_consignee) 
        {
                //bool bOk = false;
                if (list == null | id_stat_receiving == null | code_consignee == null) return null;
                List<trWagon> list_wag = new List<trWagon>();
                try
                {
                    int position = 1;
                    foreach (MTList wag in list)
                    {
                        // состояние вагонов
                        int id_conditions = 17; // ожидает прибытие с УЗ                        
                        // червоная
                        if (id_stat_receiving == 467201) {
                            if (wag.IDStation == id_stat_receiving & IsConsignee(wag.Consignee, code_consignee))
                            {
                                //bOk = true; 
                                list_wag.Add(new trWagon()
                               {
                                   Position = position++,
                                   CarriageNumber = wag.CarriageNumber,
                                   CountryCode = wag.CountryCode,
                                   Weight = wag.Weight,
                                   IDCargo = wag.IDCargo,
                                   Cargo = wag.Cargo,
                                   IDStation = wag.IDStation,
                                   Station = wag.Station,
                                   Consignee = wag.Consignee,
                                   Operation = wag.Operation,
                                   CompositionIndex = wag.CompositionIndex,
                                   DateOperation = wag.DateOperation,
                                   TrainNumber = wag.TrainNumber,
                                   Conditions = id_conditions,
                               });
                            }
                            //else { id_conditions = 18; } // маневры на УЗ
                            // если есть хоть один вагон АМКР и конечная станция червонная
                        }
                        // главн
                        if (id_stat_receiving == 467004)
                        {
                            //bOk = true;
                            //TODO: ОТКЛЮЧИЛ по КривойРог главный фильтр показывать и ставить на пу
                            if (wag.IDStation != id_stat_receiving | !IsConsignee(wag.Consignee, code_consignee))
                                return null; // есть вагон недошедший до станции назанчения или с кодом грузополучателя не АМКР 
                            list_wag.Add(new trWagon()
                           {
                               Position = wag.Position,
                               CarriageNumber = wag.CarriageNumber,
                               CountryCode = wag.CountryCode,
                               Weight = wag.Weight,
                               IDCargo = wag.IDCargo,
                               Cargo = wag.Cargo,
                               IDStation = wag.IDStation,
                               Station = wag.Station,
                               Consignee = wag.Consignee,
                               Operation = wag.Operation,
                               CompositionIndex = wag.CompositionIndex,
                               DateOperation = wag.DateOperation,
                               TrainNumber = wag.TrainNumber,
                               Conditions = id_conditions,
                           });
                        }

                    }
                }
                catch (Exception e)
                {
                    LogRW.LogError(String.Format("[MT.GetListWagonInArrival] :Ошибка формирования перечня вагонов List<trWagon> (источник: {0}, № {1}, описание:  {2})", e.Source, e.HResult, e.Message), this.eventID);
                    return null;
                }
            return list_wag;
        }
        /// <summary>
        /// Получить пакет данных trSostav
        /// </summary>
        /// <param name="id_sostav"></param>
        /// <returns></returns>
        public trSostav GetSostav(int id_sostav)
        {
            // Определим класс данных состав
            MTSostav sost = mtc.Get_MTSostav(id_sostav);
            // Определим код станции по справочникам
            int? codecs_in = refRW.GetCodeCSStations(int.Parse(sost.CompositionIndex.Substring(9, 4)) * 10);
            int? codecs_from = refRW.GetCodeCSStations(int.Parse(sost.CompositionIndex.Substring(0, 4)) * 10);
            // Определим класс данных вагоны
            List<trWagon> list_wag = new List<trWagon>();
            list_wag = GetListWagonInArrival(mtc.Get_MTListToSostav(id_sostav), codecs_in, mtc.GetMTConsignee(tMTConsignee.AMKR));
            trSostav sostav = new trSostav()
            {
                id = sost.IDMTSostav,
                codecs_in_station = codecs_in,
                codecs_from_station = codecs_from,
                //FileName = sost.FileName,
                //CompositionIndex = sost.CompositionIndex,
                DateTime = sost.DateTime,
                //Operation = sost.Operation,
                //Create = sost.Create,
                //Close = sost.Close,
                ParentID = sost.ParentID,
                Wagons = list_wag != null ? list_wag.ToArray() : null,
            };
            return sostav;
        }
        /// <summary>
        /// Поставить состав в прибытие системы RailCars & RailWay
        /// </summary>
        /// <param name="id_sostav"></param>
        /// <returns></returns>
        public int ArrivalToRailWay(int id_sostav)
        {
            try
            {
                KIS_RC_Transfer rc_transfer = new KIS_RC_Transfer(); // Перенос в системе RailCars
                KIS_RW_Transfer rw_transfer = new KIS_RW_Transfer(); // Перенос в системе RailWay

                //// Определим класс данных состав
                //MTSostav sost = mtc.Get_MTSostav(id_sostav);
                //// Определим код станции по справочникам
                //int? codecs_in = refRW.GetCodeCSStations(int.Parse(sost.CompositionIndex.Substring(9, 4)) * 10);
                //int? codecs_from = refRW.GetCodeCSStations(int.Parse(sost.CompositionIndex.Substring(0, 4)) * 10);
                //// Определим класс данных вагоны
                //List<trWagon> list_wag = new List<trWagon>();
                //list_wag = GetListWagonInArrival(mtc.Get_MTListToSostav(id_sostav), codecs_in, mtc.GetMTConsignee(tMTConsignee.AMKR));
                //trSostav sostav = new trSostav()
                //{
                //    id = sost.IDMTSostav,
                //    codecs_in_station = codecs_in,
                //    codecs_from_station = codecs_from,
                //    //FileName = sost.FileName,
                //    //CompositionIndex = sost.CompositionIndex,
                //    DateTime = sost.DateTime,
                //    //Operation = sost.Operation,
                //    //Create = sost.Create,
                //    //Close = sost.Close,
                //    ParentID = sost.ParentID,
                //    Wagons = list_wag != null ? list_wag.ToArray() : null,
                //};
                trSostav sostav = GetSostav(id_sostav);
                // Поставим вагоны в систему RailCars
                int res_arc;

                try
                {
                    res_arc = rc_transfer.PutInArrival(sostav);
                    if (res_arc < 0)
                    {
                        LogRW.LogError(String.Format("[MT.ArrivalToRailWay] :Ошибка переноса состава в прибытие системы RailCars, состав: {0}, код ошибки: {1}.", sostav.id, res_arc), this.eventID);
                    }
                }
                catch (Exception e)
                {
                    LogRW.LogError(String.Format("[MT.ArrivalToRailWay] :Ошибка переноса состава в прибытие системы RailCars, состав: {0}. Подробно: (источник: {1}, № {2}, описание:  {3})", sostav.id, e.Source, e.HResult, e.Message), this.eventID);
                    res_arc = -1;
                }
                // Поставим вагоны в систему RailWay            
                // TODO: Выполнить код постановки вагонов в систему RailWay (прибытие из КР)
                // ..................
                
                
                // Создаем или изменяем строки в справочнике САП
                int rec_sap;                
                try
                {
                    rec_sap = sap_transfer.PutInSapIncomingSupply(sostav);
                }
                catch (Exception e)
                {
                    LogRW.LogError(String.Format("[MT.ArrivalToRailWay] :Ошибка формирования строк справочника SAP входящие поставки, состав: {0}. Подробно: (источник: {1}, № {2}, описание:  {3})", sostav.id, e.Source, e.HResult, e.Message), this.eventID);
                    rec_sap = -1;
                }
            }
            catch (AggregateException agex)
            {
                agex.Handle(ex =>
                {
                    LogRW.LogError(String.Format("[MT.ArrivalToRailWay]: Общая ошибка переноса состава в прибытие системы RailWay (источник: {0}, № {1}, описание:  {2})", ex.Source, ex.HResult, ex.Message), this.eventID);
                    return true;
                });
                return -1;
            }
            return 0;//TODO: исправить возврат
        }
        /// <summary>
        /// Получить список неперенесеных составов из МТ в САП
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<trSostav> CompareMT_SAP(DateTime dt) 
        {
            List<trSostav> list = new List<trSostav>();
            
            List<MTSostav> list_sostav = mtc.GetListOpenMTSostav(dt);
            if (list_sostav.Count > 0)
            {
                foreach (MTSostav sostav in list_sostav) 
                {
                    int? codecs_in = refRW.GetCodeCSStations(int.Parse(sostav.CompositionIndex.Substring(9, 4)) * 10);
                    int count_mt = mtc.CountMTList(sostav.IDMTSostav, mtc.GetMTConsignee(tMTConsignee.AMKR), codecs_in!=null?  (int)codecs_in : 0);
                    int count_sap = sapis.CountSAPIncSupply(sostav.IDMTSostav);
                    if (count_mt != count_sap & count_mt > 0) 
                    {
                        list.Add(GetSostav(sostav.IDMTSostav));
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// Коррекция данных МТ с САП с указаного периода
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int CorrectionMT_SAP(DateTime dt) 
        {
            List<trSostav> list_sostav = CompareMT_SAP(dt);
            int counts = 0;
            foreach (trSostav sostav in list_sostav) 
            {
                try
                {
                    sapis.DeleteSAPIncSupplySostav(sostav.id);
                    if (sostav.ParentID != null) sapis.DeleteSAPIncSupplySostav((int)sostav.ParentID);
                    int rec_sap = sap_transfer.PutInSapIncomingSupply(sostav);
                    counts++;
                }
                catch (Exception e)
                {
                    LogRW.LogError(String.Format("[MT.CorrectionMT_SAP] :Ошибка коррекции справочника SAP входящие поставки, состав: {0}. Подробно: (источник: {1}, № {2}, описание:  {3})", sostav.id, e.Source, e.HResult, e.Message), this.eventID);
                }

            }
            return counts;
        }
    }
}
