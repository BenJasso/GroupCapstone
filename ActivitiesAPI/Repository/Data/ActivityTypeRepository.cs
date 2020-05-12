using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
       
        public CityRepository(ApplicationDbContext applicationDbContext)
                :base(applicationDbContext)
        {

        }

        public List<City> GetAllCities() => FindAll().ToList();
        public City GetCity(int CityId) => FindByCondition(a => a.CityId.Equals(CityId)).SingleOrDefault();
        public void CreateCity(City city) => Create(city);
    }
}
