
public interface IPlayerPrefs
{
    void SetMasterVolume(float volume);
    float GetMasterVolume();
    void SetDifficulty(float difficulty);
    float GetDifficulty();
    void UnlockLevel(int levelIndex);
    bool IsLevelUnlocked(int levelIndex);
}
