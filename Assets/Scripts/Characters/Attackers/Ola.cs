using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class Ola : MonoBehaviour
{
    private Attacker _attacker;
    private Animator _animator;

    public GameObject Ball;

    public const float ThrowDistance = 2f;
    public const float DashSpeed = 3.8f;

    public bool IsEnemyHit
    {
        get { return _animator.GetBool(AnimatorControllerFlags.IsEnemyHit); }
        set { _animator.SetBool(AnimatorControllerFlags.IsEnemyHit, value); }
    }

    public bool IsAttacking
    {
        get { return GetComponent<Animator>().GetBool(AnimatorControllerFlags.IsAttacking); }
        set { _animator.SetBool(AnimatorControllerFlags.IsAttacking, value); ; }
    }

    public void Start()
    {
        _attacker = GetComponent<Attacker>();
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Defenders>() == null) return;
        PerformAttack(collider.gameObject);
    }

    public void PerformAttack(GameObject gameObject)
    {
        _attacker.UpdateCurrentDefender(gameObject);
        IsEnemyHit = false;
        IsAttacking = true;
    }

    public void ThrowBall()
    {
        if (Ball == null) return;
        GameObject magicBall = Instantiate(Ball, new Vector3(transform.position.x - 0.2f, transform.position.y + 0.2f), Quaternion.identity, transform);
        magicBall.GetComponent<MagicBall>().SetOwner(gameObject);
        StartCoroutine(UpdateBallPositionOrPullToEnemy(magicBall));
    }

    public void PullToEnemy(GameObject magicBall)
    {
        IsEnemyHit = true;
        _attacker.SetSpeed(DashSpeed);
        _attacker.StrikeTheTarget(MagicBall.BallDamage);
        _animator.Play(Animations.OlaPulling);
        DestroyObject(magicBall);
    }

    private IEnumerator UpdateBallPositionOrPullToEnemy(GameObject magicBall)
    {
        if (gameObject == null) yield break;
        if (magicBall == null) yield break;
        float ballDistance;
        do
        {
            if (magicBall.GetComponent<MagicBall>().IsEnemyHit())
            {
                PullToEnemy(magicBall);
                yield break;
            };

            ballDistance = Mathf.Abs(transform.position.x - magicBall.transform.position.x);
            magicBall.transform.Translate(Vector2.left * MagicBall.BallSpeed * Time.deltaTime);
            yield return null;
        } while (ballDistance < ThrowDistance);

        _animator.Play(Animations.Walking);
        DestroyObject(magicBall);
    }
}

