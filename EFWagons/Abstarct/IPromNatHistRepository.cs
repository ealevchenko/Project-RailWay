﻿using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Abstarct
{
    public interface IPromNatHistRepository
    {
        IQueryable<PromNatHist> PromNatHist { get; }
    }
}
