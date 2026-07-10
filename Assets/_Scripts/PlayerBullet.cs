using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    int _bulletDamage = 30;
    int _damageOffset = 10;

    public void Shoot(Vector3 velocity)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.linearVelocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.TryGetComponent(out Enemy enemy))
        {
            enemy.GetEnemyHealth().TakeDamage(_bulletDamage + Random.Range(0,_damageOffset));
        }

        Destroy(gameObject);
    }
}
