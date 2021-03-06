using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectDamage : Condition
{
    private void Start()
    {
        character.conditionList.Add(this);
        character.characterEvent.OnTurnStart += OnTurnStart;
        character.characterEvent.OnGetDamage += OnGetDamage;
    }

    private void OnGetDamage(object sender, System.EventArgs e)
    {
        DamageEventArgs args = e as DamageEventArgs;
        if (args.damage >= 1 && args.from != null)
        {
            args.from.characterEvent.GetDamage(args.damage, null);
        }

        Settlement();
        text.text = stack.ToString();
        if (stack <= 0) Destroy(gameObject);
    }

    private void OnTurnStart(object sender, System.EventArgs e)
    {
        Settlement();
        text.text = stack.ToString();
        if (stack <= 0) Destroy(gameObject);
    }

    public void Settlement()
    {
        stack -= 1;
    }

    public override void DestoryEvent()
    {
        character.characterEvent.OnTurnStart -= OnTurnStart;
        character.characterEvent.OnGetDamage -= OnGetDamage;
    }

    public override Condition Exist(Character character)
    {
        Condition condition = null;
        foreach (var con in character.conditionList)
        {
            condition = con.GetComponent<ReflectDamage>();
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