namespace EFRailCars.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OWNERS_COUNTRIES
    {
        public OWNERS_COUNTRIES()
        {
            OWNERS = new HashSet<OWNERS>();
        }

        [Key]
        public int id_own_country { get; set; }

        [StringLength(10)]
        public string name { get; set; }

        public int? id_ora { get; set; }

        public virtual ICollection<OWNERS> OWNERS { get; set; }
    }
}
