using UnityEngine;

public class Enemy : MonoBehaviour
{
    Health _enemyHealth;

    private void Awake()
    {
        _enemyHealth = new Health(100);
    }

    private void Start()
    {
        _enemyHealth.OnDamange += _enemyHealth_OnDamange;
        _enemyHealth.onDeath += _enemyHealth_onDeath;
    }

    private void OnDestroy()
    {
        _enemyHealth.OnDamange -= _enemyHealth_OnDamange;
        _enemyHealth.onDeath -= _enemyHealth_onDeath;
    }

    private void _enemyHealth_onDeath(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    private void _enemyHealth_OnDamange(object sender, System.EventArgs e)
    {
        Debug.Log("enemyHit + " + _enemyHealth.GetHealthNormalized());
    }

    public Health GetEnemyHealth()
    {
        return _enemyHealth;
    }
}
