using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearningQ.BL.DTOs.Item
{
    public class ItemUpdate
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [JsonIgnore]
        public DateTime ModifiedDate { get; } = DateTime.Now;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        [Range(1,3)]
        public Difficulty Difficulty { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        [Range(0,100)]
        public float Progress { get; set; }

        [MaxLength(5000)]
        public string Notes { get; set; }

    }
}
