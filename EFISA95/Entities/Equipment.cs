namespace EFISA95.Entities
{
    using EFISA95.Abstract;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Equipment")]
    public partial class Equipment : IDescription
    {
        public Equipment()
        {
            Equipment11 = new HashSet<Equipment>();
            EquipmentProperty = new HashSet<EquipmentProperty>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [StringLength(50)]
        public string EquipmentLevel { get; set; }

        [Column("Equipment")]
        public int? Equipment1 { get; set; }

        public int? HierarchyScope { get; set; }

        public int? EquipmentClassID { get; set; }

        public virtual ICollection<Equipment> Equipment11 { get; set; }

        public virtual Equipment Equipment2 { get; set; }

        public virtual EquipmentClass EquipmentClass { get; set; }

        public virtual ICollection<EquipmentProperty> EquipmentProperty { get; set; }

        public int? ParentID
        {
            get
            {
                return Equipment1;
            }
            set
            {
                Equipment1 = value;
            }
        }
    }
}
