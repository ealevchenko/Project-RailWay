namespace EFRailCars.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NAZN_COUNTRIES
    {
        public NAZN_COUNTRIES()
        {
            OWNERS = new HashSet<OWNERS>();
            //VAGON_OPERATIONS = new HashSet<VAGON_OPERATIONS>();
        }

        [Key]
        public int id_country { get; set; }

        [StringLength(250)]
        public string name { get; set; }

        [StringLength(10)]
        public string id_ora { get; set; }

        public virtual ICollection<OWNERS> OWNERS { get; set; }

        //public virtual ICollection<VAGON_OPERATIONS> VAGON_OPERATIONS { get; set; }
    }
}
