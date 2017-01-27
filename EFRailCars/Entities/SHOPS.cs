namespace EFRailCars.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SHOPS
    {
        public SHOPS()
        {
            VAGON_OPERATIONS = new HashSet<VAGON_OPERATIONS>();
        }

        [Key]
        public int id_shop { get; set; }

        [StringLength(250)]
        public string name { get; set; }

        [StringLength(500)]
        public string name_full { get; set; }

        public int? id_stat { get; set; }

        public int? id_ora { get; set; }

        public virtual ICollection<VAGON_OPERATIONS> VAGON_OPERATIONS { get; set; }
    }
}
