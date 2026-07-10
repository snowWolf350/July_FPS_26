using System.Collections;
using Unity.Burst;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Health _enemyHealth;

    //shooting 
    [SerializeField] Transform _shootTransform;
    [SerializeField] GameObject _bulletGameObject;
    [SerializeField] GameObject _DamageVisual;
    [SerializeField] GameObject _healthPopUI;

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
    float _offsetMax = 1.5f;
    float _offsetMin = 0.5f;

    NavMeshAgent _navMeshAgent;

    Vector3 _MovementOffset;
    Vector3 _shootOffset;

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
        _MovementOffset = new Vector3(rand.x, 0, rand.y);
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

        _navMeshAgent.SetDestination(targetPos + _MovementOffset);
    }

    void HandleShooting()
    {
        _fireTimer += Time.deltaTime;

        if (_fireTimer < _fireRate) return;

        _fireTimer = 0;

        Vector3 playerDir = PlayerMovement.Instance.GetPlayerPosition() - transform.position;

        Vector3 shootDir;

        float randomX;
        float randomY;
        float randomZ;
        if (PlayerMovement.Instance.PlayerIsMoving())
        {

            randomX = Random.Range(-_offsetMax, _offsetMax);
            randomY = Random.Range(-_offsetMax, _offsetMax);
            randomZ = Random.Range(-_offsetMax, _offsetMax);


        }
        else
        {
                
            randomX = Random.Range(-_offsetMin, _offsetMin);
            randomY = Random.Range(-_offsetMin, _offsetMin);
            randomZ = Random.Range(-_offsetMin, _offsetMin);
        }

        shootDir = playerDir + new Vector3(randomX, randomY, randomZ);

        GameObject spawnedBullet = Instantiate(_bulletGameObject, _shootTransform.position, Quaternion.LookRotation(playerDir));

        spawnedBullet.GetComponent<EnemyBulllet>().Shoot(playerDir * _shootForce);
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

    private void _enemyHealth_OnDamange(object sender, Health.OnDamageEventArgs e)
    {
        GameObject healthPopUi = Instantiate(_healthPopUI,transform.position,transform.rotation,transform);
        healthPopUi.transform.localPosition += new Vector3(0, 2.5f, 0);
        healthPopUi.GetComponent<HealthPopUI>().SetText(e.damage);

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
