using Pigeon.Messaging.Consuming.Dispatching;
using Shared;
using Shared.Metadata;
using Shared.Services;

namespace Consumer.API.ConsumeInterceptors
{
    public class TraceabilityConsumeInterceptor : IConsumeInterceptor
    {
        private readonly ITraceabilityContext _traceabilityContext;

        public TraceabilityConsumeInterceptor(ITraceabilityContext traceabilityContext)
        {
            _traceabilityContext = traceabilityContext ?? throw new ArgumentNullException(nameof(traceabilityContext));
        }

        public ValueTask Intercept(ConsumeContext context, CancellationToken cancellationToken = default)
        {
            var metadata = context.GetMetadata<TraceabilityMetadata>(MetadataKeys.Traceability);

            _traceabilityContext.SetTraceId(metadata.TraceId!);

            return ValueTask.CompletedTask;
        }
    }
}
