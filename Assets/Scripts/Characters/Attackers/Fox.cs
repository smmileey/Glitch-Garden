using System;
using UnityEngine;
[RequireComponent(typeof(Attacker))]

public class Fox : MonoBehaviour
{
    private Animator _animator;
    private Attacker _attacker;

    void Start()
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
        if (defender.GetComponent<Gravestone>() != null) _animator.SetBool(AnimatorControllerFlags.IsJumping, true);
        else
        {
            _attacker.UpdateCurrentDefender(defender);
            _animator.SetBool(AnimatorControllerFlags.IsAttacking, true);
        }
    }
}
