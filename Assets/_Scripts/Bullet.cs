using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void Shoot(Vector3 velocity, Vector3 forward)
    {
        transform.up = forward;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.linearVelocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit with : " + collision.gameObject.name);
        Destroy(gameObject);
    }
}
