using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoApp
{
    public class Tweet
    {
        //public ObjectId Id { get; set; }
        public string polarity { get; set; }
        public string _id { get; set; }
        public DateTime date { get; set; }
        public string query { get; set; }
        public string user { get; set; }
        public string text { get; set; }
    }
}
