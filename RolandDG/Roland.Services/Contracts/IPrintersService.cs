﻿using Roland.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolandDG.Services.Contracts
{
    public interface IPrintersService
    {
        IQueryable<Printer> GetAll();
        void Add(Printer printer);
        void Delete(Printer printer);
        void Update(Printer printer);
    }
}
