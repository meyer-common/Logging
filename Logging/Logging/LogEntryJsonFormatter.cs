using Newtonsoft.Json;
using Serilog.Events;
using Serilog.Formatting;
using System.Collections.Generic;
using System.IO;

namespace Meyer.Common.Logging
{
    public class IdmCompactJsonFormatter : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            var exception = logEvent.Exception;

            logEvent.Properties.TryGetValue("contextType", out LogEventPropertyValue leClass);
            logEvent.Properties.TryGetValue("SourceContext", out LogEventPropertyValue leSourceContext);

            List<LogEntry> listOfLogEntries = new List<LogEntry>();
            do
            {
                listOfLogEntries.Add(new LogEntry
                {
                    Message = exception != null ? exception.Message : logEvent.MessageTemplate.Text,
                    Level = logEvent.Level.ToString().Trim('"'),
                    TimeStamp = logEvent.Timestamp,
                    StackTrace = exception?.StackTrace,
                    Namespace = leSourceContext?.ToString().Trim('"'),
                    Class = leClass?.ToString().Trim('"')
                });

                exception = exception?.InnerException;
            }
            while (exception != null);

            output.Write(JsonConvert.SerializeObject(listOfLogEntries));
        }
    }
}