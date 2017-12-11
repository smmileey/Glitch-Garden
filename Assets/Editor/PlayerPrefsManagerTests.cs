using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerPrefsManagerTests
{

    [Test]
    [TestCaseSource("VolumeDataCorrect")]
    public void SetMasterVolume_CorrectValue_UpdatesPlayerPrefs(float volume)
    {
        PlayerPrefsManager.Instance.SetMasterVolume(volume);

        float actualVolume = PlayerPrefs.GetFloat("master_volume");
        Assert.AreEqual(volume, actualVolume);
    }

    [Test]
    [TestCaseSource("VolumeDataIncorrect")]
    public void SetMasterVolume_IncorrectValue_ThrowsError(float volume)
    {
        PlayerPrefsManager.Instance.SetMasterVolume(volume);

        LogAssert.Expect(LogType.Error, $"Master volume not in range {volume}");
    }

    [Test]
    [TestCaseSource("DifficultyDataCorrect")]
    public void SetDifficulty_CorrectValue_UpdatesPlayerPrefs(float difficulty)
    {
        PlayerPrefsManager.Instance.SetDifficulty(difficulty);

        float actualDifficulty = PlayerPrefs.GetFloat("difficulty");
        Assert.AreEqual(difficulty, actualDifficulty);
    }

    [Test]
    [TestCaseSource("DifficultyDataIncorrect")]
    public void SetDifficulty_IncorrectValue_ThrowsError(float difficulty)
    {
        PlayerPrefsManager.Instance.SetDifficulty(difficulty);

        LogAssert.Expect(LogType.Error, $"Difficulty not in range {difficulty}");
    }

    [Test]
    public void UnlockLevel_CorrectValue_UpdatesPlayersPrefs([Values(-1, 0)]int levelIndex)
    {
        PlayerPrefsManager.Instance.UnlockLevel(levelIndex);

        int unlockStatus = PlayerPrefs.GetInt($"level_lock_{levelIndex}");
        Assert.AreEqual(1, unlockStatus);
    }

    [Test]
    public void UnlockLevel_InorrectValue_ThrowsError([Values(int.MaxValue)]int levelIndex)
    {
        PlayerPrefsManager.Instance.UnlockLevel(levelIndex);

        int unlockStatus = PlayerPrefs.GetInt($"level_lock_{levelIndex}");
        LogAssert.Expect(LogType.Error, $"Level index { levelIndex } exceeds number of scenes defined in project build settings.");
    }

    [Test]
    public void GetMasterVolume_ReturnsCorrectValue()
    {
        float expectedMasterVolume = 0.3f;
        PlayerPrefs.SetFloat("master_volume", expectedMasterVolume);

        Assert.AreEqual(expectedMasterVolume, PlayerPrefsManager.Instance.GetMasterVolume());
    }

    [Test]
    public void GetDifficulty_ReturnsCorrectValue()
    {
        float expectedDifficulty = 0.2f;
        PlayerPrefs.SetFloat("difficulty", expectedDifficulty);

        Assert.AreEqual(expectedDifficulty, PlayerPrefsManager.Instance.GetDifficulty());
    }

    [Test]
    public void IsLevelUnlocked_CorrectValue_ReturnsCorrectValue([Values(-1, 0)] int levelIndex)
    {
        PlayerPrefs.SetInt($"level_lock_{levelIndex}", 1);

        Assert.AreEqual(true, PlayerPrefsManager.Instance.IsLevelUnlocked(levelIndex));
    }

    [Test]
    public void IsLevelUnlocked_IncorrectValue_ThrowsError([Values(int.MaxValue)] int levelIndex)
    {
        PlayerPrefsManager.Instance.IsLevelUnlocked(levelIndex);
        LogAssert.Expect(LogType.Error, $"Level index { levelIndex } exceeds number of scenes defined in project build settings.");
    }

    private static IEnumerable<float> VolumeDataCorrect()
    {
        yield return 0f;
        yield return 0.2f;
        yield return 1f;
    }

    private static IEnumerable<float> VolumeDataIncorrect()
    {
        yield return -1f;
        yield return -0.3f;
        yield return 5f;
    }

    private static IEnumerable<float> DifficultyDataCorrect()
    {
        yield return 1f;
        yield return 2.5f;
        yield return 3f;
    }

    private static IEnumerable<float> DifficultyDataIncorrect()
    {
        yield return 0f;
        yield return -1.000001f;
        yield return 3.000001f;
    }
}
