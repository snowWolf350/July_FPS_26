using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Health _enemyHealth;

    //shooting 
    [SerializeField] Transform _shootTransform;
    [SerializeField] GameObject _bulletGameObject;
    float _fireRate = 3;
    float _fireTimer;
    float _shootForce = 20;
    float _playerHeight = 0.5f;
    float _minStopDistance = 7;
    float _maxStopDistance = 14;
    float _stopDistance;

    NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _enemyHealth = new Health(100);
    }

    private void Start()
    {
        _enemyHealth.OnDamange += _enemyHealth_OnDamange;
        _enemyHealth.onDeath += _enemyHealth_onDeath;

        _navMeshAgent = GetComponent<NavMeshAgent>();

        _stopDistance = Random.Range(_minStopDistance, _maxStopDistance);
    }


    private void Update()
    {
        HandleShooting();
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector3 playerPosition = PlayerMovement.Instance.GetPlayerPosition();

        transform.LookAt(playerPosition + new Vector3(0, _playerHeight, 0));

        float distance = Vector3.Distance(transform.position, playerPosition);

        if (distance < _stopDistance)
        {
            _navMeshAgent.isStopped = true;
            return;
        }

        if (_navMeshAgent.isStopped)
        {
            _navMeshAgent.isStopped = false;
        }

        _navMeshAgent.SetDestination(playerPosition);
    }

    void HandleShooting()
    {
        _fireTimer += Time.deltaTime;

        if (_fireTimer < _fireRate) return;

        _fireTimer = 0;

        Vector3 shootDir = PlayerMovement.Instance.GetPlayerPosition() - transform.position;

        Debug.DrawRay(_shootTransform.position, shootDir,Color.red,999);

        GameObject spawnedBullet = Instantiate(_bulletGameObject, _shootTransform.position, Quaternion.identity);
        spawnedBullet.GetComponent<EnemyBulllet>().Shoot(shootDir * _shootForce, _shootTransform.forward);
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
