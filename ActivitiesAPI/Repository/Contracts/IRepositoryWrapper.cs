using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IRepositoryWrapper
    {
        public IActivityRepository Activities { get; }
        void Save();
    }
}
