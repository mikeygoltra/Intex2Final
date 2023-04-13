using Intex2Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2Final.Data
{
    public class EFMummyRepository : IMummyRepository
    {
        private intex2dbContext context { get; set; }
        public EFMummyRepository(intex2dbContext temp)
        {
            context = temp;
        }
        public IQueryable<Burialmain> Burialmains => context.BurialMain;

    }
}
