using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract
{
    public interface ICodeCountryRepository
    {
        IQueryable<Code_Country> Code_Country { get; }
    }
}
