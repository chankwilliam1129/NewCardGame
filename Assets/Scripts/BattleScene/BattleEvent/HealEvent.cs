using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEvent : MonoBehaviour
{
    public int heal;
    public Character target;

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void OnDestroy()
    {
        target.characterEvent.HealHealth(heal);
    }
}