using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2Final.Models
{
    public class EFMummiesRepository : IMummiesRepository
    {
        private intex2dbContext context { get; set; }

        public EFMummiesRepository(intex2dbContext temp)
        {
            context = temp;
        }

        public IQueryable<Burialmain> Burialmains => context.Burialmain;
    }
}
