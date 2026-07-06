using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _ammoText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerShooting.Instance.OnBulletShot += Instance_OnBulletShot;
        PlayerShooting.Instance.OnGunReloaded += Instance_OnGunReloaded;
        PlayerShooting.Instance.OnEmptyMagShot += Instance_OnEmptyMagShot;
    }



    private void OnDestroy()
    {
        PlayerShooting.Instance.OnBulletShot -= Instance_OnBulletShot;
        PlayerShooting.Instance.OnEmptyMagShot -= Instance_OnEmptyMagShot;
        PlayerShooting.Instance.OnGunReloaded -= Instance_OnGunReloaded;
    }

    private void Instance_OnEmptyMagShot(object sender, System.EventArgs e)
    {

    }

    private void Instance_OnBulletShot(object sender, System.EventArgs e)
    {
        _ammoText.text = PlayerShooting.Instance.GetAmmoCount().ToString() + "/8";
    }
    private void Instance_OnGunReloaded(object sender, System.EventArgs e)
    {
        _ammoText.text = PlayerShooting.Instance.GetAmmoCount().ToString() + "/8";
    }
}