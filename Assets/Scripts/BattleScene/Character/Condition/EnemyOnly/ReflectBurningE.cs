using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectBurningE : Condition
{
    public int damage;
    public Burning burning;
    public int burning_stack;

    private void Start()
    {
        character.conditionList.Add(this);
        //character.characterEvent.OnTurnEnd += OnTurnStart;
        EnemyArea.Instance.enemy.characterEvent.OnTurnStart += OnTurnStart;
        character.characterEvent.OnGetDamaged += OnGetDamaged;
        character.characterEvent.OnBlockDamage += OnBlockDamage;
    }

    private void OnGetDamaged(object sender, System.EventArgs e)
    {
        DamageEventArgs args = e as DamageEventArgs;
        if (args.from != null && args.damage >= damage)
        {
            Condition con = burning.Exist(args.from);
            if (con == null)
            {
                con = Instantiate(burning, args.from.conditionDisplay);
                con.character = args.from;
            }
            con.Add(burning_stack);
        }

        Settlement();
        text.text = stack.ToString();
        if (stack <= 0) Destroy(gameObject);
    }

    private void OnBlockDamage(object sender, System.EventArgs e)
    {
        DamageEventArgs args = e as DamageEventArgs;
        if (args.from != null && character.GetShield() != 0)
        {
            Condition con = burning.Exist(args.from);
            if (con == null)
            {
                con = Instantiate(burning, args.from.conditionDisplay);
                con.character = args.from;
            }
            con.Add(burning_stack);
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
        EnemyArea.Instance.enemy.characterEvent.OnTurnStart -= OnTurnStart;
        character.characterEvent.OnGetDamaged -= OnGetDamaged;
        character.characterEvent.OnBlockDamage -= OnBlockDamage;

    }

    public override Condition Exist(Character character)
    {
        Condition condition = null;
        foreach (var con in character.conditionList)
        {
            condition = con.GetComponent<ReflectBurningE>();
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
