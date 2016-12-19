using EFRailWay.Abstract;
using EFRailWay.Entities;
using EFRailWay.MT;
using EFRailWay.References;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.MT;
using WebUI.MT.Models;


namespace WebUI.Controllers
{
    public class MTController : Controller
    {
        private IMTRepository repository;
        public IReferenceRailwayRepository rr_repository;
        MTContent mt_cont;
        ReferenceRailway ref_rw;

        public MTController(IMTRepository mtRepository, IReferenceRailwayRepository RRRrepository)
        {
            this.repository = mtRepository;
            this.rr_repository = RRRrepository;
            mt_cont = new MTContent(mtRepository);
            ref_rw = new ReferenceRailway(RRRrepository);

        }
        
        // GET: MT
        public ViewResult Index(int IDStart = 0, int ID = 0)
        {
            MTSostav Sostav = this.repository.MTSostav.Where(s=>s.IDMTSostav == ID).FirstOrDefault();
            MTSostavInfo model = null;
            if (Sostav != null)
            {
                Code_Station StationIn = ref_rw.GetStationsOfCode(int.Parse(Sostav.CompositionIndex.Substring(9, 4)) * 10);
                Code_Station StationFrom = ref_rw.GetStationsOfCode(int.Parse(Sostav.CompositionIndex.Substring(0, 4)) * 10);
                IQueryable<MTList> mt_list = mt_cont.Get_MTListToSostav(ID);
                List<MTWagonInfo> list_wi = new List<MTWagonInfo>();
                if (mt_list.Count() > 0)
                {
                    foreach (MTList mt_wag in mt_list)
                    {
                        MTWagonInfo wi = new MTWagonInfo()
                        {
                            Position = mt_wag.Position,
                            CarriageNumber = mt_wag.CarriageNumber,
                            CountryCode = mt_wag.CountryCode,
                            CountryName = mt_wag.CountryCode> 0 ? ref_rw.GetStateToState(int.Parse(mt_wag.CountryCode.ToString().Substring(0, 2))) : "?",
                            Weight = mt_wag.Weight,
                            IDCargo = mt_wag.IDCargo,
                            Cargo = mt_wag.Cargo,
                            StationCode = mt_wag.IDStation,
                            StationName = ref_rw.GetStationsOfCodeCS(mt_wag.IDStation).Station,
                            Consignee = mt_wag.Consignee,
                            NaturList = mt_wag.NaturList,
                        };
                        list_wi.Add(wi);
                    }
                }
                model = new MTSostavInfo()
                {
                    IDStart = IDStart,
                    ID = Sostav.IDMTSostav,
                    CompositionIndex = Sostav.CompositionIndex,
                    FileName = Sostav.FileName,
                    DateTime = Sostav.DateTime,
                    Operation = Sostav.Operation,
                    OperationName = mt_cont.OperationName(Sostav.Operation),
                    StationInCode = StationIn != null ? StationIn.CodeCS : null,
                    StationInName = StationIn != null ? StationIn.Station : "",
                    StationFromCode = StationFrom != null ? StationFrom.CodeCS : null,
                    StationFromName = StationFrom != null ? StationFrom.Station : "",
                    TrainNumber = mt_list.Count() > 0 ? mt_list.FirstOrDefault().TrainNumber as int? : null,
                    Wagons = list_wi.ToArray(),
                };
            }
            else model = new MTSostavInfo() { IDStart=IDStart, Wagons = new List<MTWagonInfo>()  };
            

            return View(model);
        }
        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = repository.MTSostav
                .Where(x => x.ParentID == null)
                .Select(x => x.CompositionIndex + ";" + x.IDMTSostav.ToString());
                //.Distinct();
            //.OrderBy(x => x);
            return PartialView(categories);

            //IEnumerable<MTSostav> indexs = repository.MTSostav
            //.Where(x => x.ParentID == null)
            //.Distinct();
            //List<MTIndex> list_index = new List<MTIndex>();
            //foreach (MTSostav mts in indexs) 
            //{
            //    list_index.Add(new MTIndex() { ID = mts.IDMTSostav, Index = mts.CompositionIndex });
            //}
            //return PartialView(list_index.ToArray());
        }
        public PartialViewResult MenuOperation(int IDStart)
        {
            IEnumerable<string> categories = mt_cont.GetOperationMTSostav(IDStart)
                .Select(x => x.DateTime.ToString() + "("+ mt_cont.OperationName(x.Operation)+ ")" + ";" + IDStart.ToString() + ";" + x.IDMTSostav.ToString());
            return PartialView(categories);
        }
    }
}