using System.Collections;
using System.Collections.Generic;
using LearningQ.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningQ.API.Controllers
{
    [ApiController]
    [Route("api/queue/{queueId}/item")]
    public class ItemController : ControllerBase
    {

        // api/queue/5/item
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems(int queueId)
        {
            var result = new List<Item>();

            return Ok(result);
        }

        // api/queue/5/item/7 
        [HttpGet("{itemId}")]
        public ActionResult GetItem(int queueId, int itemId)
        {
            
            var result = new Item();

            return Ok(result);
        }

        // api/queue/5/item/
        [HttpPost]
        public ActionResult CreateItem(int queueId, Item item) //TODO: return created entity
        {
            return NoContent();
        }

        // api/queue/5/item/7 
        [HttpPut("{itemId}")]
        public ActionResult UpdateItem(int queueId, int itemId, Item item)
        {
            return NoContent();
        }

        //TODO: implement PATCH

        // api/queue/5/item/7
        [HttpDelete("{itemId}")]
        public ActionResult DeleteQueue(int queueId, int itemId)
        {
            return NoContent();
        }

    }
}
