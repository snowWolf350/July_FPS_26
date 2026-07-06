using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource _audioSource;

    [SerializeField] AudioClip _gunShotAudio;
    [SerializeField] AudioClip _emptyGunAudio;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        PlayerShooting.Instance.OnEmptyMagShot += Instance_OnEmptyMagShot;
        PlayerShooting.Instance.OnBulletShot += Instance_OnBulletShot;
    }

    private void Instance_OnBulletShot(object sender, System.EventArgs e)
    {
        _audioSource.PlayOneShot(_gunShotAudio);
    }

    private void Instance_OnEmptyMagShot(object sender, System.EventArgs e)
    {
        _audioSource.PlayOneShot(_emptyGunAudio);
    }
}
