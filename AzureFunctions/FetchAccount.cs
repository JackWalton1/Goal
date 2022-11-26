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
    public static class FetchAccount
    {
        [FunctionName("FetchAccount")]
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

            // Retrieve username and password from query.
            string submitted_username = req.Query["username"];
            string submitted_password = req.Query["password"];

            // Locate document with matching username.
            var result = await collection.Find(document => document.Username == submitted_username).SingleAsync();

            // Validate password.
            if (result.Password == submitted_password && result != null)
            {
                // Return UserModel containing user credentials.
                return new OkObjectResult(result);
            }
            else
            {
                // Return 403 Forbidden status code.
                return new StatusCodeResult(403);
            }
        }
    }
}

