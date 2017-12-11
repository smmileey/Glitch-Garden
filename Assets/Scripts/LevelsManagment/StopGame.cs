using UnityEngine;

public class StopGame : MonoBehaviour
{
    private LevelManager _levelManager;

	void Start ()
    {
        _levelManager = FindObjectOfType<LevelManager>();	
	}

    void OnMouseDown()
    {
        _levelManager.LoadLevel(Levels.StartMenu);
    }
}
