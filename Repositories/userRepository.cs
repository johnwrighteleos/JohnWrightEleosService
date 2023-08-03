using System.Threading.Tasks;
using MongoDB.Driver;
using JohnWrightEleosService.Models;

namespace JohnWrightEleosService.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<userModel> _users;

        public UserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<userModel>("users");
        }

        public async Task<userModel> GetUserByUsername(string username)
        {
            // Implement logic to retrieve a user by username from the database.
            // Example using MongoDB query:
            return await _users.Find(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task<userModel> LoginUser(string username, string password, bool isTeamDriverLogin)
        {
            var user = await _users.Find(user => user.Username == username).FirstOrDefaultAsync();

            if (user != null)
            {
                // Check if the provided password matches the stored password
                if (user.Password == password)
                {
                    // Perform additional logic here if needed, such as handling team driver login
                    // ...

                    // Return the user object after successful login
                    return user;
                }
            }

            return null; // Login failed
        }

        // Other methods for updating and deleting users...
    }
}
