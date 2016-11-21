namespace EFISA95.Entities
{
    using EFISA95.Abstract;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EquipmentClass")]
    public partial class EquipmentClass : IDescription
    {
        public EquipmentClass()
        {
            Equipment = new HashSet<Equipment>();
            EquipmentClass1 = new HashSet<EquipmentClass>();
            EquipmentClassProperty = new HashSet<EquipmentClassProperty>();
        }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [StringLength(50)]
        public string EquipmentLevel { get; set; }

        public int? HierarchyScope { get; set; }

        public int? ParentID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }

        public virtual ICollection<EquipmentClass> EquipmentClass1 { get; set; }

        public virtual EquipmentClass EquipmentClass2 { get; set; }

        public virtual ICollection<EquipmentClassProperty> EquipmentClassProperty { get; set; }
    }
}
