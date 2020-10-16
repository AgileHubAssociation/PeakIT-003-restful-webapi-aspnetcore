using System.Collections.Generic;
using System.Linq;
using LearningQ.BL.DTOs.Item;
using LearningQ.BL.Models;
using LearningQ.DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LearningQ.API.Controllers
{
    [ApiController]
    [Route("api/queue/{queueId}/item")]
    public class ItemController : ControllerBase
    {
        // read-only fields can only be assigned inside constructors
        private readonly IRepository _repo;

        public ItemController(IRepository repo) // constructor dependency injection
        {
            _repo = repo;
        }

        // api/queue/5/item
        [HttpGet]
        public ActionResult<IEnumerable<ItemRead>> GetItems(int queueId)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }
            
            var allItemFromQueueRepo = _repo.GetAllItemsFromQueue(queueId);


            var itemsFromQueForDisplay =
                allItemFromQueueRepo
                    .Select(t => new ItemRead
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Description = t.Description,
                        Difficulty = t.Difficulty,
                        URL = t.URL,
                    })
                    .ToList();


            return Ok(itemsFromQueForDisplay);
        }

        // api/queue/5/item/7 
        [HttpGet("{itemId}")]
        public ActionResult<ItemRead> GetItem(int queueId, int itemId)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            var itemFromQueueRepo = _repo.GetItemFomQueueById(queueId, itemId);

            if (itemFromQueueRepo == null)
            {
                return NotFound();
            }

            var itemFromQueueForDisplay = new ItemRead
            {
                Id = itemFromQueueRepo.Id,
                Name = itemFromQueueRepo.Name,
                Description = itemFromQueueRepo.Description,
                CreateDate = itemFromQueueRepo.CreateDate,
                ModifiedDate = itemFromQueueRepo.ModifiedDate,
                Difficulty = itemFromQueueRepo.Difficulty,
                Priority = itemFromQueueRepo.Priority,
                Progress = itemFromQueueRepo.Progress,
                URL = itemFromQueueRepo.URL,
            };

            return Ok(itemFromQueueForDisplay);
        }

        // api/queue/5/item/
        [HttpPost]
        public ActionResult CreateItem(int queueId, ItemCreate item) //TODO: return created entity
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            var itemToAdd = new Item
            {
                Name = item.Name,
                CreateDate = item.CreateDate,
                ModifiedDate = item.ModifiedDate,
                Description = item.Description,
                URL = item.URL,
                Priority = item.Priority,
                Progress = item.Progress,
                Difficulty = item.Difficulty

            };

            _repo.AddItemInQueue(queueId, itemToAdd);

            return NoContent();
        }

        // api/queue/5/item/7 
        [HttpPut("{itemId}")]
        public ActionResult UpdateItem(int queueId, int itemId, ItemUpdate item)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            var itemFromQueueRepo = _repo.GetItemFomQueueById(queueId, itemId);

            if (itemFromQueueRepo == null)
            {
                return NotFound();
            }

            itemFromQueueRepo.Name = item.Name;
            itemFromQueueRepo.Description = item.Description;
            itemFromQueueRepo.ModifiedDate = item.ModifiedDate;
            itemFromQueueRepo.Priority = item.Priority;
            itemFromQueueRepo.Progress = item.Progress;

            _repo.UpdateItemInQueue(queueId, itemFromQueueRepo);

            return NoContent();
        }

        //TODO: implement PATCH

        // api/queue/5/item/7
        [HttpDelete("{itemId}")]
        public ActionResult DeleteQueue(int queueId, int itemId)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            var itemFromQueueRepo = _repo.GetItemFomQueueById(queueId, itemId);

            if (itemFromQueueRepo == null)
            {
                return NotFound();
            }

            _repo.DeleteItemFromQueue(queueId, itemFromQueueRepo);

            return NoContent();
        }

    }
}
