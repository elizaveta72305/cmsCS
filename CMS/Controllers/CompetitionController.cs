using CMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionController : ControllerBase
    {
        private readonly IMongoCollection<Competition> _competitionCollection;
        private readonly IMongoCollection<TaskModel> _TaskCollection;
        private readonly IMongoCollection<StatusModel> _statusCollection;



        public CompetitionController()
        {
            var dbHost = "localhost";
            var dbName = "cms";
            var connectionString = "mongodb+srv://elizaveta72305:klop666@cluster0.fmr2xrs.mongodb.net/cms";

            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _competitionCollection = database.GetCollection<Competition>("competition");
            _TaskCollection = database.GetCollection<TaskModel>("taskStatic");
            _statusCollection = database.GetCollection<StatusModel>("competitionStatus");

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Competition>>> GetCompetition()
        {
            return await _competitionCollection.Find(Builders<Competition>.Filter.Empty).ToListAsync();
        }


        [HttpGet("{competitionId}")]
        public async Task<ActionResult<Competition>> GetCompetitionById(string competitionId)
        {
            var filterDefinition = Builders<Competition>.Filter.Eq(x => x.CompetitionId, competitionId);
            return await _competitionCollection.Find(filterDefinition).SingleOrDefaultAsync();
        }

        [HttpPost]
        /// we should verify that all the taks that assigned to the competition is already exists in the tasks collections
        public async Task<ActionResult> CreateCompetition(Competition competition)
        {
            foreach (var currnetTask in competition.ListOfTasks)
            {
                string propertyNameInJsonCollection = "name";
                var filter = Builders<TaskModel>.Filter.Eq(propertyNameInJsonCollection, currnetTask);
                // check if this task    //const
                var task = await (await _TaskCollection.FindAsync(filter)).FirstOrDefaultAsync();
                if(task == null)
                {
                    return BadRequest();
                }
            }
            if(competition.Status is not null)
            {
                string propertyInJsonCollection = "status";
                var filt = Builders<StatusModel>.Filter.Eq(propertyInJsonCollection, competition.Status);
                var status = await (await _statusCollection.FindAsync(filt)).FirstOrDefaultAsync();
                if (status == null)
                {
                    return BadRequest();
                }
            }
                // check collection with name task - if name of task is lready exists
                await _competitionCollection.InsertOneAsync(competition);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCompetition(Competition competition)
        { 
            var filterDefentition = Builders<Competition>.Filter.Eq(x => x.CompetitionId, competition.CompetitionId);
            await _competitionCollection.ReplaceOneAsync(filterDefentition, competition);
            return Ok();
        }

        [HttpDelete("{competitionId}")]

        public async Task<ActionResult> DeleteCompetition(string competitionId)
        {
            var filterDefinition = Builders<Competition>.Filter.Eq(x => x.CompetitionId, competitionId);
            await _competitionCollection.DeleteOneAsync(filterDefinition);
            return Ok();
        }


    }
}

