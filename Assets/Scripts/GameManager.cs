using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Timer _timer = new Timer();
    private SettlementLife _settlementLife = new SettlementLife();

    private int _wheatCount = 0;

    // LT - LastTime
    private float _LT_CollectetedWheat;

    private void Update()
    {
        _timer.RaiseSurvivedTimer();
        _settlementLife.Update();
    }

    private void TimerAction()
    {
        
    }


}
