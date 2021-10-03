using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MainUI : MonoBehaviour
    {
        private Stack<GameObject> uiStack = new Stack<GameObject>();
        [SerializeField] private GameObject pausePanel;

        private void Awake() 
        {
            PushMain();
            GameManager.OnEnd += PushMain;
            GameManager.OnRestart += PopAll;
        }
        private void OnDestroy() 
        {
            GameManager.OnEnd -= PushMain;
            GameManager.OnRestart -= PopAll;
        }
        public void PushMain()
        {
            uiStack.Push(pausePanel);
            ToggleStackActivity(true);
        }
        public void Push(GameObject panel)
        {
            if (!uiStack.Contains(panel))
            {
                uiStack.Push(panel);
                ToggleStackActivity(true);
            }
        }
        public void PopAll()
        {
            ToggleStackActivity(false);
            uiStack.Clear();
        } 
        public void Pop()
        {
            ToggleStackActivity(true);
            uiStack.Pop();
        }
        private void ToggleStackActivity(bool active)
        {
            foreach (GameObject panel in uiStack)
            {
                panel.SetActive(active);
                panel.GetComponentInChildren<ButtonTween>()?.Perform();
            }
        }
        public void StartButton() => GameManager.RestartGame();
    }
}