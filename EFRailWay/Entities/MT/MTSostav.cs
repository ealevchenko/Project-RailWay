namespace EFRailWay.Entities.MT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.MTSostav")]
    public partial class MTSostav
    {
        public MTSostav()
        {
            MTList = new HashSet<MTList>();
        }

        [Key]
        public int IDMTSostav { get; set; }

        [Required]
        [StringLength(50)]
        public string FileName { get; set; }

        [Required]
        [StringLength(50)]
        public string CompositionIndex { get; set; }

        public DateTime DateTime { get; set; }

        public int Operation { get; set; }

        public DateTime Create { get; set; }

        public DateTime? Close { get; set; }

        public int? ParentID { get; set; }

        public virtual ICollection<MTList> MTList { get; set; }
    }
}
