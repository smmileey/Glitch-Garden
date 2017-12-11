using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class GameTime : MonoBehaviour
{
    public int LevelTimeInSeconds = 2000;

    private MusicManager _musicManager;
    private LevelManager _levelManager;
    private WinMessage _winMessage;
    private Text _progressLabel;
    private Slider _gameTimeSlider;
    private bool _levelFinished;
    private float _gameProgressInPercentage;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Text>()) _progressLabel = child.gameObject.GetComponent<Text>();
        }
        _gameTimeSlider = GetComponent<Slider>();
        _musicManager =  FindObjectOfType<MusicManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        _winMessage = FindObjectOfType<WinMessage>();

        if (_progressLabel == null) Debug.LogError("ProgressLabel is not set.");
        if (_musicManager == null) Debug.LogWarning("MusicManager not found.");
        if (_levelManager == null) Debug.LogWarning("LevelManager not found.");
        if (_winMessage == null) Debug.LogWarning("WinMessage not found.");
    }

    void Update()
    {
        if (!_levelFinished)
        {
            _gameProgressInPercentage = Time.timeSinceLevelLoad / LevelTimeInSeconds * 100;
            int gameProgressClamped = (int) Mathf.Clamp(_gameProgressInPercentage, 0f, 100f);
            _progressLabel.text = $"{gameProgressClamped} %";
            _gameTimeSlider.value = gameProgressClamped;

            if (gameProgressClamped == 100)
            {
                _levelFinished = true;
                Time.timeScale = 0;
                DisplayWinScreen();
                PlayLevelCompleteSound();
                StartCoroutine(LoadNextLevel());
            }
        }
    }

    public float Progress { get { return _gameProgressInPercentage; } }

    private void DisplayWinScreen()
    {
        _winMessage.PopUp();
    }

    private void PlayLevelCompleteSound()
    {
        var audioSource = _musicManager.GetComponent<AudioSource>();
        audioSource.clip = _musicManager.LevelComplete;
        audioSource.loop = false;
        audioSource.Play();
    }

    private IEnumerator LoadNextLevel()
    {
        float realTimeSinceStartUp = Time.realtimeSinceStartup;
        while(Time.realtimeSinceStartup < realTimeSinceStartUp + _musicManager.LevelComplete.length + 1)
        {
            yield return null;
        }

        _levelManager.LoadNextLevelOrFinishScreen();
        Time.timeScale = 1;
    }
}
