using System;
using SportsApp;
using System.Text;
using Newtonsoft.Json;
using MongoDB.Bson;
using System.Net.Http.Json;

namespace EventDatabaseManagement
{
	public class EventDatabaseManagement
	{
        public async void createEvent(string title, string icon, string imageURL, string venueName, string venueAddress, string date, string description)
        {

            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://sportsfunctionapp.azurewebsites.net/api/CreateEvent");

                var newEvent = new EventModel()
                {
                    Title = title,
                    Icon = icon,
                    ImageURL = imageURL,
                    VenueName = venueName,
                    VenueAddress = venueAddress,
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
                var endpoint = new Uri($"https://sportsfunctionapp.azurewebsites.net/api/FetchEvent?id={id}");

                // Make GET request.
                var response = await client.GetAsync(endpoint);
                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize GET request.
                return JsonConvert.DeserializeObject<EventModel>(result);
            }
        }

        public async Task<List<EventModel>> fetchAllEvents()
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://sportsfunctionapp.azurewebsites.net/api/FetchAllEvents");

                // Make GET request.
                var response = await client.GetAsync(endpoint);
                var result = response.Content.ReadAsStringAsync().Result;

                // Deserialize GET request.
                return JsonConvert.DeserializeObject<List<EventModel>>(result);
            }
        }

        public async void updateEvent(string event_id, string new_venue_name, string new_venue_address, string new_date, string new_description)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://sportsfunctionapp.azurewebsites.net/api/UpdateEvent?id={event_id}");

                // Create new EventModel object that contains the updated event info.
                var updatedEvent = new EventModel()
                {
                    VenueName = new_venue_name,
                    VenueAddress = new_venue_address,
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

