using System;

namespace App.Models
{
    public class UpdateProjectRequestModel
    {
        public virtual int Id { get; set; }
        public string ProjectName { get; set; }

        public string CustomerName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsComplete { get; set; }
    }
}
