using System;
using System.Diagnostics;

namespace WorkSphere.Global_Classes
{
    public static class clsEventLogger
    {
        private const string Source = "WorkSphereApp";
        private const string LogName = "Application";

        static clsEventLogger()
        {
            try
            {
                if (!EventLog.SourceExists(Source))
                {
                    EventLog.CreateEventSource(Source, LogName);
                }
            }
            catch
            {
                // لا ترمي استثناء هنا حتى لا تتعطل الطبقة
            }
        }

        public static void LogException(string location, Exception ex)
        {
            try
            {
                string message =
                    $"Exception Location: {location}\n\n" +
                    $"Time: {DateTime.Now}\n\n" +
                    $"Message: {ex.Message}\n\n" +
                    $"Stack Trace:\n{ex.StackTrace}";

                EventLog.WriteEntry(Source, message, EventLogEntryType.Error);
            }
            catch
            {
                // لا نفعل شيء حتى لا تسبب أخطاء إضافية
            }
        }
        public static void LogInfo(string message)
        {
            try
            {
                EventLog.WriteEntry(Source, message, EventLogEntryType.Information);
            }
            catch { }
        }
    }
}