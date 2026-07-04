
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] Image _healthBar;
    [SerializeField] Enemy _baseEnemy;

    private void Start()
    {
        _baseEnemy = transform.parent.GetComponent<Enemy>();
        _baseEnemy.GetEnemyHealth().OnDamange += EnemyHealthUI_OnDamange;
    }

    private void EnemyHealthUI_OnDamange(object sender, System.EventArgs e)
    {
        _healthBar.fillAmount = _baseEnemy.GetEnemyHealth().GetHealthNormalized();
    }
}