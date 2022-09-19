using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemezawyDev
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource, _effectSource;
        public static SoundManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void PlaySound(AudioClip clip)
        {
            _effectSource.PlayOneShot(clip);
        }
    }
}
