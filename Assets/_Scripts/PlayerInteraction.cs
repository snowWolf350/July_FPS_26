using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance;

    HackArea _currentHackArea;

    public event EventHandler<OnHackAreaChangedEventArgs> OnHackAreaChanged;
    public class OnHackAreaChangedEventArgs : EventArgs
    {
        public HackArea passedHackArea;
    }

    private void Awake()
    {
        Instance = this;
    }
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

        SetHackArea(null);
    }

    public void SetHackArea(HackArea hackArea)
    {
        _currentHackArea = hackArea;

        OnHackAreaChanged?.Invoke(this, new OnHackAreaChangedEventArgs
        {
            passedHackArea = hackArea
        });
    }
}
