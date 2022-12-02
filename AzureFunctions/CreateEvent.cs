using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;

namespace AzureFunctions
{
    public static class CreateEvent
    {
        [FunctionName("CreateEvent")]
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

            // Deserialize JSON user credentials from HTTP POST request body.
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);


            // Create new EventModel object.
            var eventModel = new EventModel {
                Title = data.Title,
                Icon = data.Icon,
                ImageURL = data.ImageURL,
                VenueName = data.VenueName,
                VenueAddress = data.VenueAddress,
                Date = data.Date,
                Description = data.Description,
                FollowerCount = data.FollowerCount
            };

            // Insert UserModel object into MongoDB.
            if (eventModel.Title != null || eventModel.Icon != null || eventModel.ImageURL != null || eventModel.VenueName != null || 
                eventModel.VenueAddress != null || eventModel.Date != null || eventModel.Description != null)
            {
                await collection.InsertOneAsync(eventModel);
            }

            // Response for testing purposes.
            string responseMessage = "Event created successfully.";
            return new OkObjectResult(responseMessage);
        }
    }
}

