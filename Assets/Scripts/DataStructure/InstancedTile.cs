using UnityEngine;
using UI;

namespace DataStructure
{
    public class InstancedTile : MonoBehaviour
    {
        public float TileBackPosZ, TileFrontPosZ;
        [SerializeField] private GameObject edgeTileBack, edgeTileFront;
        
        private void Start()
        {
            TilesQueue.OnTilesShift += RefreshEdgePositions;
            GameManager.OnRestart += RefreshEdgePositions;
            RefreshEdgePositions();
        }
        private void OnDestroy() 
        {
            TilesQueue.OnTilesShift -= RefreshEdgePositions;
            GameManager.OnRestart -= RefreshEdgePositions;
        }
        private void RefreshEdgePositions() // need to call this all the time update
        {
            TileBackPosZ = edgeTileBack.transform.position.z;
            TileFrontPosZ = edgeTileFront.transform.position.z;
        }
    }
}