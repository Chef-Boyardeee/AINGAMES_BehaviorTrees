using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFoodBT : MonoBehaviour
{
    // Step 1: Declare references to other scripts
    private Hunger hunger;
    private Inventory inventory;
    private Awareness awareness;


    // Step 2: Declare all the nodes. The root node is a Sequence
    private Sequence rootNode;

    private ActionNode an_checkHunger;

    private Selector selector_checkInventory;
    private ActionNode an_checkMeat;
    private ActionNode an_checkVegetable;
    private ActionNode an_checkFruit;

    private Inverter inverter_checkEnemies;
    private ActionNode an_checkEnemies;

    private ActionNode an_eatFood;

    private void Awake()
    {
        // Step 3: Get Components of all action-related nodes
        hunger = GetComponent<Hunger>();
        inventory = GetComponent<Inventory>();
        awareness = GetComponent<Awareness>();
        //inventory.AddItems("Meat");
    }

    private void Start()
    {
        // Step 4: Make the behavior tree. Use the constructors to
        // build up instances each node. It is better to build up
        // the tree from the children nodes all the way up to the
        // parent nodes

        an_checkHunger = new ActionNode(CheckHunger);
        an_checkMeat = new ActionNode(CheckMeat);
        an_checkVegetable = new ActionNode(CheckVegetable);
        an_checkFruit = new ActionNode(CheckFruit);
        an_checkEnemies = new ActionNode(CheckEnemies);
        an_eatFood = new ActionNode(EatFood);

        List<Node> selectorNodes = new();
        selectorNodes.Add(an_checkMeat);
        selectorNodes.Add(an_checkVegetable);
        selectorNodes.Add(an_checkFruit);
        selector_checkInventory = new Selector(selectorNodes);

        inverter_checkEnemies = new Inverter(an_checkEnemies);

        List<Node> sequenceNodes = new();
        sequenceNodes.Add(an_checkHunger);
        sequenceNodes.Add(selector_checkInventory);
        sequenceNodes.Add(inverter_checkEnemies);
        sequenceNodes.Add(an_eatFood);
        rootNode = new Sequence(sequenceNodes);
        // Step 5: Store all nodes as children of the root node
    }

    private void Update()
    {
        rootNode.Evaluate();
        // Step 6: Simply call the Evaluate function of the root node
    }

    // You can declare all action node functions here
    // Example only:

    private NodeState CheckHunger()
    {
        Debug.Log("CheckHunger passed");
        return hunger.IsHungry() ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    private NodeState CheckMeat()
    {
        Debug.Log("CheckMeat passed");
        return inventory.CheckInventoryForMeat();
    }

    private NodeState CheckVegetable()
    {
        Debug.Log("CheckVegetable passed");
        return inventory.CheckInventoryForMeat();
    }

    private NodeState CheckFruit()
    {
        Debug.Log("CheckFruit passed");
        return inventory.CheckInventoryForMeat();
    }

    private NodeState CheckEnemies()
    {
        Debug.Log("CheckEnemies passed");
        return awareness.IsEnemyAround() ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    private NodeState EatFood()
    {
        Debug.Log("EatFood passed");
        hunger.SetHunger(hunger.GetMaxHunger());
        return NodeState.SUCCESS;
    }
}
