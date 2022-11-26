using System;
using SportsApp;
using System.Text;
using Newtonsoft.Json;

namespace EventDatabaseManagement
{
	public class EventDatabaseManagement
	{

        public async void createEvent()
        {

            using (var client = new HttpClient())
            {
                // Test/temp variables.
                var title = "test";
                var location = "test";
                var date = "test";
                var description = "test";
                var endpoint = new Uri("https://sportsfunctionapp.azurewebsites.net/api/CreateEvent");

                var newEvent = new EventModel()
                {
                    Title = title,
                    Location = location,
                    Date = date,
                    Description = description
                };

                // Serialize JSON event information.
                var newPostJson = JsonConvert.SerializeObject(newEvent);
                var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");

                // Make POST request.
                var response = await client.PostAsync(endpoint, payload);
                var result = response.Content.ReadAsStringAsync().Result;
            }
        }

        public async Task<EventModel> fetchEvent()
        {
            using (var client = new HttpClient())
            {
                // Test/temp variables.
                var id = "6381de942c54e6d4762432c3";

                var endpoint = new Uri($"https://sportsfunctionapp.azurewebsites.net/api/FetchAccount?id={id}");

                // Make GET request.
                var response = await client.GetAsync(endpoint);
                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize GET request.
                return JsonConvert.DeserializeObject<EventModel>(result);
            }
        }

        public async void updateEvent()
        {
            using (var client = new HttpClient())
            {
                // Temp testing variables.
                string event_id = "6381de942c54e6d4762432c3";
                string new_location = "updated";
                string new_date = "updated";
                string new_description = "updated";

                var endpoint = new Uri($"https://sportsfunctionapp.azurewebsites.net/api/UpdateEvent?id={event_id}");

                // Create new EventModel object that contains the updated event info.
                var updatedEvent = new EventModel()
                {
                    Location = new_location,
                    Date = new_date,
                    Description = new_description
                };

                // Serialize JSON updated event information.
                var newPostJson = JsonConvert.SerializeObject(updatedEvent);
                var payload = new StringContent(newPostJson, Encoding.UTF8, "application/json");

                // Make POST request.
                var response = await client.PostAsync(endpoint, payload);
            }
        }
    }
}

