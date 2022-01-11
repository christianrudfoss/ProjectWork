using ApplicationCore.Entities.UserAggregate;
using System;

namespace App.Models
{
    public class UpdateUserRequestModel
    {
        public virtual int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int CreatedBy { get; set; }

        public string Password { get; set; }

        public Gender? Gender { get; set; }
    }
}
