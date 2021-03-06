using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class EnemyActionDisplay : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public TextMeshProUGUI number;
    public TextMeshProUGUI text;

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Animator>().SetBool("isSelect", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Animator>().SetBool("isSelect", false);
    }

    public void Setup(EnemyAction enemyAction,Vector2Int value,int level)
    {
        text.text = enemyAction.GetDescription(value, level);
        number.text = enemyAction.GetValue(value, level).ToString();
        GetComponent<Image>().sprite = enemyAction.sprite;
    }

}
