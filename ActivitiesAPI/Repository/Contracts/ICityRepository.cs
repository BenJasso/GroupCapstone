using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        List<City> GetAllCities();
        City GetCity(int cityId);
        void CreateCity(City city);
    }
}
