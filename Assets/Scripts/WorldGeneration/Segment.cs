using System.Collections.Generic;
using UnityEngine;

namespace MemezawyDev.WorldGeneration
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private GameObject _hangs;
        [SerializeField] private int _count;
        [SerializeField] private Vector2 _hangClearance;
        private List<GameObject> _spwaned = new List<GameObject>();

        // Cached
        private Vector2 _pos;

        private void Start()
        {
            for (int i = 0; i < _count; i++)
            {
                _spwaned.Add(Instantiate(_hangs));
                Physics2D.SyncTransforms(); // Makes sure all the colliders are active so that CheckAvilablity() can work proberly.
                _spwaned[i].transform.position = CalculatePos();
                _spwaned[i].transform.parent = transform;
            }
        }

        // Relocates all the hangs
        public void RefreshPlacements()
        {
            print("Relocated");
            if (_spwaned.Count == 0) return; // if the 1st item is null exit to avoid a null exception
            foreach (var item in _spwaned)
            {
                item.transform.position = CalculatePos();
            }
        }

        private Vector2 CalculatePos()
        {
            // the boundries of the segment.
            _pos.x = Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2);
            // lowest point in the segment since pos.y is always > scale.y 
            _pos.y = Random.Range(transform.position.y - (transform.localScale.y / 2),
            (transform.localScale.y / 2) + transform.position.y); // the highest point in the segment.

            // Keep Getting a pos until it's valid.
            if (CheckAvilablity(_pos, _hangClearance))
            {
                return _pos;
            }
            else
            {
                return CalculatePos();
            }
        }

        private bool CheckAvilablity(Vector2 pos, Vector2 scale)
        {
            // Overlaps a Box if it hit anything then false else true.
            var colliders = Physics2D.OverlapBoxAll(pos, scale, 0f);
            foreach (var collider in colliders)
            {
                if (collider.isTrigger)
                {
                    continue;
                }
                else return false;
            }
            return true;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            SegmentManager.Instance.CurrentSegment = this;
        }

        private void OnEnable()
        {
            RefreshPlacements();
        }
    }
}
