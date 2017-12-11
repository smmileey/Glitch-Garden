using UnityEngine;

public class Shredder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        DestroyObject(collider.gameObject);
    }
}
