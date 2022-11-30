using System;
using SportsApp;
using System.Text;
using Newtonsoft.Json;

namespace EventDatabaseManagement
{
	public class EventDatabaseManagement
	{
        public async void createEvent(string title, string icon, string imageURL, string location, string date, string description)
        {

            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://sportsfunctionapp.azurewebsites.net/api/CreateEvent");

                var newEvent = new EventModel()
                {
                    Title = title,
                    Icon = icon,
                    ImageURL = imageURL,
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

        public async Task<EventModel> fetchEvent(string id)
        {
            using (var client = new HttpClient())
            {
                // Test/temp variables.
                // var id = "6381de942c54e6d4762432c3";

                var endpoint = new Uri($"https://sportsfunctionapp.azurewebsites.net/api/FetchAccount?id={id}");

                // Make GET request.
                var response = await client.GetAsync(endpoint);
                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize GET request.
                return JsonConvert.DeserializeObject<EventModel>(result);
            }
        }

        public async void updateEvent(string event_id, string new_location, string new_date, string new_description)
        {
            using (var client = new HttpClient())
            {
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

