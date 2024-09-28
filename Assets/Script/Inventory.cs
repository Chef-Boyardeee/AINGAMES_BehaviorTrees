using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<string> items = new List<string>();

    private void Awake()
    {
        ActionNode actionNode = new(CheckInventoryForMeat);

    }
    public bool ContainsItem(string id)
    {
        return (items.Contains(id));
    }

    public NodeState CheckInventoryForMeat()
    {
        if (items.Contains("Meat"))
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }

    public NodeState CheckInventoryForVegetable()
    {
        if (items.Contains("Vegetable"))
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }

    public NodeState CheckInventoryForFruit()
    {
        if (items.Contains("Fruit"))
            return NodeState.SUCCESS;
        else
            return NodeState.FAILURE;
    }

    public void AddItems(string item)
    {
        items.Add(item);
    }
}
