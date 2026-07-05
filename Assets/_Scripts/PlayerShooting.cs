using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public static PlayerShooting Instance;

    [SerializeField] GameObject _bulletGameObject;
    [SerializeField] Transform _shootTransform;

    float _shootForce = 30;

    //reloading
    int _maximumBullets = 8;
    int _currentBullets;

    Health _playerHealth;

    public event EventHandler OnBulletShot;
    public event EventHandler OnEmptyMagShot;

    private void Awake()
    {
        Instance = this;
        _playerHealth = new Health(100);
    }


    private void Start()
    {
        GameInput.Instance.OnLeftMousePressed += Instance_OnLeftMousePressed;
        _currentBullets = _maximumBullets;
    }

    private void Instance_OnLeftMousePressed(object sender, System.EventArgs e)
    {
        if(_currentBullets < 0)
        {
            //empty mag
            OnEmptyMagShot?.Invoke(this, EventArgs.Empty);
            return;
        }

        GameObject spawnedBullet = Instantiate(_bulletGameObject,_shootTransform.position,Quaternion.identity);
        //spawnedBullet.GetComponent<Rigidbody>().AddForce(_shootTransform.forward * _shootForce, ForceMode.Impulse);
        spawnedBullet.GetComponent<PlayerBullet>().Shoot(_shootTransform.forward * _shootForce, _shootTransform.forward);
        OnBulletShot?.Invoke(this, EventArgs.Empty);
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
