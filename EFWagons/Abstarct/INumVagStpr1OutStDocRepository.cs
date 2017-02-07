using EFWagons.Abstract;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Abstarct
{
    public interface INumVagStpr1OutStDocRepository: IDBRepository
    {
        IQueryable<NumVagStpr1OutStDoc> NumVagStpr1OutStDoc { get; }
    }
}
