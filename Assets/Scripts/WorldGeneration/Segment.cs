using UnityEngine;

namespace MemezawyDev.WorldGeneration
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private GameObject _hangs;
        [SerializeField] private int _count;
        [SerializeField] private Vector2 _hangClearance;
        private GameObject[] _spwaned;

        // Cached
        private Vector2 _pos;

        private void Awake()
        {
            _spwaned = new GameObject[_count];
        }

        private void Start()
        {
            for (int i = 0; i < _count; i++)
            {
                _spwaned[i] = Instantiate(_hangs);
                Physics2D.SyncTransforms(); // Makes sure all the colliders are active so that CheckAvilablity() can work proberly.
                _spwaned[i].transform.position = CalculatePos();
                _spwaned[i].transform.parent = transform;
            }
        }

        // Relocates all the hangs
        private void RefreshPlacements()
        {
            if (_spwaned[0] == null) return; // if the 1st item is null exit to avoid a null exception
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

        private void OnEnable()
        {
            // for when the segment is reactived when the player gets close
            RefreshPlacements();
        }

        private bool CheckAvilablity(Vector2 pos, Vector2 scale)
        {
            // Overlaps a Box if it hit anything then false else true.
            var coliders = Physics2D.OverlapBox(pos, scale, 0f);
            if (coliders == null) return true;
            else
            {
                return false;    
            }
        }
    }
}
