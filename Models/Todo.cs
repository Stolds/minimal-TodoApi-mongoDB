
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models;

public class Todo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set;}

    [BsonElement("Name")]
    public string Name { get; set;} = null!;

    [BsonElement("Done")]
    public bool Done {get; set;}

    [BsonElement("Description")]
    public string Description {get; set;} = null!;

    [BsonElement("ScheduledTime")]
    public DateTime ScheduledTime {get; set;}
}