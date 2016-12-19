using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models.MT
{
    public class MTWagonInfo
    {
        public int Position { get; set; }
        public int CarriageNumber { get; set; }
        public int CountryCode { get; set; }
        public string CountryName { get; set; }
        public float Weight { get; set; }
        public int IDCargo { get; set; }
        public string Cargo { get; set; }
        public int StationCode { get; set; }
        public string StationName { get; set; }
        public int Consignee { get; set; }
        public int? NaturList { get; set; }
    }
}