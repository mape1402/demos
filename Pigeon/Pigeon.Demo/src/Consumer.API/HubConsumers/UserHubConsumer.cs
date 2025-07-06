using Pigeon.Messaging.Consuming.Dispatching;
using Shared;
using Shared.Messages;

namespace Consumer.API.HubConsumers
{
    public class UserHubConsumer : HubConsumer
    {
        private readonly ILogger<UserHubConsumer> _logger;

        public UserHubConsumer(ILogger<UserHubConsumer> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Consumer(Topics.UserCreated, "1.0.1")]
        [Consumer(Topics.UserCreated, "1.0.0")]
        public Task CreateUser(UserCreatedMessage message, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User created v{0} -> Id: {1} Name: {2}", Context.MessageVersion, message.UserId, message.Name);
            return Task.CompletedTask;
        }
    }
}
