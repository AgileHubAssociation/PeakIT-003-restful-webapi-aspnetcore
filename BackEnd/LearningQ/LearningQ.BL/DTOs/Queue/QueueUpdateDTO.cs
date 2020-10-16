using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearningQ.BL.DTOs.Queue
{
    public class QueueUpdate
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [JsonIgnore]
        public DateTime ModifiedDate { get; } = DateTime.Now;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

    }
}
