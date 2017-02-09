using EFRailWay.Entities.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract.Reference
{
    public interface ICodeStationRepository
    {
        IQueryable<Code_Station> Code_Station { get; }
        bool SaveStation(Code_Station station);
    }
}
