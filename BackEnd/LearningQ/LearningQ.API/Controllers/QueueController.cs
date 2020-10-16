using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using LearningQ.BL.Models;
using LearningQ.DAL.Repository;

namespace LearningQ.API.Controllers
{

    [ApiController]
    [Route("api/queue")]
    public class QueueController : ControllerBase
    {

        // read-only fields can only be assigned inside constructors
        private readonly IRepository _repo;

        public QueueController(IRepository repo) // constructor dependency injection
        {
            _repo = repo;
        }

        // api/queue
        [HttpGet]
        public ActionResult<IEnumerable<Queue>> GetQueues()
        {
            var queuesFromRepo = _repo.GetAllQueues();

            return Ok(queuesFromRepo);
        }

        // api/queue/5
        [HttpGet("{queueId}")]
        public ActionResult<Queue> GetQueue(int queueId)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            return Ok(queueFromRepo);
        }

        // api/queue/
        [HttpPost]
        public ActionResult CreateQueue(Queue queue) //TODO: return created entity
        {
            _repo.AddQueue(queue);

            return NoContent();
        }

        // api/queue/5
        [HttpPut("{queueId}")]
        public ActionResult UpdateQueue(int queueId, Queue queue)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            _repo.UpdateQueue(queue);

            return NoContent();
        }

        // api/queue/5/items
        [HttpPut("{queueId}/includeItems")]
        public ActionResult UpdateQueueWithItems([FromRoute] int queueId, Queue queue)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            _repo.UpdateQueue(queue);

            return NoContent();
        }

        //TODO: implement PATCH

        // api/queue/5
        [HttpDelete("{queueId}")]
        public ActionResult DeleteQueue(int queueId)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            _repo.DeleteQueue(queueFromRepo);

            return NoContent();
        }

    }
}
