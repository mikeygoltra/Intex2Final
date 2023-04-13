using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2Final.Models
{
    public interface IMummiesRepository
    {
        IQueryable<Burialmain> Burialmains { get; }
    }
}
