using System.ComponentModel.DataAnnotations;

namespace LearningQ.BL.Models
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string URL { get; set; }

        public Difficulty Difficulty { get; set; }

        public int Priority { get; set; }

        public float Progress { get; set; }

        public string Notes { get; set; }
    }
}
