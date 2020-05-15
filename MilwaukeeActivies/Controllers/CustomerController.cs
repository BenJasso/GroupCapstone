using System;
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

        public ActionResult InterestProcess()
        {
            var model = new InterestModel
            {
                AvailableInterestTypes = getInterestTypes()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> InterestProcess(InterestModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var usercurrent = _context.Customers.Where(c => c.IdentityUserId == userId).SingleOrDefault();
            List<string> listOfInterestTypes = model.SelectedInterests.ToList();
            foreach(string item in listOfInterestTypes)
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

        private IList<SelectListItem> getInterestTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Basketball", Value = "Basketball"},
                 new SelectListItem {Text = "Baseball", Value = "Baseball"},
                  new SelectListItem {Text = "Running", Value = "Running"},
                   new SelectListItem {Text = "Walking", Value = "Walking"},
                    new SelectListItem {Text = "Biking", Value = "Biking"},
                     new SelectListItem {Text = "Night Activities", Value = "Night Activities"},
                      new SelectListItem {Text = "Water", Value = "Water"}
            };
        }

        public ActionResult CreateActivity()
        {
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
            return View(); 
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
        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> Index(HomeActivityViewModel home)
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
            Favorite favorite = new Favorite
            {
                ActivityID = activityId,
                CustomerID = id
            };
            _context.Favorites.Add(favorite);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // POST: Customer/Favorites/5
        [HttpPost, ActionName("Favorites")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Favorites(int id)
        {
            List<Activities> favoredActivities = null;
            var customerFavorites = _context.Favorites.Where(f => f.CustomerID == id).ToList();
            if (_context.Favorites.Where(f => f.CustomerID == id).ToList() == null)
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
                    else
                    {

                    }
                }
            }
            return View(favoredActivities);
        }
    }
}