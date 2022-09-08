using MemezawyDev.Player.Weapons;
using UnityEngine;
using MemezawyDev.Managers;

namespace MemezawyDev.Player.Shooting
{
    [RequireComponent(typeof(Player))]
    public class PlayerShootingManager : MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private float _distanceFromPlayer;
        private Player _player;

        private void Start()
        {
            _player = Player.Instance;
        }

        private void Update()
        {
            if (!weapon.gameObject.activeInHierarchy) return;
            if (_player.Input.Fire == Input.PlayerInputManager.InputSate.Preformed)
            {
                if (weapon.Fire() && !_player.Movement.Moving)
                {
                    _player.PhysicsController.AddForce((-MouseManager.Instance.GetRelativePositionTo(transform.position)) * weapon.gunKick);
                }
            }
            RotateWeaponToMouse();
            UpdateWeaponPosition();
        }

        private void RotateWeaponToMouse()
        {
            Vector3 difference = MouseManager.Instance.GetPosition() - (Vector2)transform.position;
            difference.Normalize();
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            weapon.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
            if (rotationZ < -90 || rotationZ > 90) // Looking left
            {
                _player.Movement.LookLeft();
                weapon.transform.localRotation = Quaternion.Euler(180f, 180f, -rotationZ);
            }
            else // Looking right
            {
                // the weapon is always looking to the right
                _player.Movement.LookRight();
            }
        }
        
        private void UpdateWeaponPosition()
        {
            Vector2 pos;
            if (MouseManager.Instance.GetRelativePositionTo(transform.position) == Vector2.zero && _player.Input.IsUsingController)
            {
                // Equation of st. line
                pos = weapon.transform.position = (Vector2)transform.position + _distanceFromPlayer * new Vector2(1f, 0f);
            }
            else
            {
                // Equation of st. line
                pos =  (Vector2)transform.position + _distanceFromPlayer * MouseManager.Instance.GetRelativePositionTo(transform.position);
            }
            weapon.transform.position = pos;
            var localPos = weapon.transform.localPosition;
            localPos.y = Mathf.Clamp(localPos.y, -0.5f, Mathf.Infinity);
            weapon.transform.localPosition = localPos;
        }
    }
}
