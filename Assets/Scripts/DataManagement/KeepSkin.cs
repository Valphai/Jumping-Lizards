using UnityEngine;
using UI;

namespace DataManagement
{
    public class KeepSkin : MonoBehaviour
    {
        [SerializeField] private ShopManager shopManager;
        void Start()
        {
            shopManager.KeepSkin();
        }
    }
}
