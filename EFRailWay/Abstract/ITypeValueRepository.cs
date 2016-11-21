using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract
{
    public interface ITypeValueRepository 
    {
        IQueryable<TypeValue> TypeValue { get; }
        int SaveTypeValue(TypeValue TypeValue);
        TypeValue DeleteTypeValue(int IDTypeValue);
    }
}
