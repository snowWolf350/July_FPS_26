using System;
using UnityEngine;

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

    private void Update()
    {
        switch (_currentHackAreaState)
        {
            case hackAreaState.idle:
                break;
            case hackAreaState.hacking:
                _hackTimer += Time.deltaTime;

                if( _hackTimer > _hackTimeMax )
                {
                    SetHackAreaState(hackAreaState.hacked);
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

}
