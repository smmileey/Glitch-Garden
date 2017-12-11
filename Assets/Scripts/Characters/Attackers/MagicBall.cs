using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public const float BallSpeed = 3.2f;
    public const float BallDamage = 10f;

    private GameObject _owner;
    private bool _isEnemyHit;

    public void SetOwner(GameObject owner)
    {
        _owner = owner;
    }

    public bool IsEnemyHit()
    {
        return _isEnemyHit;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Defenders>() == null) return;
        if (transform.position.x < collider.transform.position.x) return;
        if (_owner == null) Debug.LogError("Ola not found!");

        _isEnemyHit = true;
        _owner.GetComponent<Attacker>().UpdateCurrentDefender(collider.gameObject);
    }
}
