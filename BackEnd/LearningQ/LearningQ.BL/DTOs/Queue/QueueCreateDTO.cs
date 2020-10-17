using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using LearningQ.BL.DTOs.Item;

namespace LearningQ.BL.DTOs.Queue
{
    public class QueueCreate
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

        public List<ItemCreate> Items { get; set; } = new List<ItemCreate>();

    }
}
