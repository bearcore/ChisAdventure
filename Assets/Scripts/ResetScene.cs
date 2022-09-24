using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ResetScene: MonoBehaviour
    {
        [SerializeField]
        private int sceneIndex;
        public void ResetTheScene()
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}