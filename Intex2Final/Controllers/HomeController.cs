using Intex2Final.Data;
using Intex2Final.Models;
using Intex2Final.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public IActionResult Index() //Landing page
        {
            return View();
        }

        //Change the filtering to fields rather than using view context-
            //so you will not use the bodySex and other inputs like that- but instead you will use what was in the form - you can choose to use ViewBags or Models

        //------------
        //create list of if statements
            //starting with the total amount of items -- use context.tablename."AsQueryable()"
            //then making if statements for each filter saying if the input for that filter is not null, then grab all the items in that match the value that was chosen
            //then take that list and move it down 

        public IActionResult Dashboard(int pageNum = 1, string depthIn = null, string ageIn = null, string headIn = null, string sexIn = null)
        {
            //Burialmain bury = context.Burialmain;
            //ViewBag.Burialmain = bury;
            //List<Burialmain> bury = context.Burialmain.ToList();
            //ViewBag.Burialmain = bury;

            //we have two models we are using so if we need to pass data to both, do it here

            //string depth = form["depthIn"].ToString();
            //string age = form["ageIn"].ToString();
            //string head = form["headIn"].ToString();
            //string sex = form["sexIn"].ToString();


            List<Burialmain> burialList = context.Burialmain.ToList();
            ViewBag.Burialmain = burialList;

            var buryQuery = context.Burialmain.AsQueryable();

            if (depthIn != null)
            {
                buryQuery = context.Burialmain.Where(b => b.Depth == depthIn);
            }
            if (ageIn != null)
            {
                buryQuery = context.Burialmain.Where(b => b.Ageatdeath == ageIn);
            }
            if (headIn != null)
            {
                buryQuery = context.Burialmain.Where(b => b.Headdirection == headIn);
            }
            if (sexIn != null)
            {
                buryQuery = context.Burialmain.Where(b => b.Sex == sexIn);
            }

            
            //--------------------------------------------------

            int pageSize = 5;
            var passInfo = new MummyViewModel
            {
                Burialmains = buryQuery
                .OrderBy(b=>b.Id)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),


                //repo.Burialmains
                //.Where(b => b.Sex == bodySex || bodySex == null)
                //.OrderBy(b => b.Id)
                //.Skip((pageNum - 1) * pageSize)
                //.Take(pageSize),


                PageInfo = new PageInfo
                {
                    TotalNumBurials = buryQuery.Count(),
                    BurialsPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(passInfo);
        }

        public IActionResult InfoPage(int burid)
        {

            Burialmain burialmain = context.Burialmain.FirstOrDefault(a => a.Id == burid);
            if (burialmain == null)
            {
                return NotFound();
            }
            return View(burialmain);
        }
        public IActionResult Unsupervised()
        {
            return View();
        }

        public IActionResult Supervised()
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
    }
}
