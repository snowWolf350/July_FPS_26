using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _ammoText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerShooting.Instance.OnAmmoChanged += Instance_OnBulletShot;
        PlayerShooting.Instance.OnEmptyMagShot += Instance_OnEmptyMagShot;
    }

    private void OnDestroy()
    {
        PlayerShooting.Instance.OnAmmoChanged -= Instance_OnBulletShot;
        PlayerShooting.Instance.OnEmptyMagShot -= Instance_OnEmptyMagShot;
    }

    private void Instance_OnEmptyMagShot(object sender, System.EventArgs e)
    {

    }

    private void Instance_OnBulletShot(object sender, System.EventArgs e)
    {
        _ammoText.text = PlayerShooting.Instance.GetAmmoCount().ToString() + "/8";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
