namespace EFRailWay.Entities.KIS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.Oracle_OutputSostav")]
    public partial class Oracle_OutputSostav
    {
        public int ID { get; set; }

        public DateTime DateTime { get; set; }

        public int DocNum { get; set; }

        public int IDOrcStationFrom { get; set; }

        public int IDOrcStationOn { get; set; }

        public int? WayNumOn { get; set; }

        public int? NaprOn { get; set; }

        public int? CountWagons { get; set; }

        public int? CountSetWagons { get; set; }

        public int? CountUpdareWagons { get; set; }

        public DateTime? Close { get; set; }

        public int Status { get; set; }

        [StringLength(1000)]
        public string Message { get; set; }
    }
}
