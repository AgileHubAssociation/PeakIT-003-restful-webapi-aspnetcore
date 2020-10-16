using Microsoft.AspNetCore.Mvc;

namespace LearningQ.API.Controllers
{
    [ApiController]
    [Route("api/queue/{queueId}/item")]
    public class ItemController : ControllerBase
    {

        // api/queue/5/item
        [HttpGet]
        public ActionResult GetItems(int queueId)
        {
            return Ok($"all item from queue {queueId}");
        }

        // api/queue/5/item/7 
        [HttpGet("{itemId}")]
        public ActionResult GetItem(int queueId, int itemId)
        {
            return Ok($"item {itemId} from queue {queueId}");
        }

        // api/queue/5/item/
        [HttpPost]
        public ActionResult CreateItem(int queueId, object item) //TODO: return created entity
        {
            return NoContent();
        }

        // api/queue/5/item/7 
        [HttpPut("{itemId}")]
        public ActionResult UpdateItem(int queueId, int itemId, object item)
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
