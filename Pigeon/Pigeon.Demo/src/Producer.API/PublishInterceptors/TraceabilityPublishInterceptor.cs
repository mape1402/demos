using Pigeon.Messaging.Producing;
using Shared;
using Shared.Metadata;
using Shared.Services;

namespace Producer.API.PublishInterceptors
{
    public class TraceabilityPublishInterceptor : IPublishInterceptor
    {
        private readonly ITraceabilityContext _traceabilityContext;

        public TraceabilityPublishInterceptor(ITraceabilityContext traceabilityContext)
        {
            _traceabilityContext = traceabilityContext ?? throw new ArgumentNullException(nameof(traceabilityContext));
        }

        public ValueTask Intercept(PublishContext publishContext, CancellationToken cancellationToken = default)
        {
            var metadata = new TraceabilityMetadata
            {
                TraceId = _traceabilityContext.GetTraceId()
            };

            publishContext.AddMetadata(MetadataKeys.Traceability, metadata);

            return ValueTask.CompletedTask;
        }
    }
}
