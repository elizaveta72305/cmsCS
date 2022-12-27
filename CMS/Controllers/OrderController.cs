using CMS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserController()
        {
            var dbHost = "localhost";
            var dbName = "cms";
            var connectionString = "mongodb+srv://elizaveta72305:klop666@cluster0.fmr2xrs.mongodb.net/test";

            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _userCollection = database.GetCollection<User>("user");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userCollection.Find(Builders<User>.Filter.Empty).ToListAsync();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserById(string userId)
        {
            var filterDefinition = Builders<User>.Filter.Eq(x => x.UserId, userId);
            return await _userCollection.Find(filterDefinition).SingleOrDefaultAsync();

        }

        [HttpPost]

        public async Task<ActionResult> Create(User user)
        {
            await _userCollection.InsertOneAsync(user);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(User user)
        {
            var filterDefentition = Builders<User>.Filter.Eq(x => x.UserId, user.UserId);
            await _userCollection.ReplaceOneAsync(filterDefentition, user);
            return Ok();
        }

        [HttpDelete("{userId}")]

        public async Task<ActionResult> DeleteUser(string userId)
        {
            var filterDefinition = Builders<User>.Filter.Eq(x => x.UserId, userId);
            await _userCollection.DeleteOneAsync(filterDefinition);
            return Ok();
        }


    }
}
