using UnityEngine;

public class HackUI : MonoBehaviour
{

    private void Start()
    {
        PlayerInteraction.Instance.OnHackAreaChanged += PlayerInteraction_OnHackAreaChanged;
        Hide();
    }

    private void PlayerInteraction_OnHackAreaChanged(object sender, PlayerInteraction.OnHackAreaChangedEventArgs e)
    {
        if (e.passedHackArea != null && e.passedHackArea.HackAreaStateIs(HackArea.hackAreaState.idle))
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
