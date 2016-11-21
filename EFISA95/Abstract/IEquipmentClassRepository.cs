using EFISA95.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFISA95.Abstract
{
    public interface IEquipmentClassRepository
    {
        IQueryable<EquipmentClass> EquipmentClass { get; }
    }
}
