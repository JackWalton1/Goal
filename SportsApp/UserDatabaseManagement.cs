using System;
using SportsApp;
using System.Text;
using Newtonsoft.Json;
using MongoDB.Driver.Core.Authentication;

namespace UserDatabaseManagement
{
	public class UserDatabaseManagement
	{
        public async void createAccount(string username, string password, string location, string access_level)
        {

            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://sportsfunctionapp.azurewebsites.net/api/CreateAccount");

                var newUser = new UserModel()
                {
                    Username = username,
                    Password = password,
                    Location = location,
                    AccessLevel = access_level
                };

                // Serialize JSON user credentials.
                var newPostJson = JsonConvert.SerializeObject(newUser);
                var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");

                // Make POST request.
                var response = await client.PostAsync(endpoint, payload);
                var result = response.Content.ReadAsStringAsync().Result;
            }
        }

        public async Task<UserModel> fetchAccount(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://sportsfunctionapp.azurewebsites.net/api/FetchAccount?username={username}&password={password}");

                // Make GET request.
                var response = await client.GetAsync(endpoint);
                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize GET request.
                return JsonConvert.DeserializeObject<UserModel>(result);
            }
        }

        public async void updateAccount(string username, string new_password, string new_location)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://sportsfunctionapp.azurewebsites.net/api/UpdateAccount?username={username}");

                // Create new UserModel object that contains the updated user info.
                var updatedUser = new UserModel()
                {
                    Password = new_password,
                    Location = new_location,
                };

                // Serialize JSON updated user information.
                var newPostJson = JsonConvert.SerializeObject(updatedUser);
                var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");

                // Make POST request.
                var response = await client.PostAsync(endpoint, payload);

            }
        }
    }
}

