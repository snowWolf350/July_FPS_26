using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] Image _healthBar;

    private void Start()
    {
        PlayerShooting.Instance.GetPlayerHealth().OnDamange += PlayerHealthUI_OnDamange;
    }

    private void PlayerHealthUI_OnDamange(object sender, System.EventArgs e)
    {
        _healthBar.fillAmount = PlayerShooting.Instance.GetPlayerHealth().GetHealthNormalized();
        _healthText.text = (PlayerShooting.Instance.GetPlayerHealth().GetHealthNormalized() * 100).ToString() + "/100";
    }
}
