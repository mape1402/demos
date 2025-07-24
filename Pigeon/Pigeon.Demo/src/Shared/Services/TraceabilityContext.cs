namespace Shared.Services
{
    public class TraceabilityContext : ITraceabilityContext
    {
        private string _traceId;

        public string GetTraceId()
        {
            if(string.IsNullOrWhiteSpace(_traceId))
                _traceId = Guid.NewGuid().ToString();

            return _traceId;
        }

        public void SetTraceId(string traceId)
        {
            if(string.IsNullOrWhiteSpace(traceId))
                throw new ArgumentNullException(nameof(traceId));

            _traceId = traceId;
        }
    }
}
