using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2Final.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumBurials { get; set; }
        public int BurialsPerPage { get; set; }
        public int CurrentPage { get; set; }
        
        public string Depth { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string Headdir { get; set; }
        public int TotalPages => (int) Math.Ceiling((double)TotalNumBurials / BurialsPerPage);
    }
}
