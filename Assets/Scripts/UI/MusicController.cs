using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioMixer _mainMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundSlider;
    private float _tempValue;

    private void Start()
    {
        LoadSettings();
    }

    /// <summary>
    /// Изменение значения слайдера музыки
    /// </summary>
    /// <param name="sliderValue">Значение слайдера</param>
    public void SetMusicVolume(float sliderValue)
    {
        _mainMixer.SetFloat("MusicVolume", sliderValue);
    }

    /// <summary>
    /// Изменение значения слайдера звуков
    /// </summary>
    /// <param name="sliderValue">Значение слайдера</param>
    public void SetSoundsVolume(float sliderValue)
    {
        _mainMixer.SetFloat("SoundsVolume", sliderValue);
    }

    /// <summary>
    /// Настройки сохраняются при выходе из меню настроек
    /// </summary>
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
        PlayerPrefs.SetFloat("SoundsVolume", _soundSlider.value);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        _tempValue = PlayerPrefs.GetFloat("MusicVolume");
        _musicSlider.value = _tempValue;
        _mainMixer.SetFloat("MusicVolume", _tempValue);

        _tempValue = PlayerPrefs.GetFloat("SoundsVolume");
        _soundSlider.value = _tempValue;
        _mainMixer.SetFloat("SoundsVolume", _tempValue);
    }
}
