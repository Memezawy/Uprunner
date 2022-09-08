using UnityEngine;
using TMPro;

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
            if (Player.Player.Instance.Input.Pause == Player.Input.PlayerInputManager.InputSate.Started)
            {
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
}