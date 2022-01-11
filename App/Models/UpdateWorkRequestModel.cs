using System;

namespace App.Models
{
    public class UpdateWorkRequestModel
    {
        public virtual int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        public string Description { get; set; }
    }

}
