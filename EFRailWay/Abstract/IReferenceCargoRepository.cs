using EFRailWay.Entities;
using EFRailWay.Entities.Railcars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFRailWay.Abstract
{
    public interface IReferenceCargoRepository
    {
        IQueryable<ReferenceCargo> ReferenceCargo { get; }
        int SaveReferenceCargo(ReferenceCargo ReferenceCargo);
        ReferenceCargo DeleteReferenceCargo(int IDCargo);
    }
}
