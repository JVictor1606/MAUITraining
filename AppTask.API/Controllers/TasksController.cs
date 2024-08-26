using AppTask.DataBase.Repositories;
using AppTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITaskModelRepository _repository;

        public TasksController(ITaskModelRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll(Guid userId)
        {
            var user =_repository.GetAll(userId);

            if (user == null)
                return NotFound();
            return Ok(user);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var task = _repository.GetById(id);
            if (task == null) 
                return NotFound();
            return Ok(task);
        }


        [HttpPost]
        public IActionResult Add(TaskModel task)
        {
            //Validações
            _repository.Add(task);
            return Ok(task);
        }

        [HttpPut]
        public IActionResult Update(TaskModel task)
        {
            _repository.Update(task);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id) 
        {
            var task = _repository.GetById(id);
            _repository.Delete(task);
            return Ok();
        }


        [HttpPost("batchPush")]
        public IActionResult BatchPush(List<TaskModel> tasks)
        {
            foreach(var taskClient in tasks)
            {
                var taskServer = _repository.GetById(taskClient.Id);

                if(taskServer == null)
                {
                    _repository.Add(taskClient);
                }
                else
                {
                    if(taskClient.Update > taskServer.Update)
                    {
                        _repository.Update(taskClient);
                    }
                }
            }

            return Ok();
        }
    }
}
