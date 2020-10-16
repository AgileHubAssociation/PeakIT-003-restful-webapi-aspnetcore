using AutoMapper;
using LearningQ.BL.DTOs.Item;
using LearningQ.BL.DTOs.Queue;
using LearningQ.BL.Models;

namespace LearningQ.API.Profiles
{
    public class QueueProfile : Profile
    {
        // Mappings go both ways for simplicity
        public QueueProfile()
        {
            //Source -> Target
            CreateMap<Queue, QueueRead>().ReverseMap();
            CreateMap<QueueCreate, Queue>().ReverseMap();
            CreateMap<QueueUpdate, Queue>().ReverseMap();
            CreateMap<QueueUpdateWithItems, Queue>().ReverseMap();

            CreateMap<Item, ItemRead>().ReverseMap();
            CreateMap<ItemCreate, Item>().ReverseMap();
            CreateMap<ItemUpdate, Item>().ReverseMap();
        }

    }
}
