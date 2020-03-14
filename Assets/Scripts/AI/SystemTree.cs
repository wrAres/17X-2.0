using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemTree : MonoBehaviour
{

    private AIDataManager dataManager;


   /* Behavior Tree hierarchy
    * Node's number and letter refer to the path taken down the tree to get to them
    * The number infront of the letter refers to the depth of the behavior tree
    * Ex: From the root node the first child would be "node_1A" and the frist child of "node_1A" is "node_1A_2A"
    */
    public Selector rootNode;
        public Sequence node_1A;
            public CheckNode node_1A_2A;
            public CheckNode node_1A_2B;
            public CheckNode node_1A_2C;
            public CheckNode node_1A_2D;
            public CheckNode node_1A_2E;
            public CheckNode node_1A_2F;
    // public Selector node_1B;

    // Start is called before the first frame update
    void Start()
    {
        dataManager = GameObject.Find("TrigramManager").GetComponent<AIDataManager>();

        // Create and add child nodes to a list for Sequence node_1A
        node_1A_2F = new CheckNode(CheckWalkingPuzzleScore);
        node_1A_2E = new CheckNode(CheckMovingPuzzleScore);
        node_1A_2D = new CheckNode(CheckItemPlacementScore);
        node_1A_2C = new CheckNode(CheckNonExistRecipeScore);
        node_1A_2B = new CheckNode(CheckNewSpellsScore);
        node_1A_2A = new CheckNode(CheckTimeUseSpellScore);

        List<Node> node_1A_children = new List<Node>();
        node_1A_children.Add(node_1A_2A);
        node_1A_children.Add(node_1A_2B);
        node_1A_children.Add(node_1A_2C);
        node_1A_children.Add(node_1A_2D);
        node_1A_children.Add(node_1A_2E);
        node_1A_children.Add(node_1A_2F);

        // Create node_1A and attach its children
        node_1A = new Sequence(node_1A_children);

        List<Node> rootChildren = new List<Node>();
        rootChildren.Add(node_1A);

        rootNode = new Selector(rootChildren);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private NodeStates CheckNewSpellsScore()
    {
        if (dataManager.SpellSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNonExistRecipeScore()
    {
        if (dataManager.RecipeSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckItemPlacementScore()
    {
        if (dataManager.ItemPlacementSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckMovingPuzzleScore()
    {
        if (dataManager.MovingPuzzleSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckWalkingPuzzleScore()
    {
        if (dataManager.WalkingPuzzleSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckTimeUseSpellScore()
    {
        if (dataManager.TalismanSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }
}
