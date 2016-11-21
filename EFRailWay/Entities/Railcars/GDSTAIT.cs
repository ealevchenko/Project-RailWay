namespace EFRailWay.Entities.Railcars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GDSTAIT")]
    public partial class GDSTAIT
    {
        public GDSTAIT()
        {
            VAGON_OPERATIONS = new HashSet<VAGON_OPERATIONS>();
        }

        [Key]
        public int id_gdstait { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public int? id_ora { get; set; }

        public virtual ICollection<VAGON_OPERATIONS> VAGON_OPERATIONS { get; set; }
    }
}
