using System.Collections.Generic;
using Tool.Analytics;
using UnityEngine.Analytics;

public class UnityAnalyticsService : IAnalyticsService
{
    public void SendEvent(string eventName) =>
        Analytics.CustomEvent(eventName);
    
    public void SendEvent(string eventName, Dictionary<string, object> eventData) =>
        Analytics.CustomEvent(eventName, eventData);
    
}
