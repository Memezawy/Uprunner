using System.Collections;
using UnityEngine;
using MemezawyDev.Core;

namespace MemezawyDev.Enemy
{
    public class ChaserEnemy : MonoBehaviour
    {
        [SerializeField] private float _followRate;
        private Vector2 pos;
        private bool _canGo = false;

        private void Start()
        {
            pos = transform.position; // to avoid it always starting at (0,0).
            StartCoroutine(HeadStart(2)); // Gives the player some time to move
        }

        private void FixedUpdate()
        {
            if (!_canGo) return; // applies the headstart.
            pos.x = Mathf.Lerp(pos.x, Player.Player.Instance.transform.position.x, Time.deltaTime * _followRate);
            pos.y = Mathf.Lerp(pos.y, Player.Player.Instance.transform.position.y, Time.deltaTime * _followRate);
            transform.position = pos;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                print("GameOver");
                GameManager.Instance.GameOver();
            }
        }
        private IEnumerator HeadStart(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            _canGo = true;
        }
    }
}
