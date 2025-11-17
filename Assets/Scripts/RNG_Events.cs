using UnityEngine;

public class RNG_Events
{
    private float _multBase_wheatCollect = 1f;
    private float _mult_wheatCollectBad = 0.5f;
    private float _mult_wheatCollectGood = 1.5f;

    public float MultBase_WheatCollect => _multBase_wheatCollect;
    public float Mult_WheatCollect => _mult_wheatCollectGood;

    public void HumanWillBornAgain(SettlementLogic settlement)
    {
        //settlement.addUnit
    }
    public void Plague(SettlementLogic settlement)
    {
        //settlement.KillUnit()
    }

    public void HarvestSeason(SettlementLogic settlement)
    {
        settlement.ChangeWheatMultiplier(_mult_wheatCollectGood);
    }

    public void DroughtSeason(SettlementLogic settlement)
    {
        settlement.ChangeWheatMultiplier(_mult_wheatCollectBad);
    }

    public void ResetSeason(SettlementLogic settlement)
    {
        settlement.ChangeWheatMultiplier(_multBase_wheatCollect);
    }
}
