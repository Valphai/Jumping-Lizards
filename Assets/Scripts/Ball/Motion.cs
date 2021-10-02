using UnityEngine;
using System;
using UI;

namespace Ball
{
    public class Motion : MonoBehaviour
    {
        public static event Action OnJump;
        private bool moveAllowed;
        private float jumpValue, speedValue;
        private Rigidbody rb;
        [SerializeField] private float jumpIncrementSpeed, speedIncrementSpeed;
        [SerializeField] private float jumpMaxValue, speedMaxValue;
        [SerializeField] private float flipBoundry;
        [SerializeField] private Transform colliderTransform;
        [SerializeField] private KeyCode jumpKey;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            GameManager.OnRestart += ResetPosition;
            GameManager.OnEnd += DisableMovement;
        }
        private void OnDisable() 
        {
            GameManager.OnRestart -= ResetPosition;
            GameManager.OnEnd -= DisableMovement;
        }
        private void Update()
        {
            ColliderFollow();
            if (moveAllowed)
                PlayerInput();
        }
        private void PlayerInput()
        {
            // if (Input.GetKeyDown(jumpKey))//Input.touchCount > 0)
            // {
                // Touch touch = Input.GetTouch(0);

                if (Input.GetKey(jumpKey))//touch.phase == TouchPhase.Stationary)
                {
                    jumpValue += IncrementValue(jumpValue, jumpMaxValue, jumpIncrementSpeed);
                    speedValue += IncrementValue(speedValue, speedMaxValue, speedIncrementSpeed);
                }
                else if (Input.GetKeyUp(jumpKey))//touch.phase == TouchPhase.Ended)
                {
                    JumpLogic();
                }
            // }
        }
        private void JumpLogic()
        {
            float boost = (2.5f - transform.localPosition.y) * 5 + 2;
            rb.useGravity = true;
            Vector3 jump = Vector3.up * (jumpValue + boost);
            Vector3 forward = Vector3.forward * (speedValue + boost);;

            if (transform.localPosition.y <= flipBoundry)
            {
                rb.velocity = jump + forward;
                RandomRotation();
                OnJump?.Invoke();
            }
            else
            {
                moveAllowed = false;
                rb.velocity = Vector3.down;
            }
            
            jumpValue = 0;
            speedValue = 0;
        }
        private void RandomRotation()
        {
            float randY = UnityEngine.Random.Range(0,360);
            float randX = UnityEngine.Random.Range(0,360);
            float randZ = UnityEngine.Random.Range(0,360);
            Vector3 randomRotation = new Vector3(randX, randY, randZ);
            rb.AddTorque(randomRotation, ForceMode.Impulse);
        }
        private void ColliderFollow() => colliderTransform.position = new Vector3(0, 0, transform.position.z);
        private void OnCollisionEnter(Collision collision) => GameManager.GameEnd();
        private void DisableMovement()
        {
            rb.useGravity = false;
            moveAllowed = false;
        }
        private void ResetPosition()
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = Vector3.up * 2.5f;
            transform.rotation = Quaternion.identity;
            rb.useGravity = false;
            moveAllowed = true;
        }
        private float IncrementValue(float val, float max, float incrementSpeed)
        {
            if (val >= max) return 0;
            return incrementSpeed * Time.deltaTime;
        }
    }
}