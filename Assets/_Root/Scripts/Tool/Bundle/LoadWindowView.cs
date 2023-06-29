using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tool.Bundles.Examples
{
    internal class LoadWindowView : AssetBundleViewBase
    {
        [SerializeField] private Button _loadAssetsButton;


        private void Start()
        {
            _loadAssetsButton.onClick.AddListener(LoadAssets);
        }

        private void OnDestroy()
        {
            _loadAssetsButton.onClick.RemoveAllListeners();
        }


        private void LoadAssets()
        {
            var tmp = GetComponentInChildren<TMP_Text>();
            
            _loadAssetsButton.interactable = false;
            tmp.text = "";
            StartCoroutine(DownloadAndSetAssetBundles());
        }
    }
}