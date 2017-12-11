using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{

    public List<AudioClip> LevelAudioClips;
    public AudioClip LevelComplete;

    private AudioSource _audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        SetVolume(PlayerPrefsManager.Instance.GetMasterVolume());
    }

    void OnLevelWasLoaded(int level)
    {
        PlayClip(level);
    }

    internal void SetVolume(float value)
    {
        _audioSource.volume = value;
    }

    private void PlayClip(int level)
    {
        //_audioSource = GetComponent<AudioSource>();
        if (LevelAudioClips != null && level < LevelAudioClips.Count)
        {
            AudioClip nextClipToPlay = LevelAudioClips[level];
            if (nextClipToPlay != null && nextClipToPlay != _audioSource.clip)
            {
                _audioSource.clip = LevelAudioClips[level];
                _audioSource.loop = true;
                _audioSource.Play();
            }
        };
    }
}
