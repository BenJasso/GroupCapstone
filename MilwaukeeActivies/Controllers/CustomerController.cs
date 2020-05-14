﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MilwaukeeActivies.Data;
using MilwaukeeActivies.Models;

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
        public async Task<IActionResult> Favorites(int? id)
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