using Intex2Final.Data;
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

        private IMummyRepository repo;
        private intex2dbContext context;

        public HomeController(ILogger<HomeController> logger, intex2dbContext temp, IMummyRepository another)
        {
            _logger = logger;
            context = temp;
            repo = another;
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
        public IActionResult Dashboard(string bodySex, int pageNum = 1)
        {
            int pageSize = 5;

            //we have two models we are using so if we need to pass data to both, do it here
            var passInfo = new MummyViewModel
            {
                Burialmains = repo.Burialmains
                .Where(b => b.Sex == bodySex || bodySex == null)
                .OrderBy(b => b.Id)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize),


                PageInfo = new PageInfo
                {
                    TotalNumBurials =
                        (bodySex == null ?
                            repo.Burialmains.Count()
                            : repo.Burialmains.Where(x => x.Sex == bodySex).Count()),
                    BurialsPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(passInfo);
        }

        public IActionResult InfoPage(int burid)
        {

            Burialmain burialmain = repo.Burialmains.FirstOrDefault(a => a.Id == burid);
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
    }
}
