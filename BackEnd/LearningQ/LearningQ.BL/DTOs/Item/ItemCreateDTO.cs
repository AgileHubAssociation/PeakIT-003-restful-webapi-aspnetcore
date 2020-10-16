using System;
using System.ComponentModel.DataAnnotations;

namespace LearningQ.BL.DTOs.Item
{
    public class ItemCreate
    {
        [Required]
        public string Name { get; set; }
        public DateTime CreateDate { get; } = DateTime.Now;
        public DateTime ModifiedDate { get; } = DateTime.Now;

        public string Description { get; set; }
        public string URL { get; set; }
        public Difficulty Difficulty { get; set; }
        public int Priority { get; set; }
        public float Progress { get; set; }
    }
}
