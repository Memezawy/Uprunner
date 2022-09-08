using UnityEngine;

namespace MemezawyDev.Managers
{
    public class MouseManager : MonoBehaviour
    {
        public Camera _cam;
        public static MouseManager Instance { get; private set; }
        public GameObject HoveringOver { get; private set; }
        public Vector2 ColiderContactPoint { get; private set; }

        

        private Player.Player _player;

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
        }

        private void Start()
        {
            _player = Player.Player.Instance;
        }

        private void Update()
        {
            transform.position = GetPosition();
        }

        public Vector2 GetPosition()
        {
            if (_player.Input.IsUsingController)
            {
                return (Vector2)_player.transform.position + (4 * _player.Input.Look.normalized); // to add some distance between them
            }
            else // Using a Mouse
            {
                return _cam.ScreenToWorldPoint(_player.Input.Look);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawCube(transform.position, Vector3.one);
        }

        public Vector2 GetRelativePositionTo(Vector2 pos)
        {
            if (_player.Input.IsUsingController)
            {
                return  _player.Input.Look.normalized;
            }
            else
            {
                return ((Vector2)transform.position - pos).normalized;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HoveringOver = collision.gameObject;
            ContactPoint2D[] contacts = new ContactPoint2D[1];
            collision.GetContacts(contacts);
            ColiderContactPoint = contacts[0].point;
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            HoveringOver = null;
            ColiderContactPoint = Vector2.zero;
        }
    
    }
}
