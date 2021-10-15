using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ActivitySignUp.Web.ViewModels
{
    public class ActivityViewModel
    {
        [Required]
        public int ActivityId { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
