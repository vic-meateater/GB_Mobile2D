using System;

namespace Tool.Ads
{
    internal interface IAdsPlayer
    {
        event Action Started;
        event Action Finised;
        event Action Failed;
        event Action Completed;
        event Action BecomeReady;
        event Action Clicked;
        event Action Skipped;

        void Play();
    }
}