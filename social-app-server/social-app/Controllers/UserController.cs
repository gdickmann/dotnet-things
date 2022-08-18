using Microsoft.AspNetCore.Mvc;
using social_app.Database;
using social_app.Models.User;
using social_app.Models.User.Request;

namespace social_app.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly SocialAppDbContext _context;

        public UserController(SocialAppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        [HttpPost("create")]
        public async Task<ActionResult> Create(UserRequest request)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(new User
                {
                    Username = request.Name,
                    Email = request.Email,
                    Password = request.Password
                });

                await _context.SaveChangesAsync();
                return Ok("User created successfully.");
            }

            return BadRequest();
        }

        /// <summary>
        /// Updates a specified user.
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> Update(User request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == request.Id);

            if (user != null)
            {
                _context.Users.Update(request);
                await _context.SaveChangesAsync();

                return Ok("User updated successfully.");
            }

            return NotFound($"User with ID {request.Id} wasn't found.");
        }

        /// <summary>
        /// Deletes a specified user.
        /// </summary>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return Ok("User deleted successfully.");
            }

            return NotFound($"User with ID {id} wasn't found.");
        }

        /// <summary>
        /// Gets a specified user.
        /// </summary>
        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = _context.Users.FirstOrDefault(u => u.Id == id);
            return response == null ? NotFound($"User with ID {id} wasn't found.") : Ok(response);
        }

        /// <summary>
        /// Returns all users.
        /// </summary>
        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var response = _context.Users.ToList();
            return response == null ? NotFound() : Ok(response);
        }
    }
}