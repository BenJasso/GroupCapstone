using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Data
{
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {
       
        public ActivityRepository(ApplicationDbContext applicationDbContext)
                :base(applicationDbContext)
        {

        }

        public List<Activity> GetAllActivities() => FindAll().ToList();
        public Activity GetActivity(int activityId) => FindByCondition(a => a.ActivityId.Equals(activityId)).SingleOrDefault();
        public void CreateActivity(Activity activity) => Create(activity);
    }
}
