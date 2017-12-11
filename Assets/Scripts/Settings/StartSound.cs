using System.Collections;
using UnityEngine;

public class StartSound : MonoBehaviour
{

    private const string StartSceneName = "01aStartMenu";

	void Start ()
    {
        StartCoroutine(LoadStartMenu());
    }

    private IEnumerator LoadStartMenu()
    {
        yield return new WaitForSeconds(3.5f);
        FindObjectOfType<LevelManager>().LoadLevel(StartSceneName);
    }
}
