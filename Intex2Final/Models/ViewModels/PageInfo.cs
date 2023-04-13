using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2Final.Models.ViewModels
{
    public class PageInfo
    {
        //get the total number of burials you need to show
        public int TotalNumBurials { get; set; }
        //decide how many burials you will show per page
        public int BurialsPerPage { get; set; }
        public int CurrentPage { get; set; }

        //figure out how many pages you need
        public int TotalPages => (int)Math.Ceiling((double)TotalNumBurials / BurialsPerPage);
    }
}
