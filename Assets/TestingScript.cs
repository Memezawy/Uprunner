using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemezawyDev
{
    public class TestingScript : MonoBehaviour
    {
        public GameObject _hang;
        public float _size;

        private void Start()
        {
            Instantiate(_hang);
        }
        private void Update()
        {
            var pos = _hang.transform.position;
            var scale = _hang.transform.lossyScale;
            var _pointA = new Vector2(pos.x - scale.x, pos.y + scale.y);
            var _pointB = new Vector2(pos.x + scale.x, pos.y - scale.y);
            var coliders = Physics2D.OverlapArea(_pointA, _pointB);
            print($"Hang Collided with {coliders.name} at {coliders.transform.position}");
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(_hang.transform.position, _hang.transform.lossyScale * _size);
            
        }
    }

}
