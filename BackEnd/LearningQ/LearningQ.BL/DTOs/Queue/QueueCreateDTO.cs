using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LearningQ.BL.DTOs.Item;

namespace LearningQ.BL.DTOs.Queue
{
    public class QueueCreate
    {
        [Required]
        public string Name { get; set; }
        public DateTime CreateDate { get; } = DateTime.Now;
        public DateTime ModifiedDate { get; } = DateTime.Now;

        public string Description { get; set; }

        public List<ItemCreate> Items { get; set; } = new List<ItemCreate>();

    }
}
