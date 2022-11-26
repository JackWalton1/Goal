using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AzureFunctions
{
    public static class CreateAccount
    {
        [FunctionName("CreateAccount")]
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

            // // Create new UserModel object.
            var user = new UserModel { Username = data.Username, Password = data.Password, Location = data.Location, AccessLevel = data.AccessLevel };

            // Insert UserModel object into MongoDB.
            if (user.Username != null || user.Password != null || user.Location != null || user.AccessLevel != null)
            {
                await collection.InsertOneAsync(user);
            }

            // Response for testing purposes.
            string responseMessage = "Account created successfully.";
            return new OkObjectResult(responseMessage);
        }
    }
}

