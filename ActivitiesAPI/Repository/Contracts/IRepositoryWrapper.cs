using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IRepositoryWrapper
    {
        public ICityRepository Cities { get; }
        public IActivityRepository Activities { get; }
        public IActivityTypeRepository ActivityTypes { get; }
        void Save();
    }
}
