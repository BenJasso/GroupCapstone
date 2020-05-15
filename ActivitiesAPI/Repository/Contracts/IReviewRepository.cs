using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        List<Review> GetAllReviews();
        Review GetReview(int reviewId);
        void CreateReview(Review review);
    }
}
