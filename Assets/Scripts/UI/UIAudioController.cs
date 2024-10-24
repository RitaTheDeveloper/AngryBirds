using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UIAudioController : MonoBehaviour
{
    [SerializeField] private Slider _soundSlider;

    public AudioMixer audioMixer;

    private void Awake()
    {
        _soundSlider.value = PlayerPrefs.GetFloat("VolumeParameter", 1);
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(_soundSlider.value);
        // audioMixer.SetFloat("VolumeParam", _soundSlider.value);
        PlayerPrefs.SetFloat("VolumeParameter", _soundSlider.value);
        PlayerPrefs.Save();
    }
}
