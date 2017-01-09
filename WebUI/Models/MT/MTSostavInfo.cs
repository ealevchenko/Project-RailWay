using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Models.MT;

namespace WebUI.MT.Models
{
    public class MTSostavInfo
    {
        public string date{ get; set; } 
        public DateTime dt_start { get; set; } 
        public DateTime dt_stop { get; set; } 
        public int IDStart { get; set; } 
        public int ID { get; set; } 
        public string CompositionIndex { get; set; }
        public string FileName { get; set; }
        public DateTime DateTime { get; set; }
        public int Operation { get; set; }
        public string OperationName { get; set; }
        public int? StationInCode { get; set; }    
        public string StationInName { get; set; }   
        public int? StationFromCode { get; set; }
        public string StationFromName { get; set; }
        public int? TrainNumber { get; set; }
        public IEnumerable<MTWagonInfo> Wagons { get; set; }
        public int TotalWagons
        {
            get { return Wagons.Count(); }
        }
        public int TotalWagonsinAMKR
        {
            get { return Wagons.Count(); }
        }
    }
}