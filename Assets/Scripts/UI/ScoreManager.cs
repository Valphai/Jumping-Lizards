using UnityEngine;
using TMPro;
using DataManagement;

namespace UI
{
    public class ScoreManager : MonoBehaviour
    {
        public static int Score;
        private bool calculationAllowed;
        private TMP_Text scoreText;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private TMP_Text cashText;

        private void Awake() 
        {
            playerTransform = GameObject.FindGameObjectWithTag("Ball").GetComponent<Transform>();
            scoreText = GetComponentInChildren<TMP_Text>();
            ConvertScore();
            scoreText.text = Score.ToString();
            GameManager.OnRestart += EnableCalculation;
            GameManager.OnEnd += DisableCalculation;
            GameManager.OnEnd += ConvertScore;
        }
        private void FixedUpdate() 
        {
            if (calculationAllowed)
                CalculateScore();
        }
        private void DisableCalculation() => calculationAllowed = false;
        private void EnableCalculation() => calculationAllowed = true;
        private void ConvertScore()
        {
            SaveData.DataSave.Cash += (int)(Score/15);
            Score = 0;
            UpdateText();
        }

        public void UpdateText() => cashText.text = SaveData.DataSave.Cash.ToString();

        private void OnDisable() 
        {
            GameManager.OnRestart -= EnableCalculation;
            GameManager.OnEnd -= DisableCalculation;
            GameManager.OnEnd -= ConvertScore;
            
        }
        private void CalculateScore()
        {
            Score = Mathf.Abs((int)playerTransform.position.z);
            scoreText.text = Score.ToString();
        }
    }
}
