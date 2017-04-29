using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace MongoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient client = new MongoClient("mongodb://127.0.0.1:27017");
            MongoServer server = client.GetServer();
            var db = server.GetDatabase("Social_Network");
            var tweets = db.GetCollection<Tweet>("tweets");
            // Find all tweets
            var totalTweets = tweets.FindAll().Count();
            Console.WriteLine("Total Documents in tweet collection: " + totalTweets);

            // Find users by selecting distinct on User
            var totalUsers = tweets.Distinct("user").Count();
            Console.WriteLine("Total user count:" + totalUsers);

            // Find top 10 of those who tag most other users.
            var taggers = GetTopTenMostTaggers();
            foreach (var user in taggers)
            {
                Console.WriteLine(user.AsBsonDocument.ToString().Replace("_id","Username:").Replace("\\", "").Replace("\"", "").Replace("{", "").Replace("}", "").Replace(":", ""));
            }

            Console.ReadLine();
        }

        protected static IMongoClient _client;
        protected static IMongoDatabase db;
        static List<BsonDocument> GetTopTenMostTaggers()
        {
            _client = new MongoClient("mongodb://127.0.0.1:27017");
            db = _client.GetDatabase("Social_Network");

            var collection = db.GetCollection<BsonDocument>("tweets");
            var aggregate = collection.Aggregate(new AggregateOptions { AllowDiskUse = true })
                .Match(new BsonDocument { { "text", BsonRegularExpression.Create(new Regex("@")) } })
                .Group(new BsonDocument { { "_id", "$user" }, { "nrOfTags", new BsonDocument("$sum", 1) } })
                .Sort(new BsonDocument { { "nrOfTags", -1 } })
                .Limit(10);
            var results = aggregate.ToList();

            return results;
        }
    }
}
