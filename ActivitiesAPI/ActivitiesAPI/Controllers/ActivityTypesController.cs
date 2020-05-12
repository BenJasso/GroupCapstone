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
    [Route("api/ActivityTypes")]
    [ApiController]
    public class ActivityTypesController : Controller
    {
        private IRepositoryWrapper _repo;
        public ActivityTypesController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET: Activities
        [HttpGet]
        public IActionResult Index()
        {
            var ActivityTypes = _repo.ActivityTypes.GetAllActivityTypes();
            return Ok(ActivityTypes);
        }

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

        public IActionResult Create(ActivityType collection)

        {
            try
            {
                _repo.ActivityTypes.CreateActivityType(collection);
                _repo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
        public ActionResult Edit(ActivityType collection)
        {
            try
            {
                _repo.ActivityTypes.Update(collection);
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
        public ActionResult Delete(ActivityType collection)
        {
            try
            {
                _repo.ActivityTypes.Delete(collection);
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