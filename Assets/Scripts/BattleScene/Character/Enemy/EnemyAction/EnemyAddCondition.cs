using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy/EnemyAction/EnemyAddCondition", fileName = "EnemyAddCondition")]
public class EnemyAddCondition : EnemyAction
{

    public AddConditionEvent addCondition;
    public Condition condition;

    public bool toPlayer;

    public override string GetDescription(int value)
    {
        if(!toPlayer)
            return condition.GetText() + value + "を得る。";

        else
            return condition.GetText() + value + "を与える。";
    }
    public override void Execute(int value)
    {
        AddConditionEvent e = Instantiate(addCondition, BattleEventManager.Instance.transform);
        e.condition = condition;
        e.value = value;
        if (toPlayer) e.target = PlayerArea.Instance.player;
        else e.target = EnemyArea.Instance.enemy;
    }

}
