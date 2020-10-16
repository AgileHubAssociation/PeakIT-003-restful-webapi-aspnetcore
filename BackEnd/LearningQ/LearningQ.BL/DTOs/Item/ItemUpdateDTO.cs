using System;
using System.ComponentModel.DataAnnotations;

namespace LearningQ.BL.DTOs.Item
{
    public class ItemUpdate
    {
        [Required]
        public string Name { get; set; }

        public DateTime ModifiedDate { get; } = DateTime.Now;

        [Required]
        public string Description { get; set; }

        [Required]
        public string URL { get; set; }

        [Required]
        public Difficulty Difficulty { get; set; }

        [Required]
        public int Priority { get; set; }

        [Required]
        public float Progress { get; set; }

    }
}
