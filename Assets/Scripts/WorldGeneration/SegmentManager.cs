using System.Collections.Generic;
using UnityEngine;

namespace MemezawyDev.WorldGeneration
{
    public class SegmentManager : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        private const float _distanceToDisable = 2f;
        private readonly List<GameObject> _spwaned = new List<GameObject>();
        private void Start()
        {
            LoadSegment();
        }
        private void Update()
        {
            if (Player.Player.Instance.transform.position.y >= _spwaned[_spwaned.Count -1].transform.position.y) // when half way through the last segment.
            {
                LoadSegment();
            }

            // Check if the player is away enough then disable the segment to save preformance.
            foreach (var _segment in _spwaned)
            {
                if (Vector2.Distance(_segment.transform.position, Player.Player.Instance.transform.position) >=
                    _segment.transform.localScale.y * _distanceToDisable)
                {
                    _segment.SetActive(false);
                }
                else
                {
                    _segment.SetActive(true);
                }
            }
        }

        private void LoadSegment()
        {
            var seg = Instantiate(_gameObject);
            // Y = (Leaves space enough so no segments collid) + (adds up the distance needed to be away from any segments)
            seg.transform.position = new Vector2(0f,
                0.5f * _gameObject.transform.localScale.y + _spwaned.Count * _gameObject.transform.localScale.y); 
            _spwaned.Add(seg);
        }
    }
}
