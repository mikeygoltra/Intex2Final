using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2Final.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumMummies { get; set; }
        public int MummiesPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int) Math.Ceiling((double) TotalNumMummies / MummiesPerPage);
    }
}
