using ActivitySignUp.Domain;
using ActivitySignUp.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActivitySignUp.Infrastructure.Repositories
{
    public class ActivitySignUpRepository : IActivitySignUpRepository
    {
        private readonly ActivityContext _context;
        private readonly ILogger<ActivitySignUpRepository> _logger;

        public ActivitySignUpRepository(ActivityContext ctx, ILogger<ActivitySignUpRepository> logger)
        {
            _context = ctx;
            _logger = logger;
        }

        public IEnumerable<Activity> GetAllActivities()
        {
            try
            {
                _logger.LogInformation("GetAllActivities was called...");

                return _context.Activities
                           .OrderBy(a => a.ActivityId)
                           .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all activities: {ex}");
                return null;
            }
        }

        public IEnumerable<Subscription> GetSubscriptions()
        {
            return _context.Subscription.ToList();
        }

        public IEnumerable<Subscription> GetSubscriptionsByActivityId(int activityId)
        {
            return _context.Subscription
                       .Where(s => s.ActivityId == activityId)
                       .ToList();
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
