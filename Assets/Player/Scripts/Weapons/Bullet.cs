using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace MemezawyDev.Player.Weapons
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletData _bulletData;
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            _rigidbody2D.AddForce(transform.right * _bulletData.speed, ForceMode2D.Impulse);
            //DestroyAfterAsync(_bulletData.lifeTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // DO Damage
            // Destory
        }

        private async void DestroyAfterAsync(float seconds)
        {
            await Task.Delay((int)seconds * 1000);
            if (!this) { return; }
            Destroy(gameObject);
        }
    }
}
