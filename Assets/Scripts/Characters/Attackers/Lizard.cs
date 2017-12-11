using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class Lizard : MonoBehaviour
{
    private Animator _animator;
    private Attacker _attacker;

	void Start ()
    {
        _animator = GetComponent<Animator>();
        _attacker = GetComponent<Attacker>();	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Defenders>() == null) return;
        PerformAttack(collider.gameObject);
    }

    public void PerformAttack(GameObject defender)
    {
        _animator.SetBool(AnimatorControllerFlags.IsAttacking, true);
        _attacker.UpdateCurrentDefender(defender); 
    }
}
