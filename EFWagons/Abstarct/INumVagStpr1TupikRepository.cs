using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Abstarct
{
    public interface INumVagStpr1TupikRepository
    {
        IQueryable<NumVagStpr1Tupik> NumVagStpr1Tupik { get; }
    }
}
