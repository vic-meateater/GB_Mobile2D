using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Tool.Bundles.Examples
{
    internal class LoadAddressableView : MonoBehaviour
    {
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private AssetReference _spritePrefab;
        [SerializeField] private Button _addBackgroundButton;
        [SerializeField] private Button _removeBackgroundButton;

        private List<AsyncOperationHandle<Sprite>> _addressablePrefabs = new();
        private void Start()
        {
            _addBackgroundButton.onClick.AddListener(LoadAddressablePrefab);
            _removeBackgroundButton.onClick.AddListener(RemoveAddressablePrefab);
        }


        private void OnDestroy()
        {
            _addBackgroundButton.onClick.RemoveAllListeners();
            _removeBackgroundButton.onClick.RemoveAllListeners();
            RemoveAddressablePrefab();
        }

        private void LoadAddressablePrefab()
        {
            var addressablePrefab = Addressables.LoadAssetAsync<Sprite>(_spritePrefab.SubObjectName);
            
            addressablePrefab.Completed += OnAddressablePrefabLoaded;
            _addressablePrefabs.Add(addressablePrefab);
        }

        private void OnAddressablePrefabLoaded(AsyncOperationHandle<Sprite> sprite)
        {
            _backgroundImage.sprite = sprite.Result;
        }


        private void RemoveAddressablePrefab()
        {
            foreach (var addressablePrefab in _addressablePrefabs)
                Addressables.Release(addressablePrefab);
            _backgroundImage.sprite = null;
            _addressablePrefabs.Clear();
            Debug.Log(_addressablePrefabs.Count);
        }
    }
}