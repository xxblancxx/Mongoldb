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
            var ex = new MongoDBExerciseHandler();

           
            //First query
            Console.WriteLine("How many Twitter users are in our database?");
            Console.WriteLine("Total amount of users: " + ex.GetUserCount());

            //Second query
            Console.WriteLine();
            Console.WriteLine("Which Twitter users link the most to other Twitter users? (Provide the top ten.)");
            WriteUsersFromBsonDocument(ex.GetTopTenTaggers());

            //Third query
            Console.WriteLine();
            Console.WriteLine("Who is are the most mentioned Twitter users? (Provide the top five.)");
            Console.WriteLine("--- --Not Completed Yet :(-- ---");

            //Fourth query
            Console.WriteLine();
            Console.WriteLine("Who are the most active Twitter users (top ten)?");
            WriteUsersFromBsonDocument(ex.GetMostActiveUsers());

            //Fifth query
            Console.WriteLine();
            Console.WriteLine("Who are the five most grumpy (most negative tweets) and the most happy (most positive tweets)? (Provide five users for each group)");
            Console.WriteLine("Top 5 Happy tweets (Containing happy|love|party|hug|kiss)");
            WriteUsersFromBsonDocument(ex.GetMostHappyUsers());
            Console.WriteLine("Top 5 Happy tweets (Containing angry|hate|fuck|kill)");
            WriteUsersFromBsonDocument(ex.GetMostGrumpyUsers());

            Console.ReadLine();
        }

        public static void WriteUsersFromBsonDocument(List<BsonDocument> response)
        {
            int count = 1;
            foreach (var user in response)
            {
                Console.WriteLine(count + user.AsBsonDocument.ToString().Replace("_id", "Username:").Replace("\\", "").Replace("\"", "").Replace("{", "").Replace("}", "").Replace(":", ""));
                count++;
            }
        }
    }
}
