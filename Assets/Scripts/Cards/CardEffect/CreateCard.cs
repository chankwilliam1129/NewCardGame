using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/CardEffect/CreateCard", fileName = "CreateCard")]
public class CreateCard : CardEffect
{
    public CreateCardEvent createCardEvent;
    public Card card;

    public override string GetDescription(Vector2Int value, bool isFinal)
    {
        return card.name + "を" + GetValueString(value, isFinal) + "枚手札に加える。";
    }

    public override void Execute(Vector2Int value, int power,CardDisplay cardDisplay)
    {
        int draw = GetFinalValue(value, power);
        for (int i = 0; i < draw; i++)
        {
            Instantiate(createCardEvent, BattleEventManager.Instance.transform).card.data = card.battleData;
        }
    }
}