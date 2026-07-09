using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Health _enemyHealth;

    //shooting 
    [SerializeField] Transform _shootTransform;
    [SerializeField] GameObject _bulletGameObject;
    [SerializeField] GameObject _DamageVisual;

    Coroutine _damageCoroutine;

    float _fireRate = 3;
    float _fireTimer;
    float _shootForce = 20;
    float _playerHeight = 0.5f;
    float _minStopDistance = 7;
    float _maxStopDistance = 14;
    float _stopDistance;
    float _damageFlashTime = 0.15f;
    float _pathUpdateTimer;

    NavMeshAgent _navMeshAgent;

    Vector3 _offset;

    private void Awake()
    {
        _enemyHealth = new Health(100);
    }

    private void Start()
    {
        _enemyHealth.OnDamange += _enemyHealth_OnDamange;
        _enemyHealth.onDeath += _enemyHealth_onDeath;

        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.avoidancePriority = Random.Range(0, 99);

        _stopDistance = Random.Range(_minStopDistance, _maxStopDistance);

        Vector2 rand = Random.insideUnitCircle * 2f;
        _offset = new Vector3(rand.x, 0, rand.y);
    }


    private void Update()
    {
        HandleShooting();
        HandleMovement();
    }

    void HandleMovement()
    {

        _pathUpdateTimer += Time.deltaTime;
   

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

        if (_pathUpdateTimer < 0.25f) return;

        _pathUpdateTimer = 0;
        Vector3 dir = (transform.position - playerPosition).normalized;

        Vector3 targetPos = playerPosition + dir * _stopDistance;

        _navMeshAgent.SetDestination(targetPos + _offset);
    }

    void HandleShooting()
    {
        _fireTimer += Time.deltaTime;

        if (_fireTimer < _fireRate) return;

        _fireTimer = 0;

        Vector3 shootDir = PlayerMovement.Instance.GetPlayerPosition() - transform.position;

        GameObject spawnedBullet = Instantiate(_bulletGameObject, _shootTransform.position, Quaternion.LookRotation(shootDir));
        spawnedBullet.GetComponent<EnemyBulllet>().Shoot(shootDir * _shootForce);
    }

    private void OnDestroy()
    {
        _enemyHealth.OnDamange -= _enemyHealth_OnDamange;
        _enemyHealth.onDeath -= _enemyHealth_onDeath;
    }

    private void _enemyHealth_onDeath(object sender, System.EventArgs e)
    {
        EnemySpawner.Instance.AddEnemyCount(-1);        
        Destroy(gameObject);
    }

    private void _enemyHealth_OnDamange(object sender, System.EventArgs e)
    {
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
        }

        _damageCoroutine = StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash()
    {
        _DamageVisual.SetActive(true);

        yield return new WaitForSeconds(_damageFlashTime);

        _DamageVisual.SetActive(false);
        _damageCoroutine = null;
    }

    public Health GetEnemyHealth()
    {
        return _enemyHealth;
    }
}
