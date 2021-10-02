using System;
using UnityEngine;
using DataManagement;

namespace UI
{
    public class ShopManager : MonoBehaviour
    {
        public static event Action<GameObject> OnEquip;
        public CharaSlot[] SkinSlots;
        private CharaSlot[] unlockedSkins;

        // private void OnValidate() {
        //     SkinSlots = GetComponentsInChildren<CharaSlot>();
        // }
        private void Awake() 
        {
            unlockedSkins = new CharaSlot[SkinSlots.Length];
            CharaSlot.OnSelect += SelectSkin;
        }
        private void Start()
        {
            RefreshSlots();
        }
        private void OnDestroy() => CharaSlot.OnSelect -= SelectSkin;
        public void KeepSkin() => OnEquip?.Invoke(SkinSlots[SaveData.DataSave.EquippedSkinIndex].Skin);
        public void RefreshSlots()
        {
            for (int i = 0; i < SaveData.DataSave.UnlockedSkins.Length; i++)
            {
                if (SaveData.DataSave.UnlockedSkins[i] == 1)
                {
                    unlockedSkins[i] = SkinSlots[i];
                    unlockedSkins[i]?.Unlock();
                }
            }
        }
        public void SelectSkin(int index)
        {
            if (unlockedSkins[index] == SkinSlots[index])
            {
                OnEquip?.Invoke(SkinSlots[index].Skin);
                SaveData.DataSave.EquippedSkinIndex = index;
            }

            else if (SkinSlots[index].Cost <= SaveData.DataSave.Cash)
            {
                SaveData.DataSave.UnlockedSkins[index] = 1;
                SaveData.DataSave.Cash -= SkinSlots[index].Cost;

                OnEquip?.Invoke(SkinSlots[index].Skin);
                SaveData.DataSave.EquippedSkinIndex = index;

            }
            RefreshSlots();
        }
    }
}