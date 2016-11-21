using EFISA95.Abstract;
using EFISA95.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFISA95.Model
{
    public class EquipmentModel
    {
        private IEquipmentRepository rep_Equipment;
        private IEquipmentPropertyRepository rep_EquipmentProperty;
        private IEquipmentClassRepository rep_EquipmentClass;
        private IEquipmentClassPropertyRepository rep_EquipmentClassProperty;
        private IEquipmentCapabilityTestSpecificationRepository rep_EquipmentCapabilityTestSpecification;

        public EquipmentModel(IEquipmentRepository rep_Equipment, IEquipmentPropertyRepository rep_EquipmentProperty,
            IEquipmentClassRepository rep_EquipmentClass, IEquipmentClassPropertyRepository rep_EquipmentClassProperty,
            IEquipmentCapabilityTestSpecificationRepository rep_EquipmentCapabilityTestSpecification)
        {
            this.rep_Equipment = rep_Equipment;
            this.rep_EquipmentProperty = rep_EquipmentProperty;
            this.rep_EquipmentClass = rep_EquipmentClass;
            this.rep_EquipmentClassProperty = rep_EquipmentClassProperty;
            this.rep_EquipmentCapabilityTestSpecification = rep_EquipmentCapabilityTestSpecification;
        }

        public EquipmentModel() 
        { 
            this.rep_Equipment = new EFEquipmentRepository();
            this.rep_EquipmentProperty = new EFEquipmentPropertyRepository();
            this.rep_EquipmentClass = new EFEquipmentClassRepository();
            this.rep_EquipmentClassProperty = new EFEquipmentClassPropertyRepository();
            this.rep_EquipmentCapabilityTestSpecification = new EFEquipmentCapabilityTestSpecificationRepository();        
        }
    }
}
