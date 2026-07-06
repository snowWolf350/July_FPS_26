using System;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance;

    HackArea _currentHackArea;

    public event EventHandler<HackAreaEventArgs> OnHackAreaChanged;
    public class HackAreaEventArgs : EventArgs
    {
        public HackArea passedHackArea;
    }

    public event EventHandler<HackAreaEventArgs> OnDroneDeployed;

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
        if (GameManager.Instance.GameIsPlaying() == false) return;
        if (_currentHackArea == null) return;

        if (_currentHackArea.HackAreaStateIs(HackArea.hackAreaState.idle) == false) return;

        _currentHackArea.SetHackAreaState(HackArea.hackAreaState.hacking);
        OnDroneDeployed?.Invoke(this,new HackAreaEventArgs
        {
            passedHackArea = _currentHackArea,
        });

        SetHackArea(null);
    }

    public void SetHackArea(HackArea hackArea)
    {
        _currentHackArea = hackArea;

        OnHackAreaChanged?.Invoke(this, new HackAreaEventArgs
        {
            passedHackArea = hackArea
        });
    }
}
