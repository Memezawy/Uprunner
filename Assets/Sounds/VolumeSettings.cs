using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MemezawyDev
{
    public class VolumeSettings : MonoBehaviour
    {
        [SerializeField] private Slider _masterSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _sfxSlider;

        private void Start()
        {
            // Master vol
            SoundManager.Instance.ChangeVolume(Mathf.Log10(_masterSlider.value) * 20, SoundManager.MIXER_MASTER);
            _masterSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeVolume(Mathf.Log10(val) * 20, SoundManager.MIXER_MASTER));

            // Music vol
            SoundManager.Instance.ChangeVolume(Mathf.Log10(_musicSlider.value) * 20, SoundManager.MIXER_MUSIC);
            _musicSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeVolume(Mathf.Log10(val) * 20, SoundManager.MIXER_MUSIC));

            // SFX vol
            SoundManager.Instance.ChangeVolume(Mathf.Log10(_sfxSlider.value) * 20, SoundManager.MIXER_SFX);
            _sfxSlider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeVolume(Mathf.Log10(val) * 20, SoundManager.MIXER_SFX));
        }
    }
}
