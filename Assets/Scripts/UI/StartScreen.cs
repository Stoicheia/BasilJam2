using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class StartScreen : MonoBehaviour
    {
        public void LoadMainScene()
        {
            SceneManager.LoadScene("Main");
        }
    }
}