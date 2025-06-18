using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace MauricioGym.Infra.Shared
{
    public static class AppInsigthsLogger

    {
        private static readonly TelemetryClient _telemetryClient;

        static AppInsigthsLogger()
        {
            // Replace with your Application Insights Instrumentation Key
            _telemetryClient = new TelemetryClient(new TelemetryConfiguration("a177eb53-0f42-4995-b459-cd2f519ee754"));
        }

        public static void LogException(Exception ex, string message, Dictionary<string, string> properties = null)
        {
            var exceptionTelemetry = new Microsoft.ApplicationInsights.DataContracts.ExceptionTelemetry(ex)
            {
                SeverityLevel = Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Error,
                Message = message
            };

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    exceptionTelemetry.Properties[property.Key] = property.Value;
                }
            }

            _telemetryClient.TrackException(exceptionTelemetry);
        }

        public static void Trace(string name, string message, Dictionary<string, string> properties = null)
        {
            var eventTelemetry = new Microsoft.ApplicationInsights.DataContracts.TraceTelemetry(message, Microsoft.ApplicationInsights.DataContracts.SeverityLevel.Information);

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    eventTelemetry.Properties[property.Key] = property.Value;
                }
            }

            _telemetryClient.TrackTrace(eventTelemetry);
        }

        public static void Trace<T>(string name, T context, Dictionary<string, string> properties = null) where T : class, new()
        {
            var obj = Newtonsoft.Json.JsonConvert.SerializeObject(context);
            Trace(name, obj, properties);
        }

    }
}
