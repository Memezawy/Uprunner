using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace MemezawyDev
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource, _sfxSource;
        [SerializeField] private AudioMixer _audioMixer;
        public static SoundManager Instance { get; private set; }

        public const string MIXER_MASTER = "MasterVolume";
        public const string MIXER_MUSIC = "MusicVolume";
        public const string MIXER_SFX = "SFXVoulme";

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

        public void PlaySound(AudioClip clip)
        {
            _sfxSource.PlayOneShot(clip);
        }

        public void SetMusic(AudioClip music)
        {
            _musicSource.clip = music;
            _musicSource.loop = true;
        }

        public void ChangeVolume(float value, string channel)
        {
            _audioMixer.SetFloat(channel, value);
        }

        public void ToggleSFX()
        {
            _sfxSource.mute = !_sfxSource.mute;
        }

        public void ToggleMusic()
        {
            _musicSource.mute = !_musicSource.mute;
        }
    }
}
