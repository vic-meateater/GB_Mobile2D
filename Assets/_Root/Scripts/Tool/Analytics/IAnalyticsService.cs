using System.Collections.Generic;

namespace Tool.Analytics
{
    public interface IAnalyticsService
    {
        void SendEvent(string eventName);
        void SendEvent(string eventName, Dictionary<string, object> eventData);
    }
}
