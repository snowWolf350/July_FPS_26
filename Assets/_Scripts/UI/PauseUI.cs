using UnityEngine;

public class PauseUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnGameStateChanged += Instance_OnGameStateChanged;
        Hide();
    }

    private void Instance_OnGameStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.GameIsPaused())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        gameObject.SetActive(true);
    }
    void Hide()
    {
        gameObject.SetActive(false);
    }
}
