using TMPro;
using UnityEngine;

public class HealthPopUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    float _destroyWaitTime = 0.5f;

    private void Start()
    {
        Destroy(gameObject,_destroyWaitTime);
    }

    public void SetText(float damageAmount)
    {
        _text.text = damageAmount.ToString();
    }
}
