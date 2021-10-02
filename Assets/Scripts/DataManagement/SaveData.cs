using UnityEngine;

namespace DataManagement
{
    public class SaveData : MonoBehaviour
    {
        public static SaveData DataSave { get; private set; }
        public int Cash;
        public int[] UnlockedSkins;
        public int EquippedSkinIndex;
        private void OnEnable()
        {
            // PlayerPrefs.DeleteAll();
            DataSave = LoadData();
        }
        private void OnDisable() => Save();
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                Save();
        }
        private static void Save()
        {
            PlayerPrefs.SetInt("Cash", DataSave.Cash);
            PlayerPrefs.SetInt("SkinIndex", DataSave.EquippedSkinIndex);

            for (int i = 0; i < DataSave.UnlockedSkins.Length; i++)
            {
                var item = DataSave.UnlockedSkins[i];
                PlayerPrefs.SetInt(i.ToString(), item); // unlocked is 1 locked 0
            }
        }

        private static SaveData LoadData()
        {
            int cash = PlayerPrefs.GetInt("Cash", 0);
            int skinIndex = PlayerPrefs.GetInt("SkinIndex", 0);
            SaveData data = new SaveData()
            {
                Cash = cash,
                EquippedSkinIndex = skinIndex,
            };
            data.UnlockedSkins = new int[6];
            data.UnlockedSkins[0] = 1; // skin unlocked by default

            for (int i = 1; i < data.UnlockedSkins.Length; i++)
            {
                int unlocked = PlayerPrefs.GetInt(i.ToString(), 0);
                data.UnlockedSkins[i] = unlocked; // unlocked is 1 locked 0
            }
            return data;
        }
    }
}
