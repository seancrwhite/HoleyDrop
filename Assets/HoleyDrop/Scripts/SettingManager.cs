using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingManager : MonoBehaviour {
    private Slider gameVolumeSlider;
    private Slider sfxVolumeSlider;
    private Slider musicVolumeSlider;

    private GameSettings gameSettings;

    void Start()
    {
        gameSettings = new GameSettings();

        gameVolumeSlider = GetComponent<Slider>();
        sfxVolumeSlider = GetComponent<Slider>();
        musicVolumeSlider = GetComponent<Slider>();

        gameVolumeSlider.onValueChanged.AddListener(delegate { OnGameVolumeChange(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { OnSfxVolumeChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { OnMusicVolumeChange(); });

        gameVolumeSlider.value = AudioListener.volume;
        sfxVolumeSlider.value = 1f;
        musicVolumeSlider.value = 1f;
    }

    public void OnGameVolumeChange()
    {
        AudioListener.volume = gameSettings.GameVolume = gameVolumeSlider.value;
    }

    public void OnSfxVolumeChange()
    {

    }

    public void OnMusicVolumeChange()
    {

    }
}
