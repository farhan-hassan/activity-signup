using ActivitySignUp.Domain.Models;
using System.Collections.Generic;

namespace ActivitySignUp.Domain
{
    public interface IActivitySignUpRepository
    {
        IEnumerable<Activity> GetAllActivities();
        IEnumerable<Subscription> GetSubscriptions();
        IEnumerable<Subscription> GetSubscriptionsByActivityId(int activityId);
        bool SaveAll();
        void AddEntity(object model);
    }
}