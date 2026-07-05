using System;
using UnityEngine;
using UnityEngine.UI;

public class HackArea : MonoBehaviour
{
    [Serializable]
    public enum hackAreaState
    {
        idle,hacking,hacked
    }

    hackAreaState _currentHackAreaState = hackAreaState.idle;

    float _hackTimeMax = 60;
    float _hackTimer;

    [SerializeField] Image _hackProgress;
    [SerializeField] Transform _droidTransform;

    public event EventHandler OnHackStarted;
    public event EventHandler OnHackFinished;

    private void Update()
    {
        switch (_currentHackAreaState)
        {
            case hackAreaState.idle:
                break;
            case hackAreaState.hacking:
                _hackTimer += Time.deltaTime;
                _hackProgress.fillAmount = (1 - _hackTimer / _hackTimeMax);
                if( _hackTimer > _hackTimeMax )
                {
                    SetHackAreaState(hackAreaState.hacked);
                    OnHackFinished?.Invoke(this, EventArgs.Empty);
                }
                break;
            case hackAreaState.hacked:
                break;
        }
    }

    public void SetHackAreaState(hackAreaState hackAreaState)
    {
        _currentHackAreaState = hackAreaState;
    }

    public bool HackAreaStateIs(hackAreaState hackAreaState)
    {
        return _currentHackAreaState == hackAreaState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out PlayerInteraction playerInteraction))
        {
            playerInteraction.SetHackArea(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out PlayerInteraction playerInteraction))
        {
            playerInteraction.SetHackArea(null);
        }
    }

    public Transform GetDroidTransform()
    {
        return _droidTransform;
    }
}
