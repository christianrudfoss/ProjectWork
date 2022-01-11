using ApplicationCore.Entities.ProjectAggregate;
using ApplicationCore.Entities.UserAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.WorkAggregate
{
    public class Work : BaseEntity
    {
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        [MaxLength(256, ErrorMessage = "ProjectName cannot exceed 256 characters")]
        public string Description { get; set; }

        public Project Project { get; set; }
        public User User { get; set; }
        public Work()
        {
            
        }
    }
}
