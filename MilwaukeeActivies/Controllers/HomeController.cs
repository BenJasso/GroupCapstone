using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MilwaukeeActivies.Data;
using MilwaukeeActivies.Models;

namespace MilwaukeeActivies.Controllers
{
    public class HomeController : Controller   
    {

        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:44386/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    ViewBag.country = "";
                    HttpResponseMessage response = await client.GetAsync("https://localhost:44386/api/activities");

                    if (response.IsSuccessStatusCode)
                    {



                        var details = await response.Content.ReadAsAsync<IEnumerable<Activities>>();
                        var ActivitiesList = details.ToList();
                        var Activity1 = ActivitiesList[0];

                        return View(ActivitiesList);


                    }
                    else
                    {
                        return View();

                    }
                }



            
        }

        public async Task<IActionResult> ActivityDetails(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44386/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ViewBag.country = "";
                HttpResponseMessage response = await client.GetAsync("https://localhost:44386/api/activities");

                if (response.IsSuccessStatusCode)
                {



                    var details = await response.Content.ReadAsAsync<IEnumerable<Activities>>();
                    var ActivitiesList = details.ToList();
                    var Activity1 = ActivitiesList.Where(a => a.ActivityId == id).SingleOrDefault();

                    return View(Activity1);


                }
                else
                {
                    return View();

                }
            }

        }

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
