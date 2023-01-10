
using CMS.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using System;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
namespace CMS.Manager
{

		[Route("manager/[controller]")]
		[ApiController]
	public class CompetitionManager: ControllerBase
    {
		private readonly IMongoCollection<Competition> _competitionCollection;

		public CompetitionManager()
		{
			var connectionString = "mongodb+srv://elizaveta72305:klop666@cluster0.fmr2xrs.mongodb.net/cms";
			var mongoUrl = MongoUrl.Create(connectionString);
			var mongoClient = new MongoClient(mongoUrl);
			var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
			_competitionCollection = database.GetCollection<Competition>("competition");
		}

		[HttpPut]
		public async Task<ActionResult> UpdateCompetitionTask(string competitionName, string[] newTask)
		{
			foreach (var oneTask in newTask)
			{
				Console.WriteLine(oneTask);
				var filterDefentition = Builders<Competition>.Filter.Eq(x => x.Name, competitionName);
				var update = Builders<Competition>.Update.Push(p => p.ListOfTasks, oneTask);
				if (filterDefentition != null)
				{
					await _competitionCollection.UpdateOneAsync(filterDefentition, update);
					return Ok();
				}
			}
			return BadRequest();
		}

	}
}
