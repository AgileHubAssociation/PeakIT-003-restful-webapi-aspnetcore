using System;
using System.Text.Json.Serialization;

namespace LearningQ.BL.DTOs.Item
{
    public class ItemRead
    {
        public int Id { get; set; }

        [JsonIgnore]
        public DateTime CreateDate { get; set; }

        [JsonIgnore]
        public DateTime ModifiedDate { get; set; }

        [JsonIgnore]
        public TimeSpan CreateAgo => DateTime.Now.Subtract(CreateDate);

        [JsonIgnore]
        public TimeSpan ModifiedAgo => DateTime.Now.Subtract(ModifiedDate);

        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public Difficulty Difficulty { get; set; }
        public int Priority { get; set; }
        public float Progress { get; set; }
    }
}
