using EFRailWay.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.Settings
{
    public interface ITypeValueRepository 
    {
        IQueryable<TypeValue> TypeValue { get; }
        int SaveTypeValue(TypeValue TypeValue);
        TypeValue DeleteTypeValue(int IDTypeValue);
    }
}
