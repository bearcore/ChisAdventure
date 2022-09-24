using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class ResetScene: MonoBehaviour
    {
        [SerializeField]
        private int sceneIndex;

        [Header("GameOver > ResetCurrentScore")]
        [SerializeField]
        private UnityEvent resetSceneEvent;

        public void ResetTheScene()
        {
            resetSceneEvent?.Invoke();
            SceneManager.LoadScene(sceneIndex);

        }
    }
}