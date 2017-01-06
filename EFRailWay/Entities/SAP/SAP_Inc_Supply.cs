namespace EFRailWay.Entities.SAP
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.SAP_Inc_Supply")]
    public partial class SAPIncSupply
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string CompositionIndex { get; set; }

        public int IDMTSostav { get; set; }

        public int CarriageNumber { get; set; }

        public int Position { get; set; }

        [StringLength(35)]
        public string NumNakl { get; set; }

        public int? CountryCode { get; set; }

        public int? IDCountry { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WeightDoc { get; set; }

        public int? DocNumReweighing { get; set; }

        public DateTime? DocDataReweighing { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? WeightReweighing { get; set; }

        public DateTime? DateTimeReweighing { get; set; }

        public int? PostReweighing { get; set; }

        public int? CodeCargo { get; set; }

        public int? IDCargo { get; set; }

        [StringLength(18)]
        public string CodeMaterial { get; set; }

        [StringLength(50)]
        public string NameMaterial { get; set; }

        [StringLength(3)]
        public string CodeStationShipment { get; set; }

        [StringLength(50)]
        public string NameStationShipment { get; set; }

        [StringLength(4)]
        public string CodeShop { get; set; }

        [StringLength(50)]
        public string NameShop { get; set; }

        [StringLength(4)]
        public string CodeNewShop { get; set; }

        [StringLength(50)]
        public string NameNewShop { get; set; }

        public bool? PermissionUnload { get; set; }

        public bool? Step1 { get; set; }

        public bool? Step2 { get; set; }
    }
}
