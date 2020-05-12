using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
    public class ActivityTypeRepository : RepositoryBase<ActivityType>, IActivityTypeRepository
    {
       
        public ActivityTypeRepository(ApplicationDbContext applicationDbContext)
                :base(applicationDbContext)
        {

        }

        public List<ActivityType> GetAllActivityTypes() => FindAll().ToList();
        public ActivityType GetActivityType(int activityTypeId) => FindByCondition(a => a.ActivityTypeId.Equals(activityTypeId)).SingleOrDefault();
        public void CreateActivityType(ActivityType activityType) => Create(activityType);
    }
}
