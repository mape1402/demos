using Microsoft.AspNetCore.Mvc;
using Pigeon.Messaging.Producing;
using Shared;
using Shared.Messages;

namespace Producer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IProducer _producer;

        public DemoController(IProducer producer)
        {
            _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        }

        [HttpPost("helloworld")]
        public async Task<ActionResult> HelloWorld(CancellationToken cancellationToken)
        {
            await _producer.PublishAsync(new HelloWorldMessage { Text = "Hello World!!" }, $"Demo.Consumer.{Topics.HelloWorld}", cancellationToken);
            return Ok();
        }

        [HttpPost("newuser")]
        public async Task<ActionResult> CreateUser(CancellationToken cancellationToken)
        {
            var message = new UserCreatedMessage
            {
                UserId = 1,
                Name = "Test"
            };

            await _producer.PublishAsync(message, $"Demo.Consumer.{Topics.UserCreated}", "1.0.1", cancellationToken);   
            await _producer.PublishAsync(message, $"Demo.Consumer.{Topics.UserCreated}", cancellationToken);   

            return Ok();
        }
    }
}
