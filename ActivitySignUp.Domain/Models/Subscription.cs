using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitySignUp.Domain.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Comments { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
