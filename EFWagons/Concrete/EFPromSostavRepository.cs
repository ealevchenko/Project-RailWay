﻿using EFWagons.Abstarct;
using EFWagons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFWagons.Concrete
{
    public class EFPromSostavRepository : EFRepository, IPromSostavRepository
    {
        public IQueryable<PromSostav> PromSostav
        {
            get { return context.PromSostav; }
        }

    }
}
