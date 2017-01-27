namespace EFRailCars.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class VAGONS
    {
        public VAGONS()
        {
            VAGON_OPERATIONS = new HashSet<VAGON_OPERATIONS>();
        }

        [Key]
        public int id_vag { get; set; }

        public int? num { get; set; }

        public int? id_ora { get; set; }

        public int? id_owner { get; set; }

        public int? id_stat { get; set; }

        public int? is_locom { get; set; }

        [StringLength(50)]
        public string locom_seria { get; set; }

        [StringLength(50)]
        public string rod { get; set; }

        [StringLength(100)]
        public string st_otpr { get; set; }

        public DateTime? date_ar { get; set; }

        public DateTime? date_end { get; set; }

        public DateTime? date_in { get; set; }  // дата входа нового вагона

        public int? IDSostav { get; set; }      // id состава МТ входа нового вагона

        public int? Natur { get; set; }         // номер натурки нового вагона

        public bool? Transit { get; set; }      // вагон пришел транзитом

        public virtual OWNERS OWNERS { get; set; }

        public virtual ICollection<VAGON_OPERATIONS> VAGON_OPERATIONS { get; set; }
    }
}
