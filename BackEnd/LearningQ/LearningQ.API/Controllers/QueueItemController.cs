using System.Collections;
using System.Collections.Generic;
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
        public ActionResult<IEnumerable<Item>> GetItems(int queueId)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            var allItemFromQueueRepo = _repo.GetAllItemsFromQueue(queueId);

            return Ok(allItemFromQueueRepo);
        }

        // api/queue/5/item/7 
        [HttpGet("{itemId}")]
        public ActionResult GetItem(int queueId, int itemId)
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

            return Ok(itemFromQueueRepo);
        }

        // api/queue/5/item/
        [HttpPost]
        public ActionResult CreateItem(int queueId, Item item) //TODO: return created entity
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            _repo.AddItemInQueue(queueId, item);

            return NoContent();
        }

        // api/queue/5/item/7 
        [HttpPut("{itemId}")]
        public ActionResult UpdateItem(int queueId, int itemId, Item item)
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

            _repo.UpdateItemInQueue(queueId, item);

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
