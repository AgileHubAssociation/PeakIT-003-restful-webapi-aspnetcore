using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LearningQ.BL.DTOs.Item;

namespace LearningQ.BL.DTOs.Queue
{
    public class QueueUpdateWithItems
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime ModifiedDate { get; } = DateTime.Now;

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public List<ItemUpdate> Items { get; set; } = new List<ItemUpdate>();
    }
}
