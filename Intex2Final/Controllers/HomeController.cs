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

        //private IMummiesRepository repo;

        //public HomeController(IMummiesRepository temp) 
        //{
        //    repo = temp;
        //}

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

        [HttpGet]
        public IActionResult Edit (long id)
        {
            ViewBag.Burialmain = context.Burialmain.ToList();
            var mummy = context.Burialmain.Single(x => x.Id == id);
            ViewBag.id = id;
            return View("NewMummy", mummy);
        }

        [HttpPost]
        public IActionResult Edit(Burialmain update)
        {
            string type = "Record Edited";
            ViewBag.type = type;
            context.Update(update);
            context.SaveChanges();
            return View("Confirmation");
        }

        [HttpGet]
        public IActionResult Delete (long id)
        {
            var mummy = context.Burialmain.Single(X => X.Id == id);

            return View(mummy);
        }

        [HttpPost]
        public IActionResult Delete(Burialmain bm)
        {
            string type = "Record Removed";
            ViewBag.type = type;
            context.Burialmain.Remove(bm);
            context.SaveChanges();
            return View("Confirmation");
        }

        public IActionResult MummiesView(int pageNum = 1)
        {
            int pageSize = 5;

            var x = new MummiesViewModel
            {
                Burialmains = context.Burialmain
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumMummies = (context.Burialmain.Count()),
                    MummiesPerPage = pageSize,
                    CurrentPage = pageNum,

                }
            };

            return View(x);

        }

        public IActionResult DisplayPrediction()
        {
            return View();
        }
    }
}


//this is a test for git hub