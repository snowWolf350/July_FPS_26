using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    HackArea _currentHackArea;

    private void Start()
    {
        GameInput.Instance.OnEPressed += GameInput_OnEPressed;
    }

    private void OnDestroy()
    {
        GameInput.Instance.OnEPressed -= GameInput_OnEPressed;
    }

    private void GameInput_OnEPressed(object sender, System.EventArgs e)
    {
        if (_currentHackArea == null) return;

        if (_currentHackArea.HackAreaStateIs(HackArea.hackAreaState.idle) == false) return;

        _currentHackArea.SetHackAreaState(HackArea.hackAreaState.hacking);
    }

    public void SetHackArea(HackArea hackArea)
    {
        _currentHackArea = hackArea;
    }
}
