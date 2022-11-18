using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payment_service.Model;
public class Payment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }

    [BsonElement("userid")]
    public int UserId { get; set; }

    [BsonElement("usersoldbeforetransaction")]
    public double UserSoldBeforeTransaction { get; set; }

    [BsonElement("sold")]
    public double Sold { get; set; }

    [BsonElement("usersoldaftertransaction")]
    public double UserSoldAfterTransaction { get; set; }
}

