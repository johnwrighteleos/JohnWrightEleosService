using MongoDB.Driver;

namespace JohnWrightEleosService.DatabaseConfig
{
    public class DatabaseUtility
    {
        public static IMongoDatabase GetDatabaseConnection(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            return client.GetDatabase(databaseName);
        }
    }
}