using UnityEngine;
using DataStructure;
using UI;

namespace Ball
{
    public class GenerateTiles : MonoBehaviour
    {
        private TilesQueue tilesQueue;
        private Vector3 tilePosBack, tilePosMid, tilePosFront;
        private Motion PlayerMotion;
        [SerializeField] private float spawnOffsetX;
        [SerializeField] private float spaceBetweenTiles;
        [SerializeField] private float spawnEligableDistance;

        private void Start() {
            tilesQueue = GetComponent<TilesQueue>();
            PlayerMotion = GetComponentInChildren<Motion>();
            GameManager.OnRestart += ResetTilePosition;
        }
        private void OnDisable() => GameManager.OnRestart -= ResetTilePosition;
        private void Update()
        {
            InstancedTile frontTile = tilesQueue.tilesQ[2];
            float playerPosZ = PlayerMotion.transform.position.z;

            if (frontTile.TileBackPosZ <= playerPosZ && playerPosZ <= frontTile.TileFrontPosZ)
            {
                tilesQueue.Add();
                UpdateTilePos(tilesQueue.tilesQ[1].transform.position.z);
            }
        }
        private void ResetTilePosition() => UpdateTilePos(0f);
        private void UpdateTilePos(float zPosition)
        {
            tilePosBack = new Vector3(spawnOffsetX, 0, zPosition - spaceBetweenTiles);
            tilePosMid = new Vector3(spawnOffsetX, 0, zPosition);
            tilePosFront = new Vector3(spawnOffsetX, 0, zPosition + spaceBetweenTiles); 

            tilesQueue.tilesQ[0].transform.position = tilePosBack;
            tilesQueue.tilesQ[1].transform.position = tilePosMid;
            tilesQueue.tilesQ[2].transform.position = tilePosFront;
        }
    }
}