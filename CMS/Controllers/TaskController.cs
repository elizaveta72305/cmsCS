using CMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace CMS.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly IMongoCollection<TaskModel> _taskCollection;

        public TaskController()
        {
            var dbHost = "localhost";
            var dbName = "cms";
            var connectionString = "mongodb+srv://elizaveta72305:klop666@cluster0.fmr2xrs.mongodb.net/cms";

            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _taskCollection = database.GetCollection<TaskModel>("tasks");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            return await _taskCollection.Find(Builders<TaskModel>.Filter.Empty).ToListAsync();
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<TaskModel>> GetTaskById(string taskId)
        {
            var filterDefinition = Builders<TaskModel>.Filter.Eq(x => x.TaskId, taskId);
            return await _taskCollection.Find(filterDefinition).SingleOrDefaultAsync();
        }

        [HttpPost]

        public async Task<ActionResult> CreateTask(TaskModel taskpost)
        {
            await _taskCollection.InsertOneAsync(taskpost);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTask(TaskModel task)
    {
            var filterDefentition = Builders<TaskModel>.Filter.Eq(x => x.TaskId, task.TaskId);
            await _taskCollection.ReplaceOneAsync(filterDefentition, task);
            return Ok();
        }

        [HttpDelete("{taskId}")]

        public async Task<ActionResult> DeleteTask(string taskId)
        {
            var filterDefinition = Builders<TaskModel>.Filter.Eq(x => x.TaskId, taskId);
            await _taskCollection.DeleteOneAsync(filterDefinition);
            return Ok();
        }


    }


