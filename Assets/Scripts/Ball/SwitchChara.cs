using UnityEngine;
using UI;

namespace Ball
{
    public class SwitchChara : MonoBehaviour
    {
        [SerializeField] private GameObject currentSkin;
        private void Awake() => ShopManager.OnEquip += SwitchSkin;
        private void OnDisable() => ShopManager.OnEquip -= SwitchSkin;
        private void SwitchSkin(GameObject skin)
        {
            Destroy(currentSkin);
            currentSkin = Instantiate(skin, transform);
        }
    }
}