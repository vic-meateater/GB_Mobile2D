using System.Collections.Generic;
using UnityEngine;

namespace Tool.Analytics
{
    internal class AnalyticsManager : MonoBehaviour
    {
        private IAnalyticsService[] _analyticsService;

        private void Awake()
        {
            _analyticsService = new IAnalyticsService[]
            {
                new UnityAnalyticsService()
            };
        }

        public void SendMainMenuOpenedEvent() =>
            SendEvent("MainMenuOpened");

        public void SendGameStartEvent() => 
            SendEvent("gameStarted");

        private void SendEvent(string eventName)
        {
            foreach (IAnalyticsService service in _analyticsService)
            {
                service.SendEvent(eventName);
                Debug.Log($"we send event from {service} event {eventName}");
            }
        }

        private void SendEvent(string eventName, Dictionary<string, object> eventData)
        {
            foreach (IAnalyticsService service in _analyticsService)
            {
                service.SendEvent(eventName, eventData);
            }
        }
    }
}