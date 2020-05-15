using Repository.Contracts;
using Repository.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _context;
        private IActivityRepository _Activities;
        public IActivityRepository Activities
        {
            get
            {
                if(_Activities == null)
                {
                    _Activities = new ActivityRepository(_context);
                }
                return _Activities;
            }
        }
        private IActivityTypeRepository _ActivityTypes;
        public IActivityTypeRepository ActivityTypes
        {
            get
            {
                if (_ActivityTypes == null)
                {
                    _ActivityTypes = new ActivityTypeRepository(_context);
                }
                return _ActivityTypes;
            }
        }

        private ICityRepository _Cities;
        public ICityRepository Cities
        {
            get
            {
                if (_Cities == null)
                {
                    _Cities = new CityRepository(_context);
                }
                return _Cities;
            }
        }
        private IReviewRepository _Reviews;
        public IReviewRepository Reviews
        {
            get
            {
                if (_Reviews == null)
                {
                    _Reviews = new ReviewRepository(_context);
                }
                return _Reviews;
            }
        }
        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
