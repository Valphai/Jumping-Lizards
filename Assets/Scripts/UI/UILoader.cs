using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UILoader : MonoBehaviour
    {
        private void Awake() 
        {
            if(!SceneManager.GetSceneByName("UI").isLoaded)
                SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            else
                SceneManager.UnloadSceneAsync("UI");
        }
    }
}
