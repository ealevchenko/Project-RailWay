namespace EFRailWay.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RailWay.Project")]
    public partial class Project
    {
        public Project()
        {
            appSettings = new HashSet<appSettings>();
        }

        [Key]
        public int IDProject { get; set; }

        [Column("Project")]
        [Required]
        [StringLength(50)]
        public string Project1 { get; set; }

        [StringLength(500)]
        public string ProjectDescription { get; set; }

        public virtual ICollection<appSettings> appSettings { get; set; }
    }
}
