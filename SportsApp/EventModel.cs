using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SportsApp;

public class EventModel
{
    [Required]
    [BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Location { get; set; }
    [Required]
    public string Date { get; set; }
    [Required]
    public string Description { get; set; }
}

