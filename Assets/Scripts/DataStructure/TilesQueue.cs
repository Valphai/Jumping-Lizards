using System;
using UnityEngine;

namespace DataStructure
{
    public class TilesQueue : MonoBehaviour
    {
        public InstancedTile[] tilesQ = new InstancedTile[3];
        public static event Action OnTilesShift;
        [SerializeField] private GameObject[] tilesToSpawn;
        [SerializeField] private GameObject grid;

        private void Start()
        {
            for (int i = 0; i < tilesQ.Length; i++)
            {
                SpawnTile(i);
            }
        }

        public void Add()
        {
            // spawn in front and shift the rest back & delete 0th elem
            Shift();
            SpawnTile(tilesQ.Length - 1);
        }
        private void SpawnTile(int positionIndex)
        {
            int rand = UnityEngine.Random.Range(0, tilesToSpawn.Length);
            tilesQ[positionIndex] = Instantiate(tilesToSpawn[rand], grid.transform).GetComponent<InstancedTile>();
        }
        private void Shift()
        {
            GameObject backTile = tilesQ[0].gameObject;
            Destroy(backTile);
            for (int i = 1; i < tilesQ.Length; i++)
            {
                tilesQ[i - 1] = tilesQ[i];
            }
            OnTilesShift?.Invoke();
        }
    }
}
