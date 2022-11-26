using System;
using SportsApp;
using System.Text;
using Newtonsoft.Json;

namespace UserDatabaseManagement
{
	public class UserDatabaseManagement
	{

        public async void createAccount()
        {

            using (var client = new HttpClient())
            {
                // Test/temp variables.
                var username = "carter";
                var password = "pass123";
                var location = "test_location";
                var access_level = "test_access";
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

        public async Task<UserModel> fetchAccount()
        {
            using (var client = new HttpClient())
            {
                // Test/temp variables.
                var username = "CARTER1";
                var password = "carterpassword";

                var endpoint = new Uri($"https://sportsfunctionapp.azurewebsites.net/api/FetchAccount?username={username}&password={password}");

                // Make GET request.
                var response = await client.GetAsync(endpoint);
                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize GET request.
                return JsonConvert.DeserializeObject<UserModel>(result);
            }
        }

        public async void updateAccount()
        {
            using (var client = new HttpClient())
            {
                // Temp testing variables.
                string username = "CARTER1";
                string new_password = "p";
                string new_location = "l";

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

