using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LearningQ.BL.DTOs.Item
{
    public class ItemCreate
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [JsonIgnore]
        public DateTime CreateDate { get; } = DateTime.Now;
        [JsonIgnore]
        public DateTime ModifiedDate { get; } = DateTime.Now;

        [MaxLength(500)]
        public string Description { get; set; }

        public string URL { get; set; }

        [Range(1,3)]
        public Difficulty Difficulty { get; set; }

        public int Priority { get; set; }

        [Range(0,100)]
        public float Progress { get; set; }

        [MaxLength(5000)]
        public string Notes { get; set; }
    }
}
