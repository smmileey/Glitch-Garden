using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : IPlayerPrefs
{

    const string MASTER_VOLUME = "master_volume";
    const string DIFFICULTY = "difficulty";
    const string LEVEL_LOCK_STATUS = "level_lock_";
    const string FIRST_RUN = "first_run";

    private static PlayerPrefsManager _playerPrefsManager;

    public static PlayerPrefsManager Instance { get { return _playerPrefsManager ?? (_playerPrefsManager = new PlayerPrefsManager()); } }

    private PlayerPrefsManager()
    {
    }

    public void SetMasterVolume(float volume)
    {
        if (volume < 0f || volume > 1f) Debug.LogError($"Master volume not in range {volume}");
        else PlayerPrefs.SetFloat(MASTER_VOLUME, volume);
    }

    public float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME);
    }

    public void FirstRunDone()
    {
        PlayerPrefs.SetInt(FIRST_RUN, 0);
    }

    public bool IsFirstRun()
    {
        return PlayerPrefs.GetInt(FIRST_RUN, 1) == 1;
    }

    public void SetDifficulty(float difficulty)
    {
        if (difficulty < 1f || difficulty > 3f) Debug.LogError($"Difficulty not in range {difficulty}");
        else PlayerPrefs.SetFloat(DIFFICULTY, difficulty);
    }

    public float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULTY);
    }

    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex <= SceneManager.sceneCountInBuildSettings - 1)
        {
            PlayerPrefs.SetInt($"{LEVEL_LOCK_STATUS}{levelIndex}", 1);
        }
        else Debug.LogError($"Level index {levelIndex} exceeds number of scenes defined in project build settings.");
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        if (levelIndex <= SceneManager.sceneCountInBuildSettings - 1) return PlayerPrefs.GetInt($"{LEVEL_LOCK_STATUS}{levelIndex}") == 1;
        else
        {
            Debug.LogError($"Level index {levelIndex} exceeds number of scenes defined in project build settings.");
            return false;
        }
    }
}
