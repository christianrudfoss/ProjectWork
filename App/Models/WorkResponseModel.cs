using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class WorkResponseModel
    {
        public virtual int Id { get; set; }
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Consultent Name")]
        public string UserName { get; set; }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public string Description { get; set; }

        [Display(Name = "Minutes of work")]
        public double MinutesOfWork { get; set; }

        public ProjectResponseModel Project { get; set; }
        public UserResponseModel User { get; set; }
    }
}
