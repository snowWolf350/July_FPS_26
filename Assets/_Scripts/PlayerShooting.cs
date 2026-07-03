using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject _bulletGameObject;
    [SerializeField] Transform _shootTransform;

    float _shootForce = 30f;

    Health _playerHealth;

    private void Awake()
    {

        _playerHealth = new Health(100);
    }


    private void Start()
    {
        GameInput.Instance.OnLeftMousePressed += Instance_OnLeftMousePressed;
    }

    private void Instance_OnLeftMousePressed(object sender, System.EventArgs e)
    {
        GameObject spawnedBullet = Instantiate(_bulletGameObject,_shootTransform.position,Quaternion.identity);
        //spawnedBullet.GetComponent<Rigidbody>().AddForce(_shootTransform.forward * _shootForce, ForceMode.Impulse);
        spawnedBullet.GetComponent<Bullet>().Shoot(_shootTransform.forward * _shootForce, _shootTransform.forward);
    }
}
