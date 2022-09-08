using UnityEngine;
using UnityEngine.SceneManagement;

namespace MemezawyDev.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private GameObject _pausedScreen;
        [SerializeField] private GameObject _enemyInScene;

        public bool IsPaused { get; private set; }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            QualitySettings.vSyncCount = 1;
        }
        // Called by a button.
        public void Quit()
        {
            Application.Quit();
        }

        public void GameOver()
        {
            Time.timeScale = 0;
            _gameOverScreen.SetActive(true);
        }
        
        public void PauseGame()
        {
            print("paused");
            Time.timeScale = 0;
            _pausedScreen.SetActive(true);
            IsPaused = true;
        }        
        public void ResumeGame()
        {
            print("resumed");
            Time.timeScale = 1;
            _pausedScreen.SetActive(false);
            IsPaused = false;
        }
        public void StartGame()
        {
            SceneManager.LoadSceneAsync(0);
            Time.timeScale = 1;
        }
    }      
        
}
