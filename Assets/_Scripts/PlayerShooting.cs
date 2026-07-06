using System;
using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public static PlayerShooting Instance;

    [SerializeField] GameObject _bulletGameObject;
    [SerializeField] Transform _shootTransform;
    [SerializeField] LayerMask aimLayerMask = new LayerMask();

    float _shootForce = 50;

    //reloading
    int _maximumBullets = 8;
    int _currentBullets;

    Health _playerHealth;

    public event EventHandler OnAmmoChanged;
    public event EventHandler OnEmptyMagShot;

    Coroutine _reloadCoroutine;
    float ReloadTime = 2;

    Vector2 screenCentrePoint = new Vector2(Screen.width / 2, Screen.height / 2);
    Vector3 mouseWorldPosition = Vector3.zero;

    private void Awake()
    {
        Instance = this;
        _playerHealth = new Health(100);
    }


    private void Start()
    {
        GameInput.Instance.OnLeftMousePressed += GameInput_OnLeftMousePressed;
        GameInput.Instance.OnRPressed += GameInput_OnRPressed;
        _currentBullets = _maximumBullets;
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCentrePoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }
    }

    private void GameInput_OnRPressed(object sender, EventArgs e)
    {
        if (_currentBullets == _maximumBullets) return;

        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
        }

        _reloadCoroutine = StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
            yield return new WaitForSeconds(ReloadTime);

            _currentBullets = _maximumBullets;
            OnAmmoChanged?.Invoke(this, EventArgs.Empty);
            _reloadCoroutine = null;
    }

    private void GameInput_OnLeftMousePressed(object sender, System.EventArgs e)
    {
        if(_currentBullets < 0)
        {
            //empty mag
            OnEmptyMagShot?.Invoke(this, EventArgs.Empty);
            return;
        }
        Vector3 aimDir = (mouseWorldPosition - _shootTransform.position).normalized;
        GameObject spawnedBullet = Instantiate(_bulletGameObject, _shootTransform.position, Quaternion.LookRotation(aimDir, Vector3.up));

        //GameObject spawnedBullet = Instantiate(_bulletGameObject,_shootTransform.position,Quaternion.identity);
        //spawnedBullet.GetComponent<Rigidbody>().AddForce(_shootTransform.forward * _shootForce, ForceMode.Impulse);

        spawnedBullet.GetComponent<PlayerBullet>().Shoot(aimDir * _shootForce, _shootTransform.forward);
        OnAmmoChanged?.Invoke(this, EventArgs.Empty);
        _currentBullets--;
    }

    public int GetAmmoCount()
    {
        return _currentBullets;
    }
    public Health GetPlayerHealth()
    {
        return _playerHealth;
    }
}
