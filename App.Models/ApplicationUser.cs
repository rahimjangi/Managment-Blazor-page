using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required(ErrorMessage ="This field can not be null")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field can not be null")]
        public string LastName { get; set; }
    }
}
