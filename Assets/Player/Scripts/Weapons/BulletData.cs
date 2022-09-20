using System.Collections;
using UnityEngine;

namespace MemezawyDev.Player.Weapons
{
    [CreateAssetMenu(menuName = "Weapons/BulletData")]
    public class BulletData : ScriptableObject
    {
        public float damage;
        public float speed;
        public float lifeTime;
        // Should add more like Sprite etc.
    }
}