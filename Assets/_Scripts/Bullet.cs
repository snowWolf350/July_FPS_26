using UnityEngine;

public class Bullet : MonoBehaviour
{
    float _bulletDamage = 30;

    public void Shoot(Vector3 velocity, Vector3 forward)
    {
        transform.up = forward;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.linearVelocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.TryGetComponent(out Enemy enemy))
        {
            enemy.GetEnemyHealth().TakeDamage(_bulletDamage);
        }

        Destroy(gameObject);
    }
}
