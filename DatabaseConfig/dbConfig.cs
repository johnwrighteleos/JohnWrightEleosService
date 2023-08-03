//this is just for my exploratory purposes to make sure i can connect to my database

using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace JohnWrightEleosService.DatabaseConfig
{
    class dbConfig
    {
        public class YourDocument
        {
            [BsonId]
            public ObjectId Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Email { get; set; }
        }

        static void Main(string[] args)
        {
            string connectionString = "mongodb+srv://johnwright:<l6fzVvQ5hKFTKdPA>@johnwebservice.pgjs8vn.mongodb.net/?retryWrites=true&w=majority"; 

            // Create a MongoClient instance to connect to the MongoDB server
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("JohnWebService");
            var collection = database.GetCollection<YourDocument>("Authentication");
            //just for confirming i can actually upload something from my backend to my database
            // Insert a document into the collection
            var document = new YourDocument
            {
                Name = "John Doe",
                Age = 30,
                Email = "john@example.com"
            };

            collection.InsertOne(document);

            // Query data from the collection
            var filter = Builders<YourDocument>.Filter.Eq("Name", "John Doe");
            var result = collection.Find(filter).ToList();

            foreach (var item in result)
            {
                Console.WriteLine($"Name: {item.Name}, Age: {item.Age}, Email: {item.Email}");
            }
        }
    }
}
