using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float Damage = 5f;
    public float Speed = 1f;

	void Update ()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Attacker attacker = collider.gameObject.GetComponent<Attacker>();
        if (attacker == null) return;

        attacker.ReceiveDamage(Damage);
        DestroyObject(gameObject);
    }
}
