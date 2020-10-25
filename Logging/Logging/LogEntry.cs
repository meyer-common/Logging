using System;

namespace Meyer.Common.Logging
{
    public class LogEntry
    {
        public DateTimeOffset TimeStamp { get; set; }

        public string Level { get; set; }

        public string Class { get; set; }

        public string Namespace { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }
    }
}