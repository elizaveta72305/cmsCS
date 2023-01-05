using CMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatusController : ControllerBase
    {
        private readonly IMongoCollection<StatusModel> _statusCollection;

        public StatusController()
        {
            var dbHost = "localhost";
            var dbName = "cms";
            var connectionString = "mongodb+srv://elizaveta72305:klop666@cluster0.fmr2xrs.mongodb.net/cms";

            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _statusCollection = database.GetCollection<StatusModel>("competitionStatus");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusModel>>> GetStatus()
        {
            return await _statusCollection.Find(Builders<StatusModel>.Filter.Empty).ToListAsync();
        }

        [HttpGet("{statusId}")]
        public async Task<ActionResult<StatusModel>> GetStatusById(string statusId)
        {
            if (statusId == null)
            {
                return BadRequest();
            }
            var filterDefinition = Builders<StatusModel>.Filter.Eq(x => x.StatusId, statusId);
            if (filterDefinition == null)
            {
                return NotFound();
            }
            return await _statusCollection.Find(filterDefinition).SingleOrDefaultAsync();
        }

        [HttpPost]

        public async Task<ActionResult> CreateStatus(StatusModel statuspost)
        {
            if (statuspost == null)
            {
                return BadRequest();
            }
            await _statusCollection.InsertOneAsync(statuspost);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateStatus(StatusModel task)
        {
            var filterDefentition = Builders<StatusModel>.Filter.Eq(x => x.StatusId, task.StatusId);
            await _statusCollection.ReplaceOneAsync(filterDefentition, task);
            return Ok();
        }

        [HttpDelete("{statusId}")]

        public async Task<ActionResult> DeleteStatus(string statusId)
        {
            var filterDefinition = Builders<StatusModel>.Filter.Eq(x => x.StatusId, statusId);
            await _statusCollection.DeleteOneAsync(filterDefinition);
            return Ok();
        }
    }
}
