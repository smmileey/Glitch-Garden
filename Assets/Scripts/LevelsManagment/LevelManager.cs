using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

	public void LoadLevel(string name)
    {
		SceneManager.LoadScene(name);
	}

	public void QuitRequest()
    {
		Application.Quit ();
	}

    public void LoadNextLevelOrFinishScreen()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextLevelIndex >= SceneManager.sceneCountInBuildSettings) SceneManager.LoadScene(Levels.WinScreen);
        SceneManager.LoadScene(nextLevelIndex);
    }
}
