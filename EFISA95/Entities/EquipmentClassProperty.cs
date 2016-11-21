namespace EFISA95.Entities
{
    using EFISA95.Abstract;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EquipmentClassProperty")]
    public partial class EquipmentClassProperty : IDescription
    {
        public EquipmentClassProperty()
        {
            EquipmentClassProperty11 = new HashSet<EquipmentClassProperty>();
            EquipmentProperty = new HashSet<EquipmentProperty>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Value { get; set; }

        [Column("EquipmentClassProperty")]
        public int? EquipmentClassProperty1 { get; set; }

        [StringLength(50)]
        public string EquipmentCapabilityTestSpecification { get; set; }

        public int? EquipmentClassID { get; set; }

        public int? UnitID { get; set; }

        public virtual EquipmentCapabilityTestSpecification EquipmentCapabilityTestSpecification1 { get; set; }

        public virtual EquipmentClass EquipmentClass { get; set; }

        public virtual ICollection<EquipmentClassProperty> EquipmentClassProperty11 { get; set; }

        public virtual EquipmentClassProperty EquipmentClassProperty2 { get; set; }

        public virtual ICollection<EquipmentProperty> EquipmentProperty { get; set; }

        public int? ParentID
        {
            get
            {
                return EquipmentClassProperty1;
            }
            set
            {
                EquipmentClassProperty1 = value;
            }
        }
    }
}
