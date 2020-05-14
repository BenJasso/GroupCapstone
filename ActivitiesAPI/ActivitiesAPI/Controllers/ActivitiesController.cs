using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Contracts;

namespace ActivitiesAPI.Controllers
{


    [Route("api/activities")]
    [ApiController]
    public class ActivitiesController : Controller
    {

        private readonly IRepositoryWrapper _repo;
        public ActivitiesController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET: Activities
        [HttpGet]
        public IActionResult Index()
        {
            var activities = _repo.Activities.GetAllActivities();
            return Ok(activities);
        }

        // GET: Activities/Details/5
        public ActionResult Details(int id)
        {
            return View();

        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activities/Create
        [HttpPost]

        public IActionResult Create(Activity collection)

        {
            try
            {

                Activity fresh = new Activity();
                fresh.ActivityTypeId = collection.ActivityTypeId;
                fresh.Address = collection.Address;
                fresh.Price = collection.Price;
                fresh.Date = collection.Date;
                fresh.EventName = collection.EventName;
                fresh.Company = collection.Company;
                fresh.SiteURL = collection.SiteURL;
                fresh.Season = collection.Season;
                fresh.ZipCode = collection.ZipCode;
                fresh.Indoor = collection.Indoor;
                fresh.Description = collection.Description;
                fresh.CityId = collection.CityId;
                _repo.Activities.CreateActivity(fresh);
                _repo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("index");
            }
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // PUT: Activities/Edit/5
        [HttpPut]
        //causes an error [ValidateAntiForgeryToken]
        public ActionResult Edit( Activity collection)
        {
            try
            {
                _repo.Activities.Update(collection);
                _repo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // DELETE: Activities/Delete/5
        [HttpDelete]
        //causes error[ValidateAntiForgeryToken]
        public ActionResult Delete(Activity collection)
        {
            try
            {
                _repo.Activities.Delete(collection);
                _repo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}