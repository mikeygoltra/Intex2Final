using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2Final.Models.ViewModels
{
    public class MummyViewModel
    {
        public IQueryable<Burialmain> Burialmains { get; set; }
        //public Burialmain Burialmains { get; set; }
        public PageInfo PageInfo { get; set; }
        public string PassData { get; set; }
    }
}
