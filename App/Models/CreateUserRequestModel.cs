using ApplicationCore.Entities.UserAggregate;
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class CreateUserRequestModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }

        public string Password { get; set; }

        public Gender? Gender { get; set; }
    }
}
