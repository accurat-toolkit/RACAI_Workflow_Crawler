using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Agora.SysUtils.Logging {
    public class SysLog {
        static SysLog mainInstance = new SysLog();

        public static SysLog MainInstance {
            get { return SysLog.mainInstance; }
            set { SysLog.mainInstance = value; }
        }

        private EventLog elog = null;

        string eventSource = null;
        /// <summary>
        /// Used to 
        /// </summary>
        public string EventSource {
            get { return eventSource; }
            set { eventSource = value; }
        }

        /// <summary>
        /// Creates a new instance of Agora Event Logger
        /// </summary>
        public SysLog(EventLog elog) {
            this.elog = elog;
        }
        public SysLog() {
            this.EventSource = Thread.GetDomain().FriendlyName;
            elog = new EventLog();
        }
        /// <summary>
        /// Writes a new entry in the event log
        /// </summary>
        /// <param name="message">Text that will be displayed in the EventViewer</param>
        /// <param name="type">Event type. This will reflect the severity of the message. To display information use another overload or set this parameter to EventLogEntryType.Information</param>
        public void WriteEventMessage(string message, EventLogEntryType type) {
            try {

                if (!EventLog.Exists(eventSource)) {
                    try {
                        EventLog.CreateEventSource(eventSource, eventSource);
                    } catch { }
                }
                elog.Source = eventSource;
                elog.EnableRaisingEvents = true;
                elog.WriteEntry(message, type);
            } catch { }
        }

        /// <summary>
        /// Creates a new information entry in the event log using EventLogEntryType.Information. For advanced options use another overload or WriteEventException
        /// </summary>
        /// <param name="message">Text that will be displayed in the EventViewer</param>
        public void WriteEventMessage(string message) {
            try {
                if (!EventLog.Exists(eventSource)) {
                    EventLog.CreateEventSource(eventSource, eventSource);
                }
                elog.Source = eventSource;
                elog.EnableRaisingEvents = true;
                elog.WriteEntry(message, EventLogEntryType.Information);
            } catch { }
        }
        /// <summary>
        /// Used to write exceptions to the event viewer. The message will message and stacktrace
        /// </summary>
        /// <param name="e">Exception that occured</param>
        public void WriteEventException(Exception e) {
            try {
                WriteEventMessage(e.ToString() + "\r\n-------------------\r\n" + e.StackTrace.ToString() + "\r\n------------\r\n", EventLogEntryType.Error);
            } catch { }
        }
    }
}
