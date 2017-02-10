using EFRailWay.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract
{
    public interface IReferenceStationRepository
    {
        IQueryable<ReferenceStation> ReferenceStation { get; }
        int SaveReferenceStation(ReferenceStation ReferenceStation);
        ReferenceStation DeleteReferenceStation(int IDStation);
    }
}
