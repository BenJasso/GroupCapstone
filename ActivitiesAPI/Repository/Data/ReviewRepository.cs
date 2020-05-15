using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
       
        public ReviewRepository(ApplicationDbContext applicationDbContext)
                :base(applicationDbContext)
        {

        }

        public List<Review> GetAllReviews() => FindAll().ToList();
        public Review GetReview(int reviewId) => FindByCondition(r => r.ReviewId.Equals(reviewId)).SingleOrDefault();
        public void CreateReview(Review review) => Create(review);
    }
}
