using Intex2Final.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2Final.Components
{
    public class ViewComponents : ViewComponent
    {
        private IMummyRepository repo { get; set; } //pulling in the data from repo (because the context is called in the repo)

        public ViewComponents(IMummyRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedSex = RouteData?.Values["bodySex"];

            var sex = repo.Burialmains
                .Select(x => x.Sex)
                .Distinct()
                .OrderBy(x => x);

            return View(sex);
        }
    }
    public class HeadDirectionViewComponent : ViewComponent
    {

        private IMummyRepository repo { get; set; } //pulling in the data from repo (because the context is called in the repo)

        public HeadDirectionViewComponent(IMummyRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedHeaDir = RouteData?.Values["bodyHeadDir"];

            var head = repo.Burialmains
                .Select(x => x.Headdirection)
                .Distinct()
                .OrderBy(x => x);

            return View(head); // try with 2 filters at first
        }

    }
}
