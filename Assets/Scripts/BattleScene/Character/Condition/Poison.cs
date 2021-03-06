using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Poison : Condition
{
    private void Start()
    {
        character.conditionList.Add(this);
        character.characterEvent.OnTurnStart += OnTurnStart;
    }

    private void OnTurnStart(object sender, System.EventArgs e)
    {
        Settlement();
        text.text = stack.ToString();
        if (stack <= 0) Destroy(gameObject);
    }

    public void Settlement()
    {
        float value = character.GetHealthPoint() * (100.0f / (stack + 100.0f));

        Instantiate(particleEffect, character.transform);

        character.SetHealthPoint((int)value);
        stack /= 2;
    }

    public override void DestoryEvent()
    {
        character.characterEvent.OnTurnStart -= OnTurnStart;
    }

    public override Condition Exist(Character character)
    {
        Condition condition = null;
        foreach (var con in character.conditionList)
        {
            condition = con.GetComponent<Poison>();
            if (condition != null) break;
        }
        return condition;
    }

    public override void Add(int value)
    {
        stack += value;
        text.text = stack.ToString();
    }
}