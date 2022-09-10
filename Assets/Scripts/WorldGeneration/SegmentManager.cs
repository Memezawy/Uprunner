using MemezawyDev.Core.ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace MemezawyDev.WorldGeneration
{
    public class SegmentManager : MonoBehaviour
    {
        [SerializeField] private GameObject _segment;
        private readonly List<Segment> _spwaned = new List<Segment>();
        private ObjectPool<Segment> _segmentsPool;
        private bool _bActive = false;
        private bool _AActive = false;

        public static SegmentManager Instance { get; private set; }
        public Segment CurrentSegment { get; set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            _segmentsPool = new ObjectPool<Segment>(() =>
            { return Instantiate(_segment).GetComponent<Segment>(); },
                segment =>
                {
                    segment.gameObject.SetActive(true);
                    _spwaned.Add(segment);
                },
                segment =>
                {
                    segment.gameObject.SetActive(false);
                    _spwaned.Remove(segment);
                },
                2);
            var firstSegment = _segmentsPool.Get();
            firstSegment.transform.position = new Vector2(0f, 0.5f * firstSegment.transform.lossyScale.y - 1f);
        }
        private void Update()
        {
            // 3/4 the way done.
            if (Player.Player.Instance.transform.position.y >= CurrentSegment.transform.position.y + 0.25f * CurrentSegment.transform.lossyScale.y)
            {
                if (_AActive) return;
                print("upper Part");
                MoveAbove();
                _AActive = true;
            }
            else if (Player.Player.Instance.transform.position.y <= CurrentSegment.transform.position.y - 0.25f * CurrentSegment.transform.lossyScale.y)
            {
                if (_bActive) return;
                print("lower Part");
                MoveBellow();
                _bActive = true;
            }
            else
            {
                print("Mid Part");

                foreach (var seg in _spwaned)
                {
                    if (seg == CurrentSegment) continue;
                    _segmentsPool.Return(seg);
                }
                _AActive = false;
                _bActive = false;
            }
        }
        private void MoveBellow()
        {
            var pos = new Vector2(0f, CurrentSegment.transform.position.y - _segment.transform.lossyScale.y);
            if (ContainsSegment(pos)) return;
            var segment = _segmentsPool.Get();
            segment.transform.position = pos;
        }
        private void MoveAbove()
        {
            var pos = new Vector2(0f, CurrentSegment.transform.position.y + _segment.transform.lossyScale.y);
            if (ContainsSegment(pos)) return;
            var segment = _segmentsPool.Get();
            segment.transform.position = pos;
        }

        private bool ContainsSegment(Vector3 pos)
        {
            foreach (var segment in _spwaned)
            {
                if (segment.transform.position == pos)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
