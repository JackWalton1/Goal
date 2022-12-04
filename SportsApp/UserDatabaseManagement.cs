using System;
using SportsApp;
using System.Text;
using Newtonsoft.Json;
using MongoDB.Driver.Core.Authentication;

namespace UserDatabaseManagement
{
	public class UserDatabaseManagement
	{
        public async void createAccount(string username, string password, string location, string access_level, string EventsFollowed, string VenueName, string VenueAddress, string EventIDs, string UserFollowers, int FollowCount)
        {

            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://sportsfunctionapp.azurewebsites.net/api/CreateAccount");

                var newUser = new UserModel()
                {
                    Username = username,
                    Password = password,
                    Location = location,
                    AccessLevel = access_level,
                    VenueName = VenueName,
                    VenueAddress = VenueAddress,
                    EventsFollowed = EventsFollowed,
                    FollowCount = FollowCount
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

        public async void updateAccount(string username, string new_events_followed, int newFollowingCount)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://sportsfunctionapp.azurewebsites.net/api/UpdateAccount?username={username}");

                // Create new UserModel object that contains the updated user info.
                var updatedUser = new UserModel()
                {
                    EventsFollowed = new_events_followed,
                    FollowCount = newFollowingCount
                };

                // Serialize JSON updated user information.
                var newPostJson = JsonConvert.SerializeObject(updatedUser);
                var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");

                // Make POST request.
                var response = await client.PostAsync(endpoint, payload);
            }
        }

        public async void updateAdminEventIDs(string username, string newAdminEventIDs)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://sportsfunctionapp.azurewebsites.net/api/updateAdminEventIDs?username={username}");

                // Create new UserModel object that contains the updated user info.
                var updatedUser = new UserModel()
                {

                    EventIDs = newAdminEventIDs
                };

                // Serialize JSON updated user information.
                var newPostJson = JsonConvert.SerializeObject(updatedUser);
                var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");

                // Make POST request.
                var response = await client.PostAsync(endpoint, payload);

                //HttpResponseMessage response = await client.PostAsync(endpoint, payload);
            }
        }
    }
}

