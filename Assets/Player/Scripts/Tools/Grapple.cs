using UnityEngine;
using MemezawyDev.Player.Input;
using MemezawyDev.Managers;

namespace MemezawyDev.Player.Tools
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(SpringJoint2D))]
    public class Grapple : MonoBehaviour
    {
        [SerializeField] private string _hangsTag;
        [SerializeField] private float _endGrapplePush;
        [SerializeField] private float _maxGrappleDistance;

        private Player _player;
        private PlayerInputManager _playerInputManager;
        private LineRenderer _lineRenderer;
        private SpringJoint2D _springJoint;
        private Vector2 _grapplePos;
        private GameObject _grapplingObject;
        private bool _isGrappling;
        private void Start()
        {
            // To avoid Null expection
            _player = Player.Instance;
            _playerInputManager = _player.Input;
            _lineRenderer = GetComponent<LineRenderer>();
            _springJoint = GetComponent<SpringJoint2D>();

            // makes sure the components are off when the game starts
            _lineRenderer.enabled = false;
            _springJoint.enabled = false;
            _springJoint.frequency = 0;
        }

        private void Update()
        {
            if (_playerInputManager.Grapple == PlayerInputManager.InputSate.Preformed && CanGrapple())
            {
                StartGrapple();
            }
            if (_isGrappling)
            {
                UpdateGrapple();
            }

            if (_grapplePos == Vector2.zero) return;
            // End Grapple Early.
            if (_player.Input.CancelGrapple == PlayerInputManager.InputSate.Preformed && _isGrappling)
            {
                EndGrapple();
            }
            if (Vector2.Distance(transform.position, _grapplePos ) <= 0.01f && _isGrappling)
            {
                EndGrapple();
            }
        }

        private void StartGrapple()
        {
            _isGrappling = true;
            _player.Movement.enabled = false;
            _player.PhysicsController.Collision = false;
        }

        private void EndGrapple()
        {
            _isGrappling = false;
            _springJoint.enabled = false;
            _lineRenderer.enabled = false;
            // Turns the movement back on
            _player.Movement.enabled = true;
            _player.PhysicsController.StopTheBody();
            _player.PhysicsController.Collision = true;
            _grapplingObject = null; // so that we can grapple to the same platform twice. 
        }

        private void UpdateGrapple()
        {
            UpdateLineRenderer();
            _springJoint.enabled = true;
            _springJoint.anchor = Vector2.zero;
            _springJoint.connectedAnchor = _grapplePos;
        }
        private void UpdateLineRenderer()
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, new Vector2(_grapplePos.x, _grapplingObject.transform.position.y)); // To make it look like it's grapping to the platform
        }

        private bool CanGrapple()
        {
            var hang = MouseManager.Instance.HoveringOver;
            if (hang == null || MouseManager.Instance.ColiderContactPoint == Vector2.zero || _grapplingObject == hang) return false;
            // To avoid grappling to a lower platform.
            if (hang.transform.position.y < _player.transform.position.y) return false;
            if (hang.CompareTag(_hangsTag) && Vector2.Distance(transform.position, hang.transform.position) <= _maxGrappleDistance)
            {
                // So that at the end the player always ends up above the platform
                _grapplePos = MouseManager.Instance.ColiderContactPoint + new Vector2(0, _endGrapplePush);
                _grapplingObject = hang;
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
