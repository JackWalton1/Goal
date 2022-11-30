using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Blob;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace AzureFunctions
{
    public static class UpdateEvent
    {
        [FunctionName("UpdateEvent")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string connectionString = "mongodb+srv://cchannui:cchannui@cluster0.zjtdqmq.mongodb.net/test";
            string databaseName = "sports_app";
            string collectionName = "events";

            // Establish connection to MongoDB.
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<EventModel>(collectionName);

            // Deserialize JSON event information from HTTP POST request body.
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            // Extract updated event information from JSON.
            string new_location = data.Location;
            string new_date = data.Date;
            string new_description = data.Description;

            // Retrieve event ID from query.
            string submitted_id = req.Query["id"];

            // Establish filter and update definitions.
            var filterDefinition = Builders<EventModel>.Filter.Eq(document => document.Id, submitted_id);
            var updateDefinition = Builders<EventModel>.Update
                .Set(document => document.Location, new_location)
                .Set(document => document.Date, new_date)
                .Set(document => document.Description, new_description);

            var optionsDefinition = new FindOneAndUpdateOptions<EventModel, EventModel>()
            {
                ReturnDocument = ReturnDocument.After
            };

            // Find and update event information.
            var result = collection.FindOneAndUpdate(filterDefinition, updateDefinition, optionsDefinition);

            if (result != null)
            {
                return new OkObjectResult(result);
            } else
            {
                // Event update failed.
                return new StatusCodeResult(400);
            }
        }
    }
}

