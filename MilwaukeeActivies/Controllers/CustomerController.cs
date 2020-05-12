using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MilwaukeeActivies.Data;
using MilwaukeeActivies.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MilwaukeeActivies.Controllers
{
    [Authorize(Roles ="Customer")]
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context1)
        {
            _context = context1;
        }
        // GET: /<controller>/
        public IActionResult Index(Customer customer1)
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
                return View(customer1);
            }
        }
        // GET: Customer/Create
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            try
            {
                Customer newCustomer = new Customer();
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                customer.IdentityUserId = userId;
                
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                // TODO: Add insert logic here
            }
            catch
            {
                return View(customer);
            }
        }


    }
}
