namespace EFRailCars.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OWNERS
    {
        public OWNERS()
        {
            VAGONS = new HashSet<VAGONS>();
        }

        [Key]
        public int id_owner { get; set; }

        [StringLength(300)]
        public string name { get; set; }

        [StringLength(50)]
        public string abr { get; set; }

        public int? id_country { get; set; }

        public int? id_ora { get; set; }

        public int? id_ora_temp { get; set; }

        public virtual NAZN_COUNTRIES NAZN_COUNTRIES { get; set; }

        public virtual OWNERS_COUNTRIES OWNERS_COUNTRIES { get; set; }

        public virtual ICollection<VAGONS> VAGONS { get; set; }
    }
}
