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
                fresh.ActivityTypes = collection.ActivityTypes;
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
                fresh.CityName = collection.CityName;
                _repo.Activities.CreateActivity(fresh);
                _repo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("index");
            }
        }

        //[HttpPost]
        //public IActionResult Create(List<Activity> collection)
        //{
        //    foreach (var item in collection)
        //    {
        //        Activity fresh = new Activity();
        //        fresh.ActivityTypes = item.ActivityTypes;
        //        fresh.Address = item.Address;
        //        fresh.Price = item.Price;
        //        fresh.Date = item.Date;
        //        fresh.EventName = item.EventName;
        //        fresh.Company = item.Company;
        //        fresh.SiteURL = item.SiteURL;
        //        fresh.Season = item.Season;
        //        fresh.ZipCode = item.ZipCode;
        //        fresh.Indoor = item.Indoor;
        //        fresh.Description = item.Description;
        //        fresh.CityName = item.CityName;
        //        _repo.Activities.CreateActivity(fresh);
        //        _repo.Save();
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

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