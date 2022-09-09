using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

namespace MemezawyDev.Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        private int _tallestHeight;

        private void Update()
        {
            if (Player.Player.Instance.transform.position.y > _tallestHeight)
            {
                _tallestHeight = (int)Player.Player.Instance.transform.position.y;
                _scoreText.text = "Score : " + _tallestHeight;
            }
        }

        public static void HandlePause(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (GameManager.Instance.IsPaused)
            {
                GameManager.Instance.ResumeGame();
            }
            else
            {
                GameManager.Instance.PauseGame();
            }

        }
    }
}