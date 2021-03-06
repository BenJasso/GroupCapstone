﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MilwaukeeActivies.Data;
using MilwaukeeActivies.Models;
using Newtonsoft.Json;

namespace MilwaukeeActivies.Controllers
{
    
    [Authorize(Roles ="Customer")]
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> InterestProcess()
        {
            List<ActivityType> AllInterests = new List<ActivityType>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44386/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                ViewBag.country = "";
                HttpResponseMessage response = await client.GetAsync("https://localhost:44386/api/ActivityTypes");

                if (response.IsSuccessStatusCode)
                {
                    var details = await response.Content.ReadAsAsync<IEnumerable<ActivityType>>();
                    var ActivitiesList = details.ToList();
                    AllInterests = ActivitiesList;

                    
                }
                else
                {
                   
                }
            }
            var model = new InterestModel
            {
                AvailableInterestTypes = getInterestTypes(AllInterests)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult InterestProcess(InterestModel model)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usercurrent = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            if (_context.Interests.Where(c=> c.CustomerID == usercurrent.CustomerID) == null)
            {


                List<string> listOfInterestTypes = model.SelectedInterests.ToList();
                foreach (string item in listOfInterestTypes)
                {
                    var SelectedInterest = new Interest()
                    {
                        ActivityType = item,
                        CustomerID = usercurrent.CustomerID
                    };
                    _context.Interests.Add(SelectedInterest);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var customersInterests = _context.Interests.Where(c => c.CustomerID == usercurrent.CustomerID);
                foreach(var item in customersInterests)
                {
                    _context.Interests.Remove(item);
                }
                List<string> listOfInterestTypes = model.SelectedInterests.ToList();
                foreach (string item in listOfInterestTypes)
                {
                    var SelectedInterest = new Interest()
                    {
                        ActivityType = item,
                        CustomerID = usercurrent.CustomerID
                    };
                    _context.Interests.Add(SelectedInterest);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
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

        private IList<SelectListItem> getInterestTypes(List<ActivityType> Interests)
        {
            List<SelectListItem> listOfInterests = new List<SelectListItem>();
            {
                foreach(var item in Interests)
                {
                    listOfInterests.Add(new SelectListItem { Text = item.ActivityTypes, Value = item.ActivityTypes });
                }
                
            };
            return listOfInterests;
        }

        public async Task<IActionResult> CreateActivity()
        {
            List<SelectListItem> selectListActivityTypes = new List<SelectListItem>();
            List<ActivityType> activityTypesList = new List<ActivityType>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:44386/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                HttpResponseMessage response = await client.GetAsync("https://localhost:44386/api/ActivityTypes");

                if (response.IsSuccessStatusCode)
                {
                    var details = await response.Content.ReadAsAsync<IEnumerable<ActivityType>>();
                    activityTypesList = details.ToList();
                        foreach(var item in activityTypesList)
                        {
                        selectListActivityTypes.Add(new SelectListItem { Text = item.ActivityTypes, Value = item.ActivityTypes });     
                        }
                   
                    


                }
                else
                {

                }
            }
            ViewBag.SelectListItems = new SelectList(selectListActivityTypes,"Value", "Text" );
            return View();
        }

        [HttpPost, ActionName("CreateActivity")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateActivity(Activities Activity)
        {
            Activities activity = new Activities();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(Activity), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:44386/api/activities", content)) ;
              
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> GetAllActivities()
        {
            Activities Model = new Activities();
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

                    return View(Activity1);
                }
                else
                {
                    return View();
                }
            }
        }

        // GET: Customer
        public async Task<IActionResult> AllActivities()
        {
           
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (_context.Customers.Where(e => e.IdentityUserId == userId).SingleOrDefault() == null)
            {
                return View("Create");
            }
            else
            {


                var usercurrent = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
                ViewBag.UserId = usercurrent.CustomerID;
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
        }
        [HttpPost]
        public async Task<IActionResult> AllActivities(HomeActivityViewModel home)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usercurrent = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            ViewBag.UserId = usercurrent.CustomerID;
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

                  

                    return View(homeActivity);
                }
                else
                {
                    return View();
                }
            }
        }

//         using (var client = new HttpClient())
//                {
//                    client.BaseAddress = new Uri("http://localhost:44386/");
//    client.DefaultRequestHeaders.Accept.Clear();
//                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//                    ViewBag.country = "";
//                    HttpResponseMessage response = await client.GetAsync("https://localhost:44386/api/activities");

//                    if (response.IsSuccessStatusCode)
//                    {
//                        var details = await response.Content.ReadAsAsync<IEnumerable<Activities>>();
//    HomeActivityViewModel homeActivity = new HomeActivityViewModel();
//    homeActivity.Activities = details.ToList();
//                       

//                        return View(homeActivity);
//}
//                    else
//                    {
//                        return View();

//                    }
//                }

        public IActionResult CreateReview(int activityId)
        {
            ViewBag.ActivityId = activityId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateReview(Review Item)
        {
            try
            {
                // TODO: Add insert logic here
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                Customer newCustomer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
                Item.CustomerId = newCustomer.CustomerID;
                
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(Item), Encoding.UTF8, "application/json");
                    

                    using (var response = await httpClient.PostAsync("https://localhost:44386/api/reviews", content));

                }

                return RedirectToAction("ViewReviews", new { activityId = Item.ActivityId });
            }
            catch
            {
                return View();
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
                        if(item.ActivityId == activityId)
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

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.Include(c => c.IdentityUser).SingleOrDefaultAsync(c => c.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewData["IdentityUserId"] = new SelectList(_context.Customers, "Id");
            return View();
        }

        // POST: Customer/Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer item)
        {
            try
            {
                // TODO: Add insert logic here
                Customer newCustomer = new Customer();
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                item.IdentityUserId = userId;
                _context.Customers.Add(item);
                _context.SaveChanges();
                return RedirectToAction(nameof(InterestProcess));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", customer.IdentityUserId);
            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId, CustomerName, IdentityUserId")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdentityUserId"] = new SelectList(_context.Users, "Id", customer.IdentityUserId);
            return View(customer);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.Include(c => c.IdentityUser).SingleOrDefaultAsync(c => c.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            // TODO: Add delete logic here
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(c => c.CustomerID == id);
        }


        // GET Customer/Favorites/5
        public async Task<IActionResult> AddFavorites(int id, int activityId)
        {

            var alreadyFavorited = _context.Favorites.Where(f => f.CustomerID == id && f.ActivityID == activityId).SingleOrDefault();
            if (alreadyFavorited == null)
            {
                Favorite favorite = new Favorite
                {
                    ActivityID = activityId,
                    CustomerID = id
                };
                _context.Favorites.Add(favorite);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Favorites()
        {
            
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Customer newCustomer = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            ViewBag.userId = newCustomer.CustomerID;
            List <Activities> favoredActivities = new List<Activities>();
            var customerFavorites = _context.Favorites.Where(f => f.CustomerID == newCustomer.CustomerID).ToList();
            if (_context.Favorites.Where(f => f.CustomerID == newCustomer.CustomerID).ToList() == null)
            {
                return View("Index");
            }
            foreach (Favorite favoredActivity in customerFavorites)
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
                        var Activity1 = ActivitiesList.Where(a => a.ActivityId == favoredActivity.ActivityID).SingleOrDefault();
                        favoredActivities.Add(Activity1);
                    }
                    
                }
            }
            return View(favoredActivities);
        }


        public async Task<IActionResult> Index()
        {
            List<Activities> TotalfilteredActivities = new List<Activities>();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usercurrent = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            if (_context.Customers.Where(e => e.IdentityUserId == userId).SingleOrDefault() == null)
            {
                return View("Create");
            }
            else
            {

                ViewBag.UserId = usercurrent.CustomerID;
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

                        List<Activities> allActivities = details.ToList();
                        List<Interest> customerInterests = _context.Interests.Where(i => i.CustomerID == usercurrent.CustomerID).ToList();
                        List<Activities> filteredActivities = new List<Activities>();
                        foreach (var item in customerInterests)
                        {
                            List<Activities> temp = new List<Activities>();
                            temp = allActivities.Where(a => a.ActivityTypes == item.ActivityType).ToList();
                            foreach (var item2 in temp)
                            {
                                filteredActivities.Add(item2);
                            }

                        }


                        TotalfilteredActivities = filteredActivities;

                    }
                    else
                    {
                        return View();
                    }
                }
            }
            return View(TotalfilteredActivities);
        }





        public async Task<IActionResult> RemoveFavorite(int id, int userID)
        {
            var favorite = _context.Favorites.Where(f => f.CustomerID == userID && f.ActivityID == id).SingleOrDefault();
            _context.Favorites.Remove(favorite);
            _context.SaveChanges();
           
            return RedirectToAction(nameof(Index));
        }


       
    }
}