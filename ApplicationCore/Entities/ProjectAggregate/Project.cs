using ApplicationCore.Entities.WorkAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.ProjectAggregate
{
    public class Project: BaseEntity
    {
        [Required]
        [MaxLength(100, ErrorMessage = "ProjectName cannot exceed 100 characters")]
        public string ProjectName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "CustomerName cannot exceed 100 characters")]
        public string CustomerName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsComplete { get; set; }

        public ICollection<Work> Works { get; set; }
        public Project()
        {
            
        }
    }
}
