using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract
{
    public interface IReferenceRailwayRepository : ICodeCargoRepository, ICodeInternalRailroadRepository, ICodeStateRepository, ICodeStationRepository
    {

    }
}
