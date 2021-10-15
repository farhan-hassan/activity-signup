using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitySignUp.Domain.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Subscription> Subscriptions { get; set; }
    }
}
