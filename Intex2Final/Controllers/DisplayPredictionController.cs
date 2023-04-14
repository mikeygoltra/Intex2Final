using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ML.OnnxRuntime;

namespace Intex2Final.Controllers
{
    public class DisplayPredictionController : Controller
    {
        public IActionResult DisplayPrediction()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SwaggerData()
        {
            // Make a call to the Swagger API to retrieve the data.
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://intex2.bello-net.work/score");
                var response = await httpClient.GetAsync("/swagger/v1/swagger.json");

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    ViewBag.SwaggerData = responseData;
                }
                else
                {
                    ViewBag.SwaggerData = "Error retrieving data from API";
                }
            }

            return View("DisplayPrediction");
        }

    }
}
