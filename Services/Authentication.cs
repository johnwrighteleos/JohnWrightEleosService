using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JohnWrightEleosService.Models;

namespace JohnWrightEleosService.Services
{
    public class Authentication
    {
        private string _secretKey;
        private List<UserCredentials> _validUsers; // In-memory list of valid users

        public Authentication(string secretKey)
        {
            _secretKey = secretKey;

            // Initialize the list of valid users (you can replace this with your data source)
            _validUsers = new List<UserCredentials>
            {
                new UserCredentials { Username = "user1", Password = "password1" },
                new UserCredentials { Username = "user2", Password = "password2" },
                // Add more valid users as needed
            };
        }

        public userModel AuthenticateUser(string username, string password, bool isTeamDriverLogin)
        {
            // Find the user in the list of valid users
            UserCredentials userCredentials = _validUsers.Find(u => u.Username == username && u.Password == password);

            if (userCredentials != null)
            {
                userModel user = new userModel
                {
                    Username = NormalizeUsername(username),
                    FullName = "John Doe" // Set the full name of the authenticated user
                };

                if (isTeamDriverLogin)
                {
                    // You can add more logic here if needed for team driver authentication
                    // For example, set the telematics object in the user object
                    user.Telematics = new Telematics { /* Initialize telematics object */ };
                }

                return user;
            }

            return null; // Authentication failed
        }

        private string NormalizeUsername(string username)
        {
            // Implement your normalization logic here if needed
            // For example, convert the username to lowercase or uppercase
            return username;
        }

        public string GenerateToken(userModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("username", user.Username),
                    new Claim("full_name", user.FullName)
                    // Add more claims as needed
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    // Class to represent user credentials
    public class UserCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
