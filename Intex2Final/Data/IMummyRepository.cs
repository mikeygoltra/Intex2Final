﻿using Intex2Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2Final.Data
{
    public interface IMummyRepository
    {
        IQueryable<Burialmain> Burialmains { get; }
    }
}