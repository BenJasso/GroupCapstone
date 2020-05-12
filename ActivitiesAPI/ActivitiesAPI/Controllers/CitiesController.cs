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


    [Route("api/Cities")]
    [ApiController]
    public class CitiesController : Controller
    {

        private IRepositoryWrapper _repo;
        public CitiesController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET: Activities
        [HttpGet]
        public IActionResult Index()
        {
            var cities = _repo.Cities.GetAllCities();
            return Ok(cities);
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

        public IActionResult Create(City collection)

        {
            try
            {
                _repo.Cities.CreateCity(collection);
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
        public ActionResult Edit( City collection)
        {
            try
            {
                _repo.Cities.Update(collection);
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
        public ActionResult Delete(City collection)
        {
            try
            {
                _repo.Cities.Delete(collection);
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