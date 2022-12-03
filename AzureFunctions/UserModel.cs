using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AzureFunctions;

public class UserModel
{
    [Required]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] public string Id { get; set; }
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set; }
    [Required] public string Location { get; set; }
    [Required] public string AccessLevel { get; set; }
    // For Users, (avoiding making an abstract class and refactoring)
    [Required] public string EventsFollowed { get; set; }
    // For Town Admins
    [Required] public string VenueName { get; set; }
    [Required] public string VenueAddress { get; set; }
    [Required] public string EventIDs { get; set; }
    [Required] public string UserFollowers { get; set; }
    // For Both user and Town Admins, to be dealt with differently tho
    [Required] public int FollowCount { get; set; }
}

