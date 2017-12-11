using UnityEngine;

public class LoseCollider : MonoBehaviour
{
    private LevelManager _levelManager;

    void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        if (_levelManager == null) Debug.LogError("No LevelManager found.");
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Attacker>() == null) return;
        _levelManager.LoadLevel(Levels.LoseScreen);
    }
}
