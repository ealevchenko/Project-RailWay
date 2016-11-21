using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Abstarct
{
    public interface INumVagStpr1GrRepository
    {
        IQueryable<NumVagStpr1Gr> NumVagStpr1Gr { get; }
    }
}
