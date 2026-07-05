using UnityEngine;

public class Droid : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    HackArea _currentHackArea;
    private void Start()
    {
        PlayerInteraction.Instance.OnDroneDeployed += PlayerInteraction_OnDroneDeployed; ;
    }

    private void OnDestroy()
    {
        PlayerInteraction.Instance.OnDroneDeployed -= PlayerInteraction_OnDroneDeployed;
    }

    private void PlayerInteraction_OnDroneDeployed(object sender, PlayerInteraction.HackAreaEventArgs e)
    {
        StartHackAt(e.passedHackArea);
    }

    public void StartHackAt(HackArea hackArea)
    {
        _currentHackArea = hackArea;
        transform.parent = hackArea.GetDroidTransform();
        hackArea.OnHackFinished += HackArea_OnHackFinished;
    }

    private void HackArea_OnHackFinished(object sender, System.EventArgs e)
    {
        Debug.Log("Hack finished");
        transform.parent = _playerTransform;
        _currentHackArea.OnHackFinished -= HackArea_OnHackFinished;
        _currentHackArea = null;
    }
}
