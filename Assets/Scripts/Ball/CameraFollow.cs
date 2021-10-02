using UnityEngine;

namespace Ball
{
    public class CameraFollow : MonoBehaviour
    {
        public Motion PlayerMotion;
        private Vector3 ball;
        [SerializeField] private float offsetX, offsetY, offsetZ;

        void Update()
        {
            ball = PlayerMotion.transform.position;
            transform.position = new Vector3(offsetX + (int)ball.x, offsetY, offsetZ + ball.z);
        }
    }
}
