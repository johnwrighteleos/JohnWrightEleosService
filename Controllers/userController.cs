using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JohnWrightEleosService.Models;
using JohnWrightEleosService.Services;
using JohnWrightEleosService.Repositories;

namespace JohnWrightEleosService.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly Authentication _authentication;
        private readonly UserRepository _userRepository;

        public UserController()
        {
            _authentication = new Authentication("your_secret_key");
            _userRepository = new UserRepository(); // Initialize your UserRepository instance here.
        }

        [HttpPost("user")]
        public async Task<IActionResult> RegisterUser([FromBody] userModel user)
        {
            if (string.IsNullOrEmpty(user.Username))
            {
                return BadRequest("Please add a username");
            }

            // Check if user exists
            var userExists = await _userRepository.GetUserByUsername(user.Username);
            if (userExists != null)
            {
                return BadRequest("User already exists");
            }

            if (string.IsNullOrEmpty(user.FullName))
            {
                return BadRequest("Please add user's full name");
            }

            // Authenticate User (you need to implement AuthenticateUser in Authentication.cs)
            var authenticatedUser = _authentication.AuthenticateUser(user.Username, user.Password);
            if (authenticatedUser == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Create User
            user.ApiToken = _authentication.GenerateToken(authenticatedUser);
            var createdUser = await _userRepository.CreateUser(user);

            if (createdUser != null)
            {
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
            }
            else
            {
                return BadRequest("Invalid user data");
            }
        }

        // You can continue to add your other controller actions here...

        // For example:
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
