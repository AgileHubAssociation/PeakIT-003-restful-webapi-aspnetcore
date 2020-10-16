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
    [Route("api/queue/{queueId:int:min(1)}/item")]
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
                return NotFound("Queues does not exist!");
            }

            var itemsFromQueue = _repo.GetAllItemsFromQueue(queueId);

            var itemForDisplay = _mapper.Map<IEnumerable<ItemRead>>(itemsFromQueue);

            return Ok(itemForDisplay);
        }

        // api/queue/5/item/7 
        [HttpGet("{itemId:int:min(1)}", Name = nameof(GetItem))]
        public ActionResult<ItemRead> GetItem(int queueId, int itemId)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound("Queues does not exist!");
            }

            var itemFromQueue = _repo.GetItemFomQueueById(queueId, itemId);

            if (itemFromQueue == null)
            {
                return NotFound();
            }

            var itemForDisplay = _mapper.Map<ItemRead>(itemFromQueue);

            return Ok(itemForDisplay);
        }

        // api/queue/5/item/
        [HttpPost]
        public ActionResult CreateItem(int queueId, ItemCreate item)  //TODO: return the created item
        {
            var itemToAdd = _mapper.Map<Item>(item); // destination <- source

            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound("Queues does not exist!");
            }

            _repo.AddItemInQueue(queueId, itemToAdd);
            _repo.SaveChanges();

            var itemReadDto = _mapper.Map<ItemRead>(itemToAdd);
            
            return CreatedAtRoute(nameof(GetItem), new {queueId = itemReadDto.Id, itemId = itemReadDto.Id}, itemReadDto);
        }

        // api/queue/5/item/7 
        [HttpPut("{itemId:int:min(1)}")]
        public ActionResult UpdateItem(int queueId, int itemId, ItemUpdate item)
        {
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

        [HttpPatch("{itemId:int:min(1)}")]
        public ActionResult PartialUpdateIem(int queueId, int itemId, JsonPatchDocument<ItemUpdate> patchDoc)
        {
            
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound("Queues does not exist!");
            }

            var itemFromRepo = _repo.GetItemFomQueueById(queueId, itemId);

            if (itemFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<ItemUpdate>(itemFromRepo);

            patchDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, itemFromRepo); //source -> destination

            _repo.UpdateItemInQueue(itemId, itemFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        // api/queue/5/item/7
        [HttpDelete("{itemId:int:min(1)}")]
        public ActionResult DeleteQueue(int queueId, int itemId)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound("Queues does not exist!");
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
