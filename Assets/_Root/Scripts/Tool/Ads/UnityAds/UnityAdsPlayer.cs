using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Tool.Ads.UnityAds
{
    internal abstract class UnityAdsPlayer : IAdsPlayer, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public event Action Started;
        public event Action Finised;
        public event Action Failed;
        public event Action Completed;
        public event Action BecomeReady;
        public event Action Clicked;
        public event Action Skipped;

        protected readonly string Id;


        protected UnityAdsPlayer(string id)
        {
            Id = id;
        }

        public void Play()
        {
            Load();
            OnPlaying();
            Load();
        }

        protected abstract void Load();
        protected abstract void OnPlaying();


        void IUnityAdsLoadListener.OnUnityAdsAdLoaded(string placementId)
        {
            if (IsIdMy(placementId) != false) 
                BecomeReady?.Invoke();
        }

        void IUnityAdsLoadListener.OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Failed?.Invoke();
            Error(placementId);
            Error(message);
        }
        

        void IUnityAdsShowListener.OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Failed?.Invoke();
            Error(message);
        }

        void IUnityAdsShowListener.OnUnityAdsShowStart(string placementId)
        {
            if (IsIdMy(placementId)) Started?.Invoke();
        }

        void IUnityAdsShowListener.OnUnityAdsShowClick(string placementId)
        {
            if (IsIdMy(placementId)) Clicked?.Invoke();
        }

        void IUnityAdsShowListener.OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (!IsIdMy(placementId)) return;
            
            switch (showCompletionState)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    Completed?.Invoke();
                    break;
                case UnityAdsShowCompletionState.SKIPPED:
                    Skipped?.Invoke();
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    Failed?.Invoke();
                    break;
            }
        }

        private bool IsIdMy(string placementId) => Id == placementId;

        private void Log(string message) => Debug.Log(WrapMessage(message));
        private void Error(string message) => Debug.LogError(WrapMessage(message));
        private string WrapMessage(string message) => $"[{GetType().Name}] {message}";


    }
}
