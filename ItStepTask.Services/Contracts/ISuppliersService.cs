﻿using ItStepTask.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItStepTask.Services.Contracts
{
    public interface ISuppliersService : IService<Supplier>
    {
        IQueryable<Supplier> GetAll();
    }
}
