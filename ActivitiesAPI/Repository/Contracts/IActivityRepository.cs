using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IActivityRepository : IRepositoryBase<Activity>
    {
        List<Activity> GetAllActivities();
        Activity GetActivity(int activityId);
        void CreateActivity(Activity activity);
    }
}
