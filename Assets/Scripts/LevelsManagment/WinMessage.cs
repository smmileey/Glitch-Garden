using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WinMessage : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PopUp()
    {
        _animator.Play(Animations.WinMessagePopUp);        
    }
}
