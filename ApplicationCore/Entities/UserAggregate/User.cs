using ApplicationCore.Entities.WorkAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.UserAggregate
{
    public enum Gender
    {
        Male, Female
    }
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100,ErrorMessage="Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Password cannot exceed 100 characters")]
        public string Password { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        public ICollection<Work> Work { get; set; }
        public User()
        {
            
        }
    }
}
