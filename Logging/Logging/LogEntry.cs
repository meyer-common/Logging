using System;

namespace Meyer.Common.Logging
{
    public class LogEntry
    {
        public DateTimeOffset TimeStamp { get; internal set; }

        public string Level { get; internal set; }

        public string Application { get; internal set; }

        public Version Version { get; internal set; }

        public string Class { get; internal set; }

        public string Namespace { get; internal set; }

        public string Message { get; internal set; }

        public string StackTrace { get; internal set; }
    }
}