using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using LearningQ.BL.DTOs.Item;

namespace LearningQ.BL.DTOs.Queue
{
    public class QueueRead
    {
        public int Id { get; set; }

        [JsonIgnore]
        public DateTime CreateDate { get; set; }

        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }

        //[JsonIgnore]
        public TimeSpan CreateAgo => DateTime.Now.Subtract(CreateDate);

        //[JsonIgnore]
        public TimeSpan ModifiedAgo => DateTime.Now.Subtract(ModifiedDate);

        public string Name { get; set; }
        public string Description { get; set; }

        public List<ItemRead> Items { get; set; } = new List<ItemRead>();
    }


}
