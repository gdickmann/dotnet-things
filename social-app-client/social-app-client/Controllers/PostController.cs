using MassTransit;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using social_app_client.Models.Post;
using System.Text;

namespace social_app_client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        
        private readonly ILogger<PostController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public PostController(ILogger<PostController> logger, IPublishEndpoint publishEndpoint)
        {            
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(PostRequest request)
        {
            await _publishEndpoint.Publish<Post>(new
            {
                Id = Guid.NewGuid(),
                Author = request.Author,
                Tag = request.Tag,
                Title = request.Title
            });
            return Ok();
        }
    }
}
