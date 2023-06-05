using UnityEngine;

namespace Tool
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            if (enabled)
                DontDestroyOnLoad(gameObject);
        }
    }
}
