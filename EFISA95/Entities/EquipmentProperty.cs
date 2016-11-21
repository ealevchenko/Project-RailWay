namespace EFISA95.Entities
{
    using EFISA95.Abstract;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EquipmentProperty")]
    public partial class EquipmentProperty : IDescription
    {
        public EquipmentProperty()
        {
            EquipmentProperty11 = new HashSet<EquipmentProperty>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Value { get; set; }

        [Column("EquipmentProperty")]
        public int? EquipmentProperty1 { get; set; }

        [StringLength(50)]
        public string EquipmentCapabilityTestSpecification { get; set; }

        [StringLength(50)]
        public string TestResult { get; set; }

        public int? EquipmentID { get; set; }

        public int? ClassPropertyID { get; set; }

        public int? UnitID { get; set; }

        public virtual Equipment Equipment { get; set; }

        public virtual EquipmentCapabilityTestSpecification EquipmentCapabilityTestSpecification1 { get; set; }

        public virtual EquipmentClassProperty EquipmentClassProperty { get; set; }

        public virtual ICollection<EquipmentProperty> EquipmentProperty11 { get; set; }

        public virtual EquipmentProperty EquipmentProperty2 { get; set; }

        public int? ParentID
        {
            get
            {
                return EquipmentProperty1;
            }
            set
            {
                EquipmentProperty1 = value;
            }
        }
    }
}
