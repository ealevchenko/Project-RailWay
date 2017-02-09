namespace EFRailWay.Entities.MT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.MTConsignee")]
    public partial class MTConsignee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Code { get; set; }

        [Required]
        [StringLength(200)]
        public string CodeDescription { get; set; }

        public int Consignee { get; set; }
    }
}
