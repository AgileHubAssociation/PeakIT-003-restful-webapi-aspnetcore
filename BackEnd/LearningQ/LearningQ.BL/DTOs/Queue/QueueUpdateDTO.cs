using System;
using System.ComponentModel.DataAnnotations;

namespace LearningQ.BL.DTOs.Queue
{
    public class QueueUpdate
    {
        [Required]
        public string Name { get; set; }

        public DateTime ModifiedDate { get; } = DateTime.Now;

        [Required]
        public string Description { get; set; }

    }
}
