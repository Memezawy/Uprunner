using MemezawyDev.Core.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace MemezawyDev.WorldGeneration
{
    public class SegmentManager : MonoBehaviour
    {
        [SerializeField] private GameObject _segment;
        private const float _distanceToDisable = 2f;
        private readonly List<GameObject> _spwaned = new List<GameObject>();
        private ObjectPool<Segment> _segmentsPool;
        private void Start()
        {
            LoadSegment();
            _segmentsPool = new ObjectPool<Segment>(() =>
            { return Instantiate(_segment).GetComponent<Segment>(); },
                segment => { segment.gameObject.SetActive(true); },
                segment => { segment.gameObject.SetActive(false); },
                3);

        }
        private void Update()
        {
            if (Player.Player.Instance.transform.position.y >= _spwaned[_spwaned.Count - 1].transform.position.y) // when half way through the last segment.
            {
                LoadSegment();
            }

            // Check if the player is away enough then disable the segment to save preformance.
            foreach (GameObject _segment in _spwaned)
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
            GameObject seg = Instantiate(_segment);
            // Y = (Leaves space enough so no segments collid) + (adds up the distance needed to be away from any segments)
            seg.transform.position = new Vector2(0f,
                0.5f * _segment.transform.localScale.y + _spwaned.Count * _segment.transform.localScale.y);
            _spwaned.Add(seg);
        }
    }
}
