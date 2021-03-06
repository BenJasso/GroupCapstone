﻿using System;
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

        public async Task<IActionResult> AllActivities()
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
                        HomeActivityViewModel homeActivity = new HomeActivityViewModel();
                        homeActivity.Activities = details.ToList();
                        

                        return View(homeActivity);
                    }
                    else
                    {
                        return View();

                    }
                }
        }
        [HttpPost]
        public async Task<IActionResult> AllActivities(HomeActivityViewModel home)
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

                    HomeActivityViewModel homeActivity = new HomeActivityViewModel();
                    homeActivity.Activities = details.Where(a => a.Price < home.MaxBudget &&
                                                                 a.Date > home.dateStart && a.Date < home.dateEnd).ToList();
                   
                    //var ActivitiesList = details.ToList();
                    //var Activity1 = ActivitiesList[0];

                    return View(homeActivity);

                }
                else
                {
                    return View();

                }
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Landingpage()
        {
            return View();
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

        public async Task<IActionResult> ViewReviews(int activityId)
        {
            ViewBag.ActivityId = activityId;
            List<Review> activityReviews = new List<Review>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44386/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ViewBag.country = "";
                HttpResponseMessage response = await client.GetAsync("https://localhost:44386/api/reviews");

                if (response.IsSuccessStatusCode)
                {



                    var details = await response.Content.ReadAsAsync<IEnumerable<Review>>();
                    foreach (Review item in details)
                    {
                        if (item.ActivityId == activityId)
                        {
                            activityReviews.Add(item);
                        }

                    }


                    return View(activityReviews);


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
