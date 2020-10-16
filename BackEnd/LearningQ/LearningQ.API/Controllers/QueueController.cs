using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LearningQ.BL.Models;

namespace LearningQ.API.Controllers
{

    [ApiController]
    [Route("api/queue")]
    public class QueueController : ControllerBase
    {

        // api/queue
        [HttpGet]
        public ActionResult<IEnumerable<Queue>> GetQueues()
        {
            var result = new List<Queue>();

            return Ok(result);
        }

        // api/queue/5
        [HttpGet("{queueId}")]
        public ActionResult<Queue> GetQueue(int queueId)
        {
            var result = new Queue();

            return Ok(result);
        }

        // api/queue/
        [HttpPost]
        public ActionResult CreateQueue(Queue queue) //TODO: return created entity
        {
            return NoContent();
        }

        // api/queue/5
        [HttpPut("{queueId}")]
        public ActionResult UpdateQueue(int queueId, Queue queue)
        {
            return NoContent();
        }

        // api/queue/5/items
        [HttpPut("{queueId}/includeItems")]
        public ActionResult UpdateQueueWithItems([FromRoute] int queueId, Queue queue)
        {
            return NoContent();
        }

        //TODO: implement PATCH

        // api/queue/5
        [HttpDelete("{queueId}")]
        public ActionResult DeleteQueue(int queueId)
        {
            return NoContent();
        }

    }
}
