using Newtonsoft.Json;
using Serilog.Events;
using Serilog.Formatting;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Meyer.Common.Logging
{
    public class LogEntryJsonFormatter : ITextFormatter
    {
        private static AssemblyName assembly = Assembly.GetEntryAssembly().GetName();

        public void Format(LogEvent logEvent, TextWriter output)
        {
            var exception = logEvent.Exception;

            logEvent.Properties.TryGetValue("contextType", out LogEventPropertyValue contextType);
            logEvent.Properties.TryGetValue("SourceContext", out LogEventPropertyValue sourceContext);

            List<LogEntry> listOfLogEntries = new List<LogEntry>();
            do
            {
                listOfLogEntries.Add(new LogEntry
                {
                    Application = assembly.Name,
                    Version = assembly.Version,
                    Message = exception != null ? exception.Message : logEvent.MessageTemplate.Text,
                    Level = logEvent.Level.ToString().Trim('"'),
                    TimeStamp = logEvent.Timestamp,
                    StackTrace = exception?.StackTrace,
                    Namespace = sourceContext?.ToString().Trim('"'),
                    Class = contextType?.ToString().Trim('"')
                });

                exception = exception?.InnerException;
            }
            while (exception != null);

            output.Write(JsonConvert.SerializeObject(listOfLogEntries));
        }
    }
}
