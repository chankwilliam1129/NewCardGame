using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/CardEffect/HealHealth", fileName = "HealHealth")]
public class HealHealth : CardEffect
{
    public HealEvent healEvent;

    public override string GetDescription(Vector2Int value, bool isFinal)
    {
        return "体力を" + GetValueString(value, isFinal) + "回復する。";
    }

    public override void Execute(Vector2Int value, int power,CardDisplay cardDisplay)
    {
        HealEvent e = Instantiate(healEvent, BattleEventManager.Instance.transform);
        e.heal = GetFinalValue(value, power);
        e.target = PlayerArea.Instance.player;
    }
}