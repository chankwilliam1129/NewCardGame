using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public NodeData data;
    public Vector2Int location;

    private void Start()
    {
        GetComponent<Image>().sprite = data.sprite;
        transform.localPosition = new Vector3(location.x * 100, location.y * 100, 0);
    }
    public Node SetNodeData(NodeData nodeData, Vector2Int lo)
    {
        data = nodeData;
        location = lo;
        return this;
    } 
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Animator>().SetBool("isTouch", true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Animator>().SetBool("isTouch", false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {    
        if (CanWalk()) 
        {
            Node curNode = MapManager.Instance.GetNodeList(MapData.Instance.playerLocation.y)[MapData.Instance.playerLocation.x];
            curNode.GetComponent<Animator>().SetBool("isSelect", false);
            curNode.GetComponent<Animator>().SetBool("isPass", false);
            curNode.GetComponent<Animator>().SetBool("isSelected", true);
 
            MapData.Instance.playerLocation = location;
            MapData.Instance.selectedNode.Add(location);

           GetComponent<Animator>().SetBool("isSelect", true);
            foreach (var n in MapManager.Instance.GetNodeList(MapData.Instance.playerLocation.y))
            {
                if (n != this) n.GetComponent<Animator>().SetBool("isPass", true);
            }

            if (data.nodeType == NodeType.Store) 
            {
                SceneManager.LoadScene("StoreScene");
            }
        }
    }
    private bool CanWalk()
    {
        return IsNextLocation();
    }
    private bool IsNextLocation()
    {
        return location.y == MapData.Instance.playerLocation.y + 1;
    }
    private bool IsUpLocation()
    {
        return location.y > MapData.Instance.playerLocation.y; 
    } 
    private bool IsUnderLocation()
    {
        return location.y < MapData.Instance.playerLocation.y || (location.y == MapData.Instance.playerLocation.y && location.x != MapData.Instance.playerLocation.x);
    }
    private bool IsSameLocation()
    {
        return location == MapData.Instance.playerLocation;
    }

    public void StateCheck()
    {
        if (IsUnderLocation()) GetComponent<Animator>().SetBool("isPass", true);
        if (IsSameLocation())
        {
            GetComponent<Animator>().SetBool("isTouch", true);
            GetComponent<Animator>().SetBool("isSelect", true);
            GetComponent<Animator>().SetBool("isPass", false);
        }
        else if (MapData.Instance.selectedNode.FindIndex(n => n == location) != -1)
        {
            GetComponent<Animator>().SetBool("isPass", false);
            GetComponent<Animator>().SetBool("isSelected", true);
        }
    }
}
