using System.Collections.Generic;
using AutoMapper;
using LearningQ.BL.DTOs.Item;
using LearningQ.BL.Models;
using LearningQ.DAL.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace LearningQ.API.Controllers
{
    [ApiController]
    [Route("api/queue/{queueId}/item")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ItemController : ControllerBase
    {
        // read-only fields can only be assigned inside constructors
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public ItemController(IRepository repo, IMapper mapper) // constructor dependency injection
        {
            _repo = repo;
            _mapper = mapper;
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

            var itemsFromQueForDisplay = _mapper.Map<IEnumerable<ItemRead>>(allItemFromQueueRepo);

            return Ok(itemsFromQueForDisplay);
        }

        // api/queue/5/item/7 
        [HttpGet("{itemId}", Name = nameof(GetItem))]
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

            var itemFromQueueForDisplay = _mapper.Map<ItemRead>(itemFromQueueRepo);

            return Ok(itemFromQueueForDisplay);
        }

        // api/queue/5/item/
        [HttpPost]
        public ActionResult<ItemRead> CreateItem(int queueId, ItemCreate item)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            var itemToAdd = _mapper.Map<Item>(item); // destination <- source

            _repo.AddItemInQueue(queueId, itemToAdd);
            _repo.SaveChanges();

            var itemReadDto = _mapper.Map<ItemRead>(itemToAdd);
            
            return CreatedAtRoute(nameof(GetItem), new {queueId = itemReadDto.Id, itemId = itemReadDto.Id}, itemReadDto);
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

            _mapper.Map(item, itemFromQueueRepo); //source -> destination

            _repo.UpdateItemInQueue(queueId, itemFromQueueRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        // api/queue/5/item/7 
        [HttpPatch("{itemId}")]
        public ActionResult PartialUpdateIem(int queueId, int itemId, JsonPatchDocument<ItemUpdate> patchDoc)
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

            var itemToPatch = _mapper.Map<ItemUpdate>(itemFromQueueRepo);

            patchDoc.ApplyTo(itemToPatch, ModelState);

            if (!TryValidateModel(itemToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(itemToPatch, itemFromQueueRepo); //source -> destination

            _repo.UpdateItemInQueue(queueId, itemFromQueueRepo);
            _repo.SaveChanges();

            return NoContent();
        }

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
            _repo.SaveChanges();

            return NoContent();
        }

    }
}
