using System;
using LearningQ.BL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LearningQ.DAL.Repository
{
    public class SQLiteRepository : IRepository
    {
        private readonly QueueDbContext _context;

        public SQLiteRepository(QueueDbContext context) //Dependency injection using concrete implementation
        {
            _context = context;
        }

        #region Queues

        public IEnumerable<Queue> GetAllQueues()
        {
            return _context.Queues.ToList();
        }

        public Queue GetQueueById(int id)
        {
            return _context.Queues.FirstOrDefault(t => t.Id == id);
        }

        public void AddQueue(Queue queue)
        {
            _context.Queues.Add(queue);
        }

        public void UpdateQueue(Queue queue)
        {
            //ef does this automatically
        }

        public void DeleteQueue(Queue queue)
        {
            _context.Queues.Remove(queue);
        }
        #endregion


        #region Items
        public IEnumerable<Item> GetAllItemsFromQueue(int queueId)
        {
            return
                _context
                .Queues
                .Include(t => t.Items)
                .FirstOrDefault(t => t.Id == queueId)
                ?.Items;
        }

        public Item GetItemFomQueueById(int queueId, int itemId)
        {
            var queue = _context
                .Queues
                .Include(t => t.Items)
                .FirstOrDefault(t => t.Id == queueId)
                ?? throw new NullReferenceException("DB Error: DB Error: Queue does not exist");

            return
                queue
                .Items
                ?.FirstOrDefault(t => t.Id == itemId);
        }

        public void AddItemInQueue(int queueId, Item item)
        {
            var queue = _context
                            .Queues
                            .Include(t => t.Items)
                            .FirstOrDefault(t => t.Id == queueId)
                        ?? throw new NullReferenceException("DB Error: DB Error: Queue does not exist");

            queue
                .Items
                .Add(item);
        }

        public void UpdateItemInQueue(int queueId, Item item)
        {
            // ef does this automatically
        }

        public void DeleteItemFromQueue(int queueId, Item item)
        {
            var queue = _context
                            .Queues
                            .Include(t => t.Items)
                            .FirstOrDefault(t => t.Id == queueId)
                        ?? throw new NullReferenceException("DB Error: DB Error: Queue does not exist");

            queue
                .Items
                .Remove(item);
        }
        #endregion

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

    }

}
