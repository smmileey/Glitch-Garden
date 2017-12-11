using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Gravestone : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<Attacker>() == null) return;
        _animator.SetTrigger(Triggers.Attack);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Exit " + collider.name);
    }
}
