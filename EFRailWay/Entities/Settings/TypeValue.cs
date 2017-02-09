namespace EFRailWay.Entities.Settings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.TypeValue")]
    public partial class TypeValue
    {
        public TypeValue()
        {
            appSettings = new HashSet<appSettings>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDTypeValue { get; set; }

        [Column("TypeValue")]
        [Required]
        [StringLength(10)]
        public string TypeValue1 { get; set; }

        public virtual ICollection<appSettings> appSettings { get; set; }
    }
}
