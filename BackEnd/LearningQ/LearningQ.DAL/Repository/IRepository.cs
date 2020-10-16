using LearningQ.BL.Models;
using System.Collections.Generic;

namespace LearningQ.DAL.Repository
{
    public interface IRepository
    {

        #region Queues
        IEnumerable<Queue> GetAllQueues();
        Queue GetQueueById(int id);
        void AddQueue(Queue queue);
        void UpdateQueue(Queue queue);
        void DeleteQueue(Queue queue);
        #endregion

        #region Items
        IEnumerable<Item> GetAllItemsFromQueue(int queueId);
        Item GetItemFomQueueById(int queueId, int itemId);
        void AddItemInQueue(int queueId, Item item);
        void UpdateItemInQueue(int queueId, Item item);
        void DeleteItemFromQueue(int queueId, Item item);
        #endregion

        public bool SaveChanges();

    }
}