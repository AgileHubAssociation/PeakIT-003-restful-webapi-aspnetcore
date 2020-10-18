using Microsoft.AspNetCore.Mvc;

namespace LearningQ.API.Controllers
{

    [ApiController]
    [Route("api/queue")]
    public class QueueController : ControllerBase
    {

        // api/queue
        [HttpGet]
        public ActionResult GetQueues()
        {
            return Ok("all the queues");
        }

        // api/queue/5
        [HttpGet("{queueId}")]
        public ActionResult GetQueue(int queueId)
        {
            return Ok($"queue with id {queueId}");
        }

        // api/queue/
        [HttpPost]
        public ActionResult CreateQueue(object queue) //TODO: return created entity
        {
            return NoContent();
        }

        // api/queue/5
        [HttpPut("{queueId}")]
        public ActionResult UpdateQueue(int queueId, object queue)
        {
            return NoContent();
        }

        // api/queue/5/items
        [HttpPut("{queueId}/includeItems")]
        public ActionResult UpdateQueueWithItems([FromRoute] int queueId, object queue)
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


        [HttpHead]
        public ActionResult RandomCodeTest()
        {
            return StatusCode(424, new { someKey = "someValue"});
        }

    }
}
