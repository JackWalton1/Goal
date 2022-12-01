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
    public static class FetchAllEvents
    {
        [FunctionName("FetchAllEvents")]
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

            var events = await collection.Find(_ => true).ToListAsync();

            // Validate response.
            if (events != null)
            {
                return new OkObjectResult(events);
            } else
            {
                // Return 403 Forbidden status code
                return new StatusCodeResult(403);
            }
        }
    }
}

