using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/CardEffect/SelectCard", fileName = "SelectCard")]
public class SelectCard : CardEffect
{
    public CardSelectGroup cardSelect;
    public List<Card> cardList;

    public Vector2Int selectCard;
    public string cardName;

    public override string GetDescription(Vector2Int value, bool isFinal)
    {
        return GetValueString(value, isFinal) + "枚の" + cardName + "から" + GetValueString(selectCard, isFinal) + "枚を選び、手札に加える。";
    }

    public override void Execute(Vector2Int value, int power,CardDisplay cardDisplay)
    {
        CardSelectGroup group = Instantiate(cardSelect, BattleEventManager.Instance.transform);
        group.selectNumber = GetFinalValue(selectCard, power);

        int draw = GetFinalValue(value, power);
        for (int i = 0; i < draw; i++)
        {
            if (i >= cardList.Count) group.Add(cardList[Random.Range(0, cardList.Count)].battleData);
            else group.Add(cardList[i].battleData);
        }
    }
}