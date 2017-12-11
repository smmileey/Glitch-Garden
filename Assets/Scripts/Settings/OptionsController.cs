using System;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{

    public Slider VolumeSlider;
    public Slider DifficultySlider;

    private MusicManager _musicManager;
    private LevelManager _levelManager;

    void Start()
    {
        _musicManager = FindObjectOfType<MusicManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        RestoreSettingsFromPlayerPrefs();
    }

    void Update()
    {
        _musicManager.SetVolume(VolumeSlider.value);
    }

    public void SaveOptionsAndBackToStartMenu()
    {
        SaveMasterVolume();
        SaveDifficultyLevel();
        BackToStartMenu();
    }

    public void RestoreDefaults()
    {
        VolumeSlider.value = Defaults.MasterVolume;
        DifficultySlider.value = Defaults.Difficulty;
    }

    private void SaveMasterVolume()
    {
        PlayerPrefsManager.Instance.SetMasterVolume(VolumeSlider.value);
    }

    private void SaveDifficultyLevel()
    {
        PlayerPrefsManager.Instance.SetDifficulty(DifficultySlider.value);
    }

    private void BackToStartMenu()
    {
        _levelManager.LoadLevel(Levels.StartMenu);
    }

    private void RestoreSettingsFromPlayerPrefs()
    {
        RestoreMasterVolume();
        RestoreDifficultyLevel();
    }

    private void RestoreMasterVolume()
    {
        VolumeSlider.value = PlayerPrefsManager.Instance.GetMasterVolume();
    }

    private void RestoreDifficultyLevel()
    {
        DifficultySlider.value = PlayerPrefsManager.Instance.GetDifficulty();
    }
}
