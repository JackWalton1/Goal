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
using System.Collections.Generic;

namespace AzureFunctions
{
    public static class UpdateAccount
    {
        [FunctionName("UpdateAccount")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string connectionString = "mongodb+srv://cchannui:cchannui@cluster0.zjtdqmq.mongodb.net/test";
            string databaseName = "sports_app";
            string collectionName = "users";

            // Establish connection to MongoDB.
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            var collection = db.GetCollection<UserModel>(collectionName);

            // Deserialize JSON user credentials from HTTP POST request body.
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            // Extract updated information from JSON.
            List<string> new_events_followed = data.EventsFollowed;

            // Retrieve username from query.
            string submitted_username = req.Query["username"];

            // Establish filter and update definitions.
            var filterDefinition = Builders<UserModel>.Filter.Eq(document => document.Username, submitted_username);
            var updateDefinition = Builders<UserModel>.Update
                .Set(document => document.EventsFollowed, new_events_followed);

            var optionsDefinition = new FindOneAndUpdateOptions<UserModel, UserModel>()
            {
                ReturnDocument = ReturnDocument.After
            };

            // Find and update account information.
            var result = collection.FindOneAndUpdate(filterDefinition, updateDefinition, optionsDefinition);

            if (result != null)
            {
                return new OkObjectResult(result);
            } else
            {
                // Account update failed.
                return new StatusCodeResult(400);
            }
        }
    }
}

