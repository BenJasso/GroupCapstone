using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IActivityTypeRepository : IRepositoryBase<ActivityType>
    {
        List<ActivityType> GetAllActivityTypes();
        ActivityType GetActivityType(int activityTypeId);
        void CreateActivityType(ActivityType activityType);
    }
}
