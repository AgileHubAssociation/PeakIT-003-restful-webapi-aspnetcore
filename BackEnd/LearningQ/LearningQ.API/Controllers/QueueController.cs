using System.Collections.Generic;
using System.Linq;
using LearningQ.BL.DTOs.Item;
using LearningQ.BL.DTOs.Queue;
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
        public ActionResult<IEnumerable<QueueRead>> GetQueues()
        {
            var queuesFromRepo = _repo.GetAllQueues();

            var queuesForDisplay = queuesFromRepo.Select(t => new QueueRead
            {
                Id = t.Id,
                Description = t.Description,
                CreateDate = t.CreateDate,
                ModifiedDate = t.ModifiedDate,
                Name = t.Name,
                Items = t.Items.Select(t => new ItemRead
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    Difficulty = t.Difficulty,
                    URL = t.URL,
                    Priority = t.Priority,
                    Progress = t.Progress,
                    CreateDate = t.CreateDate,
                    ModifiedDate = t.ModifiedDate,
                }).ToList()
            }).ToList();


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

            var queForDisplay = new QueueRead
            {
                Id = queueFromRepo.Id,
                Name = queueFromRepo.Name,
                CreateDate = queueFromRepo.CreateDate,
                ModifiedDate = queueFromRepo.ModifiedDate,
                Description = queueFromRepo.Description,
                Items = queueFromRepo.Items.Select(t => new ItemRead
                {
                    Id = t.Id,
                    CreateDate = t.CreateDate,
                    ModifiedDate = t.ModifiedDate,
                    Name = t.Name,
                    Description = t.Description,
                    Difficulty = t.Difficulty,
                    URL = t.URL,
                    Priority = t.Priority,
                    Progress = t.Progress,

                }).ToList()

            };


            return Ok(queForDisplay);
        }

        // api/queue/
        [HttpPost]
        public ActionResult CreateQueue(QueueCreate queue) //TODO: return created entity
        {

            var queueToAdd = new Queue
            {
                Name = queue.Name,
                CreateDate = queue.CreateDate,
                ModifiedDate = queue.ModifiedDate,
                Description = queue.Description,
                Items = queue.Items.Select(t => new Item
                {
                    Name = t.Name,
                    Description = t.Description,
                    Difficulty = t.Difficulty,
                    URL = t.URL,
                    Priority = t.Priority,
                    Progress = t.Progress,
                }).ToList()
            };

            _repo.AddQueue(queueToAdd);

            return NoContent();
        }

        // api/queue/5
        [HttpPut("{queueId}")]
        public ActionResult UpdateQueue([FromRoute] int queueId, [FromBody] QueueUpdate queue)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            queueFromRepo.Name = queue.Name;
            queueFromRepo.ModifiedDate = queue.ModifiedDate;
            queueFromRepo.Description = queue.Description;

            _repo.UpdateQueue(queueFromRepo);

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
        public ActionResult UpdateQueueWithItems([FromRoute] int queueId, QueueUpdateWithItems queue)
        {
            var queueFromRepo = _repo.GetQueueById(queueId);

            if (queueFromRepo == null)
            {
                return NotFound();
            }

            queueFromRepo.Name = queue.Name;
            queueFromRepo.ModifiedDate = queue.ModifiedDate;
            queueFromRepo.Description = queue.Description;
            queueFromRepo.Items = queue.Items.Select(t => new Item
            {
                ModifiedDate = t.ModifiedDate,
                Name = t.Name,
                Description = t.Description,
                Difficulty = t.Difficulty,
                URL = t.URL,
                Priority = t.Priority,
                Progress = t.Progress,

            }).ToList();


            _repo.UpdateQueue(queueFromRepo);

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

        [Route("/demostatus")] // override route
        [HttpHead]
        public ActionResult RandomCodeTest()
        {
            return StatusCode(424, new { someKey = "someValue" });
        }


    }
}
