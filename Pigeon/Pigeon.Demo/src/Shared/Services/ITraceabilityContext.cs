namespace Shared.Services
{
    public interface ITraceabilityContext
    {
        string GetTraceId();

        void SetTraceId(string traceId);
    }
}
