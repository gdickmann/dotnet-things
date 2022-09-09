using Microsoft.AspNetCore.Mvc;
using social_app_client.Models.User;
using social_app_client.Repositories.User;

namespace social_app_client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository= repository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserRequest request)
        {
            await _repository.InsertIntoQueue(request);
            return Ok();
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserRequest request, Guid id)
        {
            await _repository.UpdateFromQueue(request, id);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteFromQueue(id);
            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _repository.GetFromQueue(id);
            return Ok(response);
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _repository.GetAllFromQueue();
            return Ok(response);
        }
    }
}
