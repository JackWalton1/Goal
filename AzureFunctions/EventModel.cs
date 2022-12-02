using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AzureFunctions;

public class EventModel
{
    [Required]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Icon { get; set; }
    [Required]
    public string ImageURL { get; set; }
    [Required]
    public string VenueName { get; set; }
    [Required]
    public string VenueAddress { get; set; }
    [Required]
    public string Date { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int FollowerCount { get; set; }
}

