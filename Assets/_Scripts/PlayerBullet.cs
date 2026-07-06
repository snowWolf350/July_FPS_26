using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float _bulletDamage = 30;

    public void Shoot(Vector3 velocity)
    {
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
