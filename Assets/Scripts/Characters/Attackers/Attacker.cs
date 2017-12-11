using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HealthComponent))]
public class Attacker : MonoBehaviour
{
    private float _currentSpeed;
    private GameObject _currentDefender;
    private Animator _animator;
    private HealthComponent _healthComponent;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _healthComponent = GetComponent<HealthComponent>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * _currentSpeed * Time.deltaTime);
    }

    public void SetSpeed(float speed)
    {
        _currentSpeed = speed;
    }

    public void StrikeTheTarget(float damage)
    {
        if (_currentDefender == null)
        {
            _animator.SetBool(AnimatorControllerFlags.IsAttacking, false);
            return;
        }
        if ((_currentDefender.GetComponent<HealthComponent>().Health -= damage) <= 0)
        {
            _animator.SetBool(AnimatorControllerFlags.IsAttacking, false);
            DestroyObject(_currentDefender);
        }
    }

    public void ReceiveDamage(float damage)
    {
        if ((_healthComponent.Health -= damage) <= 0)
        {
            Spawners.Attackers--;
            DestroyObject(gameObject);
        }
    }

    public void UpdateCurrentDefender(GameObject gameObject)
    {
        _currentDefender = gameObject;
    }
}
