using EFRailWay.Entities.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.Reference
{
    public interface ICodeCargoRepository
    {
        IQueryable<Code_Cargo> Code_Cargo { get; }
    }
}
