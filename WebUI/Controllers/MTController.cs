using EFRailWay.Abstract;
using EFRailWay.Entities;
using EFRailWay.MT;
using EFRailWay.References;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        private DateTime dt_start 
        {
            get
            {
                if (Session["dt_start"] == null)
                {
                    Session["dt_start"] = DateTime.Now.AddDays(-1).Date;
                }
                    return (DateTime)Session["dt_start"];
            }
            set {
                Session["dt_start"] = value;
            }
        }
        private DateTime dt_stop 
        {
            get
            {
                if (Session["dt_stop"] == null)
                {
                    Session["dt_stop"] = DateTime.Now.AddDays(1).Date.AddSeconds(-1);
                }
                return  (DateTime)Session["dt_stop"];
            }
            set {
                Session["dt_stop"] = value;
            }
        }
        private int IDStart 
        { 
            get
            {
                if (Session["IDStart"] == null)
                {
                    Session["IDStart"] = 0;
                }
                return (int)Session["IDStart"];
            }
            set {
                Session["IDStart"] = value;
            }        
        }
        private int ID
        { 
            get
            {
                if (Session["ID"] == null)
                {
                    Session["ID"] = 0;
                }
                return (int)Session["ID"];
            }
            set {
                Session["ID"] = value;
            }        
        }

        public MTController(IMTRepository mtRepository, IReferenceRailwayRepository RRRrepository)
        {
            this.repository = mtRepository;
            this.rr_repository = RRRrepository;
            mt_cont = new MTContent(mtRepository);
            ref_rw = new ReferenceRailway(RRRrepository);

        }
        
        // GET: MT
        public ViewResult Index(string date, int IDStart = 0, int ID = 0)
        {
            if (!String.IsNullOrWhiteSpace(date))
            {
                string[] array_date = date.Split('~');
                if (!String.IsNullOrWhiteSpace(array_date[0]))
                {
                    this.dt_start = DateTime.Parse(array_date[0].Trim()+":00");
                }
                if (!String.IsNullOrWhiteSpace(array_date[1]))
                {
                    this.dt_stop = DateTime.Parse(array_date[1].Trim() + ":59");
                }
            }
            if (IDStart == 0 | !String.IsNullOrWhiteSpace(date))
            {
                IQueryable<MTSostav> list_sostav = mt_cont.GetMTSostav(this.dt_start, this.dt_stop).Where(s => s.ParentID==null).OrderByDescending(s => s.DateTime);
                this.IDStart = list_sostav.Count() > 0 ? list_sostav.First().IDMTSostav : 0;
            }
            else this.IDStart = IDStart;
            if (ID == 0 | !String.IsNullOrWhiteSpace(date))
            {
                this.ID = this.IDStart;
            }
            else this.ID = ID;
            MTSostav Sostav = mt_cont.Get_MTSostav(this.ID);
            MTSostavInfo model = new MTSostavInfo()
            {
                IDStart = this.IDStart,
                Wagons = new List<MTWagonInfo>(),
                date = String.IsNullOrWhiteSpace(date) ? this.dt_start.ToString("dd.MM.yyyy 00:00") + " ~ " + this.dt_stop.ToString("dd.MM.yyyy 23:59") : date,
                dt_start = this.dt_start, 
                dt_stop = this.dt_stop,
            };
            if (Sostav != null)
            {
                Code_Station StationIn = ref_rw.GetStationsOfCode(int.Parse(Sostav.CompositionIndex.Substring(9, 4)) * 10);
                Code_Station StationFrom = ref_rw.GetStationsOfCode(int.Parse(Sostav.CompositionIndex.Substring(0, 4)) * 10);
                IQueryable<MTList> mt_list = mt_cont.Get_MTListToSostav(this.ID);
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
                            CountryName = mt_wag.CountryCode > 0 ? ref_rw.GetStateToState(int.Parse(mt_wag.CountryCode.ToString().Substring(0, 2))) : "?",
                            Weight = mt_wag.Weight,
                            IDCargo = mt_wag.IDCargo,
                            Cargo = mt_wag.Cargo,
                            StationCode = mt_wag.IDStation,
                            StationName = mt_wag.IDStation>0 ? ref_rw.GetStationsOfCodeCS(mt_wag.IDStation).Station : "?",
                            Consignee = mt_wag.Consignee,
                            ConsigneeName = mt_cont.IsConsignee(mt_wag.Consignee, tMTConsignee.AMKR) ? tMTConsignee.AMKR.ToString(): null,
                            NaturList = mt_wag.NaturList,
                        };
                        list_wi.Add(wi);
                    }
                }
                //model.IDStart = IDStart;
                model.ID = Sostav.IDMTSostav;
                model.CompositionIndex = Sostav.CompositionIndex;
                model.FileName = Sostav.FileName;
                model.DateTime = Sostav.DateTime;
                model.Operation = Sostav.Operation;
                model.OperationName = mt_cont.OperationName(Sostav.Operation);
                model.StationInCode = StationIn != null ? StationIn.CodeCS : null;
                model.StationInName = StationIn != null ? StationIn.Station : "";
                model.StationFromCode = StationFrom != null ? StationFrom.CodeCS : null;
                model.StationFromName = StationFrom != null ? StationFrom.Station : "";
                model.TrainNumber = mt_list.Count() > 0 ? mt_list.FirstOrDefault().TrainNumber as int? : null;
                model.Wagons = list_wi.ToArray();
            }

            return View(model);
        }
        public PartialViewResult Menu(DateTime start,DateTime stop)
        {
            ViewBag.SelectedIDStart = this.IDStart.ToString();
            IEnumerable<string> categories = repository.MTSostav
                .Where(x => x.ParentID == null & x.DateTime>=start & x.DateTime<=stop)
                .OrderByDescending(x => x.DateTime)
                .Select(x => x.CompositionIndex + ";" + x.IDMTSostav.ToString());
                //.Distinct();
            //.OrderBy(x => x);
            return PartialView(categories);
        }
        public PartialViewResult MenuOperation(int IDStart)
        {
            ViewBag.SelectedID = this.ID.ToString();
            IEnumerable<string> categories = mt_cont.GetOperationMTSostav(IDStart)
                .Select(x => x.DateTime.ToString() + "("+ mt_cont.OperationName(x.Operation)+ ")" + ";" + IDStart.ToString() + ";" + x.IDMTSostav.ToString());
            return PartialView(categories);
        }
    }
}