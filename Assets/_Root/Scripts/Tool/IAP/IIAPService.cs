using UnityEngine.Events;

namespace Tool.IAP
{
    internal interface IIAPService
    {
        UnityEvent Initialized { get; }
        UnityEvent PurchaseSucceed { get; }
        UnityEvent PurchaseFailed { get; }
        bool IsInitialized { get; }

        void Buy(string id);
        string GetCost(string productID);
        void RestorePurchases();
    }
}