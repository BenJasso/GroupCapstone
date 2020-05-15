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


    [Route("api/reviews")]
    [ApiController]
    public class ReviewsController : Controller
    {

        private readonly IRepositoryWrapper _repo;
        public ReviewsController(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        // GET: Activities
        [HttpGet]
        public IActionResult Index()
        {
            var reviews = _repo.Reviews.GetAllReviews();
            return Ok(reviews);
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

        public IActionResult Create(Review collection)

        {
            try
            {

                Review fresh = new Review();
                fresh.Comment = collection.Comment;
                fresh.ActivityId = collection.ActivityId;
                fresh.Rating = collection.Rating;
                fresh.CustomerId = collection.CustomerId;
                _repo.Reviews.CreateReview(fresh);
                _repo.Save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return Ok();
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
        public ActionResult Edit( Review collection)
        {
            try
            {
                _repo.Reviews.Update(collection);
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
        public ActionResult Delete(Review collection)
        {
            try
            {
                _repo.Reviews.Delete(collection);
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