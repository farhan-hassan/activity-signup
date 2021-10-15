using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySignUp.Web.ViewModels
{
    public class SubscriptionViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ActivityId { get; set; }
        [MaxLength(200)]
        public string Comments { get; set; }
    }
}
