using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Timer _timer = new Timer();
    private SettlementLogic _settlement = new SettlementLogic();
    
    private bool _isWalking = false;

    [SerializeField] private Vector3 _safeRange;
    [SerializeField] private Vector3 _dangerousRange;

    private void FixedUpdate()
    {
        _timer.RaiseSurvivedTime();
        _settlement.HereWeGo(_isWalking);
    }


}
