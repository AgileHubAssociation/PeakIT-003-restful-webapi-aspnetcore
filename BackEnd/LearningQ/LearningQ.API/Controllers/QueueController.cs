using System.Collections.Generic;
using AutoMapper;
using LearningQ.BL.DTOs.Queue;
using Microsoft.AspNetCore.Mvc;
using LearningQ.BL.Models;
using LearningQ.DAL.Repository;
using Microsoft.AspNetCore.JsonPatch;

namespace LearningQ.API.Controllers
{

    [ApiController]
    [Route("api/queue")]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class QueueController : ControllerBase
    {

        // read-only fields can only be assigned inside constructors
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public QueueController(IRepository repo, IMapper mapper) // constructor dependency injection
        {
            _repo = repo;
            _mapper = mapper;
        }


        // api/queue
        [HttpGet]
        public ActionResult<IEnumerable<QueueRead>> GetQueues()
        {
            var queuesFromRepo = _repo.GetAllQueues();

            var queuesForDisplay = _mapper.Map<IEnumerable<QueueRead>>(queuesFromRepo);

            return Ok(queuesForDisplay);
        }

        // api/queue/5
        [HttpGet("{queueId}")]
        public ActionResult<QueueRead> GetQueue(int queueId)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            var queForDisplay = _mapper.Map<QueueRead>(queueFromRepo);

            return Ok(queForDisplay);
        }

        // api/queue/
        [HttpPost]
        public ActionResult CreateQueue(QueueCreate queue) //TODO: return created entity
        {
            var queueToAdd = _mapper.Map<Queue>(queue); // destination <- source

            _repo.AddQueue(queueToAdd);
            _repo.SaveChanges();

            return NoContent();
        }

        // api/queue/5
        [HttpPut("{queueId}")]
        public ActionResult UpdateQueue(int queueId, QueueUpdate queue)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(queue, queueFromRepo); //source -> destination

            _repo.UpdateQueue(queueFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1#sources
        /// </summary>
        /// <param name="queueId"></param>
        /// <param name="queue"></param>
        /// <returns></returns>
        // api/queue/5/items
        [HttpPut("{queueId}/includeItems")]
        public ActionResult UpdateQueueWithItems([FromRoute] int queueId, [FromBody] QueueUpdateWithItems queue)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(queue, queueFromRepo); //source -> destination

            _repo.UpdateQueue(queueFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

        // api/queue/5/items
        [HttpPatch("{queueId}")]
        public ActionResult PartialUpdateQueue([FromRoute] int queueId, JsonPatchDocument<QueueUpdate> patchDoc)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            var queueToPatch = _mapper.Map<QueueUpdate>(queueFromRepo);

            patchDoc.ApplyTo(queueToPatch, ModelState);

            if (!TryValidateModel(queueToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(queueToPatch, queueFromRepo); //source -> destination

            _repo.UpdateQueue(queueFromRepo);
            _repo.SaveChanges();

            return NoContent();
        }

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
            _repo.SaveChanges();

            return NoContent();
        }


        [Route("/demostatus")] // override route
        [HttpHead]
        public ActionResult RandomCodeTest()
        {
            return StatusCode(424, new { someKey = "someValue" });
        }

    }
}
