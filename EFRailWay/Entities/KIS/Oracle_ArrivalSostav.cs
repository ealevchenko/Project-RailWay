namespace EFRailWay.Entities.KIS
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.Oracle_ArrivalSostav")]
    public partial class Oracle_ArrivalSostav
    {
        [Key]
        public int IDOrcSostav { get; set; }

        public DateTime DateTime { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public int Hour { get; set; }

        public int Minute { get; set; }

        public int NaturNum { get; set; }

        public int IDOrcStation { get; set; }

        public int? WayNum { get; set; }

        public int? Napr { get; set; }

        public int? CountWagons { get; set; }

        public int? CountNatHIist { get; set; }

        public int? CountSetWagons { get; set; }

        public int? CountSetNatHIist { get; set; }

        public DateTime? Close { get; set; }

        public int? Status { get; set; }

        public string ListWagons { get; set; }

        public string ListNoSetWagons { get; set; }

        public string ListNoUpdateWagons { get; set; }

    }
}
