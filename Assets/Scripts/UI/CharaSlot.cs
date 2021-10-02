using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class CharaSlot : MonoBehaviour
    {
        public static event Action<int> OnSelect;
        public GameObject Skin;
        public int Cost;
        public int PositionInShop;
        [SerializeField] private Sprite skinSprite;
        [SerializeField] private Image spriteImage;
        [SerializeField] private GameObject tick;
        [SerializeField] private GameObject priceTag;

        private void OnValidate() => spriteImage.sprite = skinSprite;
        private void Awake() 
        {
            priceTag.GetComponentInChildren<TMP_Text>().text = Cost.ToString();
            priceTag.SetActive(true);
            tick.SetActive(false);
        }
        public void Unlock()
        {
            priceTag.SetActive(false);
            tick.SetActive(true);
        }
        public void ClickedOn()
        {
            OnSelect?.Invoke(PositionInShop);
        }
    }
}
