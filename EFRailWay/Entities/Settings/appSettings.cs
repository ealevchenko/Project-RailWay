namespace EFRailWay.Entities.Settings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.appSettings")]
    public partial class appSettings
    {
        [Key]
        public int IDAppSettings { get; set; }

        [Required]
        [StringLength(50)]
        public string Key { get; set; }

        [Required]
        [StringLength(500)]
        public string Value { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int IDTypeValue { get; set; }

        public int IDProject { get; set; }

        public virtual Project Project { get; set; }

        public virtual TypeValue TypeValue { get; set; }
    }
}
