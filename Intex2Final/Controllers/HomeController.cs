using Intex2Final.Models;
using Intex2Final.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private intex2dbContext context;

        public HomeController(ILogger<HomeController> logger, intex2dbContext temp)
        {
            _logger = logger;
            context = temp;
        }

        public IActionResult Test()
        {
            List<Burialmain> bury = context.Burialmain.ToList();
            ViewBag.Burialmain = bury;
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Unsupervised()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult NewMummy()
        {
            List<Burialmain> bury = context.Burialmain.ToList();
            ViewBag.Burialmain = bury;

            var mylist = ViewBag.Burialmain as List<Burialmain>;
            var theone = mylist.Select(obj => obj.Id);
            var maxy = (theone.Max() + 1);
            ViewBag.id = maxy;

            return View(new Burialmain());
        }

        [Authorize]
        [HttpPost]
        public IActionResult NewMummy(Burialmain bm)
        {
            if (ModelState.IsValid)
            {
                string type = "Record Submitted";
                ViewBag.type = type;
                context.Add(bm);
                context.SaveChanges();
                return View("Confirmation");
            }
            else
            {
                ViewBag.Burialmain = context.Burialmain.ToList();
                return View(bm);
            }
                    
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit (long id)
        {
            ViewBag.Burialmain = context.Burialmain.ToList();
            var mummy = context.Burialmain.Single(x => x.Id == id);
            ViewBag.id = id;
            return View("NewMummy", mummy);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(Burialmain update)
        {
            string type = "Record Edited";
            ViewBag.type = type;
            context.Update(update);
            context.SaveChanges();
            return View("Confirmation");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete (long id)
        {
            var mummy = context.Burialmain.Single(X => X.Id == id);

            return View(mummy);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(Burialmain bm)
        {
            string type = "Record Removed";
            ViewBag.type = type;
            context.Burialmain.Remove(bm);
            context.SaveChanges();
            return View("Confirmation");
        }

        //[HttpGet]
        //public IActionResult MummiesView(int pageNum = 1)
        //{
        //    int pageSize = 5;

        //    var x = new MummiesViewModel
        //    {
        //        Burialmains = context.Burialmain
        //        .Skip((pageNum - 1) * pageSize)
        //        .Take(pageSize),

        //        PageInfo = new PageInfo
        //        {
        //            TotalNumBurials = (context.Burialmain.Count()),
        //            BurialsPerPage = pageSize,
        //            CurrentPage = pageNum,

        //        }
        //    };

        //    return View(x);

        //}


        //[HttpPost]
        public IActionResult MummiesView(string depth, string age, string sex, string headdir,  int pageNum = 1)
        {
            int pageSize = 5;

            var x = new MummiesViewModel
            {
                Burialmains = context.Burialmain
                .Where(b => b.Depth == depth || depth == null)
                .Where(b => b.Ageatdeath == age || age == null)
                .Where(b => b.Sex == sex || sex == null)
                .Where(b=>b.Headdirection == headdir || headdir == null)
                .OrderByDescending(b=> b.Id)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBurials = (context.Burialmain.Count()),
                    BurialsPerPage = pageSize,
                    CurrentPage = pageNum,

                    Depth = depth,
                    Age = age,
                    Sex = sex,
                    Headdir = headdir,

                }
            };

            return View(x);

        }

     

        public IActionResult InfoPage(long burid)
        {
            //---------this is where you accept the data that was passed in from the cards to display info on info page
            Burialmain burialmain = context.Burialmain.FirstOrDefault(a => a.Id == burid);
            if (burialmain == null)
            {
                return NotFound();
            }
            return View(burialmain);
        }

        //----------------------------

        public IActionResult DisplayPrediction()
        {
            return View();
        }
    }
}


//this is a test for git hub