using LearningQ.BL;
using LearningQ.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable RedundantAssignment (just for the sake of this mock)

namespace LearningQ.DAL.Repository
{
    public class MockRepository : IRepository
    {
        private readonly List<Queue> _queues;

        public MockRepository()
        {
            _queues = new List<Queue>
            {
                new Queue
                {
                    Id = 1,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Name = "Leaning C# Basics",
                    Description = "Evergreen C# books",
                    Items = new List<Item>
                    {
                        new Item
                        {
                            Id = 1,
                            CreateDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            Name = "C# in Depth",
                            Description = "Probably the best C# book",
                            Difficulty = Difficulty.Hard,
                            Priority = 2,
                            URL = "https://www.libris.ro/c-in-depth-4e-BRT9781617294532--p10934198.html",
                            Progress = 5.5f
                        },
                        new Item
                        {
                            Id = 2,
                            CreateDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            Name = "CLR via C#",
                            Description = "Another good C# book",
                            Difficulty = Difficulty.Intermediate,
                            Priority = 1,
                            URL = "https://www.amazon.com/CLR-via-4th-Developer-Reference/dp/0735667454",
                            Progress = 15f
                        },
                    }
                }
            };
        }

        #region Queues

        public IEnumerable<Queue> GetAllQueues()
        {
            return _queues;
        }

        public Queue GetQueueById(int id)
        {
            return _queues.FirstOrDefault(t => t.Id == id);
        }

        public void AddQueue(Queue queue)
        {
            //increment the id of the new queue
            queue.Id = _queues.Max(t => t.Id) + 1;

            //increment the id of the new items
            queue
                .Items
                .ForEach(t => 
                    t.Id = _queues
                            .SelectMany(x => x.Items)
                            .Max(m => m.Id) + queue.Items.IndexOf(t) + 1);

            _queues.Add(queue);

        }

        public void UpdateQueue(Queue queue)
        {
            _queues[_queues.FindIndex(t => t.Id == queue.Id)] = queue;
        }

        public void DeleteQueue(Queue queue)
        {
            _queues.Remove(queue);
        }
        #endregion


        #region Items
        public IEnumerable<Item> GetAllItemsFromQueue(int queueId)
        {
            return _queues.FirstOrDefault(t => t.Id == queueId)?.Items;
        }

        public Item GetItemFomQueueById(int queueId, int itemId)
        {
            return _queues
                .FirstOrDefault(t => t.Id == queueId)
                ?.Items
                ?.FirstOrDefault(t => t.Id == itemId);
        }

        public void AddItemInQueue(int queueId, Item item)
        {
            item.Id = _queues
                .SelectMany(t => t.Items)
                .Max(t => t.Id) + 1;

            _queues
                .FirstOrDefault(t => t.Id == queueId)
                ?.Items
                ?.Add(item);

        }

        public void UpdateItemInQueue(int queueId, Item item)
        {
            var itemToUpdate =
                _queues
                    .FirstOrDefault(t => t.Id == queueId)
                    ?.Items
                    ?.FirstOrDefault(t => t.Id == item.Id);

            if (itemToUpdate == null)
            { 
                throw new NullReferenceException("item was not found");
            }

            itemToUpdate = item;
        }

        public void DeleteItemFromQueue(int queueId, Item item)
        {
            _queues
                .FirstOrDefault(t => t.Id == queueId)
                ?.Items
                ?.Remove(item);
        }
        #endregion

        public bool SaveChanges()
        {
           //this does nothing but was added to respect the interface
           return true;
        }

    }

}
