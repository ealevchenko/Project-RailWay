namespace EFRailWay.Entities.MT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.MTList")]
    public partial class MTList
    {
        [Key]
        public int IDMTList { get; set; }

        public int IDMTSostav { get; set; }

        public int Position { get; set; }

        public int CarriageNumber { get; set; }

        public int CountryCode { get; set; }

        public float Weight { get; set; }

        public int IDCargo { get; set; }

        [Required]
        [StringLength(50)]
        public string Cargo { get; set; }

        public int IDStation { get; set; }

        [Required]
        [StringLength(50)]
        public string Station { get; set; }

        public int Consignee { get; set; }

        [Required]
        [StringLength(50)]
        public string Operation { get; set; }

        [Required]
        [StringLength(50)]
        public string CompositionIndex { get; set; }

        public DateTime DateOperation { get; set; }

        public int TrainNumber { get; set; }

        public int? NaturList { get; set; }

        public virtual MTSostav MTSostav { get; set; }
    }
}
