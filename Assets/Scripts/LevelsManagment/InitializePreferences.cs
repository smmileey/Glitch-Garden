using UnityEngine;

public class InitializePreferences : MonoBehaviour
{
	void Awake()
    {
        PlayerPrefsManager prefsManager = PlayerPrefsManager.Instance;
        if (prefsManager.IsFirstRun())
        {
            prefsManager.SetMasterVolume(Defaults.MasterVolume);
            prefsManager.SetDifficulty(Defaults.Difficulty);
            prefsManager.FirstRunDone();
        }
    }
}
