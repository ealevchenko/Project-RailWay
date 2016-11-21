namespace EFISA95.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EquipmentCapabilityTestSpecification")]
    public partial class EquipmentCapabilityTestSpecification
    {
        public EquipmentCapabilityTestSpecification()
        {
            EquipmentClassProperty = new HashSet<EquipmentClassProperty>();
            EquipmentProperty = new HashSet<EquipmentProperty>();
        }

        [Key]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Version { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        public int? HierarchyScope { get; set; }

        public virtual ICollection<EquipmentClassProperty> EquipmentClassProperty { get; set; }

        public virtual ICollection<EquipmentProperty> EquipmentProperty { get; set; }
    }
}
