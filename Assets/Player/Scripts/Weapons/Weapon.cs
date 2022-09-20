using UnityEngine;

namespace MemezawyDev.Player.Weapons
{
    public class Weapon : WeaponBase
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private int _maxAmmo;
        [Tooltip("The amount of bullets per second")]
        [SerializeField] private float _fireRate;

        public float gunKick;
        private int _currentAmmo = int.MaxValue; // Temp
        private float _nextShootTime;

        private void Shoot()
        {
            Instantiate(_bullet, _firePoint.position, _firePoint.rotation);
        }

        public override bool Fire()
        {
            if (Time.time > _nextShootTime && _currentAmmo > 0)
            {
                Shoot();
                _nextShootTime = Time.time + 1 / _fireRate;
                _currentAmmo--;
                return true;
            }
            return false;
        }

    }
}
