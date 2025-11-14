using TMPro;
using UnityEngine;

public class UI
{
    private TMP_Text _wheatLabel;
    private TMP_Text _goldLabel;
    private TMP_Text _farmerLabel;
    private TMP_Text _warriorLabel;
    private TMP_Text _builderLabel;

    public void Init_UI(TMP_Text WheatLabel, TMP_Text GoldLabel, TMP_Text FarmerLabel, TMP_Text WarriorLabel, TMP_Text BuilderLabel)
    {
        _wheatLabel = WheatLabel;
        _goldLabel = GoldLabel;
        _farmerLabel = FarmerLabel;
        _warriorLabel = WarriorLabel;
        _builderLabel = BuilderLabel;
    }

    public void UI_UpdateTab(SettlementLogic SettlementLogic, SO_Unit so_Farmer, SO_Unit so_Warrior, SO_Unit so_Builder)
    {
        _wheatLabel.text = SettlementLogic.WheatCount.ToString();
        _goldLabel.text = SettlementLogic.GoldCount.ToString();
        _farmerLabel.text = SettlementLogic.FindUnitCount(so_Farmer).ToString();
        _warriorLabel.text = SettlementLogic.FindUnitCount(so_Warrior).ToString();
        _builderLabel.text = SettlementLogic.FindUnitCount(so_Builder).ToString();
    }
}
