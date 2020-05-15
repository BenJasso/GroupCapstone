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
       
       
        public async Task<IActionResult> SelectInterests()
        {
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
                    var ActivityTypeList = details.ToList();
                    

                    return View(ActivityTypeList);
                }
                else
                {
                    return View();
                }
            }
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

        public async Task<IActionResult> Index(Customer customer1)
        {
            var customer = _context.Users.Where(u => u.Email == User.Identity.Name).SingleOrDefault();
            var id = customer.Id;
            customer1 = _context.Customers.Where(c => c.IdentityUserId == id).SingleOrDefault();
            if (_context.Customers.Where(e => e.IdentityUserId == id).SingleOrDefault() == null)
            {
                return View("Create");
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:44386/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    ViewBag.country = "";
                    HttpResponseMessage response = await client.GetAsync("https://localhost:44386/api/activities/");

                    if (response.IsSuccessStatusCode)
                    {
                        var details = await response.Content.ReadAsAsync<IEnumerable<Activities>>();
                        var ActivitiesList = details.ToList();
                        

                        return View(ActivitiesList);
                    }
                    else
                    {
                        return View();

                    }
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
            var customer = await _context.Customers
                .Include(c => c.IdentityUser)
                .SingleOrDefaultAsync(c => c.CustomerID == id);
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
                return RedirectToAction(nameof(Index));
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

            var customer = await _context.Customers
                .Include(c => c.IdentityUser)
                .SingleOrDefaultAsync(c => c.CustomerID == id);
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

        
        
    }
}