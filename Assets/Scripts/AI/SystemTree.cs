using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemTree : MonoBehaviour
{
    private AIDataManager dataManager;
    public Transform floorPiece1;
    public Transform floorPiece2;
    public Transform floorPiece3;
    public Transform floorPiece4;
    public Transform floorPiece5;
    public Transform floorPiece6;

   /* Behavior Tree hierarchy
    * Node's number and letter refer to the path taken down the tree to get to them
    * The number infront of the letter refers to the depth of the behavior tree
    * and the letter refers to the order of the node (A->B->C)
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
            public ActionNode node_1A_2G;
        public Selector node_1B;
            public Sequence node_1B_2A;
                public CheckNode node_1B_2A_3A;
                public CheckNode node_1B_2A_3B;
                public CheckNode node_1B_2A_3C;
            public CheckNode node_1B_2B;
            public ActionNode node_1B_2C;
        public Selector node_1C;
            public Sequence node_1C_2A;
                public CheckNode node_1C_2A_3A;
                public CheckNode node_1C_2A_3B;
                public CheckNode node_1C_2A_3C;
                public CheckNode node_1C_2A_3D;
            public CheckNode node_1C_2B;
            public CheckNode node_1C_2C;
            public ActionNode node_1C_2D;
        public Selector node_1D;
            public Sequence node_1D_2A;
                public CheckNode node_1D_2A_3A;
                public CheckNode node_1D_2A_3B;
                public CheckNode node_1D_2A_3C;
                public CheckNode node_1D_2A_3D;
            public CheckNode node_1D_2B;
            public CheckNode node_1D_2C;
            public CheckNode node_1D_2D;
            public CheckNode node_1D_2E;
            public ActionNode node_1D_2F;
        public ActionNode node_1E;

    // Start is called before the first frame update
    void Start()
    {
        dataManager = GameObject.Find("TrigramManager").GetComponent<AIDataManager>();
        FindFloorPiecesTranform();

        /* First Subtree creation - noda_1A
         * Create and add child nodes to a list for Sequence node_1A
         * Have to build from bottom up, so child nodes are created first
         * then later attached to parent nodes
         */
        node_1A_2G = new ActionNode(ChangeMovingPuzzleT1);
        node_1A_2F = new CheckNode(CheckWalkingPuzzleScoreT1);
        node_1A_2E = new CheckNode(CheckMovingPuzzleScoreT1);
        node_1A_2D = new CheckNode(CheckItemPlacementScoreT1);
        node_1A_2C = new CheckNode(CheckNonExistRecipeScoreT1);
        node_1A_2B = new CheckNode(CheckNewSpellsScoreT1);
        node_1A_2A = new CheckNode(CheckTimeUseSpellScoreT1);

        List<Node> node_1A_children = new List<Node>();
        node_1A_children.Add(node_1A_2A);
        node_1A_children.Add(node_1A_2B);
        node_1A_children.Add(node_1A_2C);
        node_1A_children.Add(node_1A_2D);
        node_1A_children.Add(node_1A_2E);
        node_1A_children.Add(node_1A_2F);
        node_1A_children.Add(node_1A_2G);

        // Create node_1A and attach its children
        node_1A = new Sequence(node_1A_children);


        // Second Subtree creation - node_1B
        node_1B_2A_3A = new CheckNode(CheckTimeUseSpellScoreT2);
        node_1B_2A_3B = new CheckNode(CheckNewSpellsScoreT2);
        node_1B_2A_3C = new CheckNode(CheckNonExistRecipeScoreT2);

        List<Node> node_1B_2A_children = new List<Node>();
        node_1B_2A_children.Add(node_1B_2A_3A);
        node_1B_2A_children.Add(node_1B_2A_3B);
        node_1B_2A_children.Add(node_1B_2A_3C);

        node_1B_2A = new Sequence(node_1B_2A_children);
        node_1B_2B = new CheckNode(CheckItemPlacementScoreT2);
        node_1B_2C = new ActionNode(ChangeMovingPuzzleT2);

        List<Node> node_1B_children = new List<Node>();
        node_1B_children.Add(node_1B_2A);
        node_1B_children.Add(node_1B_2B);

        node_1B = new Selector(node_1B_children);


        // Third Subtree creation - node_1C
        node_1C_2A_3A = new CheckNode(CheckTimeUseSpellScoreT3);
        node_1C_2A_3B = new CheckNode(CheckNewSpellsScoreT3);
        node_1C_2A_3C = new CheckNode(CheckNonExistRecipeScoreT3);
        node_1C_2A_3D = new CheckNode(CheckItemPlacementScoreT3);

        List<Node> node_1C_2A_children = new List<Node>();
        node_1C_2A_children.Add(node_1C_2A_3A);
        node_1C_2A_children.Add(node_1C_2A_3B);
        node_1C_2A_children.Add(node_1C_2A_3C);
        node_1C_2A_children.Add(node_1C_2A_3D);

        node_1C_2A = new Sequence(node_1C_2A_children);
        node_1C_2B = new CheckNode(CheckMovingPuzzleScoreT3);
        node_1C_2C = new CheckNode(CheckWalkingPuzzleScoreT3);
        node_1C_2D = new ActionNode(ChangeMovingPuzzleT3);

        List<Node> node_1C_children = new List<Node>();
        node_1C_children.Add(node_1C_2A);
        node_1C_children.Add(node_1C_2B);
        node_1C_children.Add(node_1C_2C);
        node_1C_children.Add(node_1C_2D);

        node_1C = new Selector(node_1C_children);


        // Fourth Subtree creation - node_1D
        node_1D_2A_3A = new CheckNode(CheckTimeUseSpellScoreT4);
        node_1D_2A_3B = new CheckNode(CheckNewSpellsScoreT4);
        node_1D_2A_3C = new CheckNode(CheckNonExistRecipeScoreT4);
        node_1D_2A_3D = new CheckNode(CheckItemPlacementScoreT4);

        List<Node> node_1D_2A_children = new List<Node>();
        node_1D_2A_children.Add(node_1D_2A_3A);
        node_1D_2A_children.Add(node_1D_2A_3B);
        node_1D_2A_children.Add(node_1D_2A_3C);
        node_1D_2A_children.Add(node_1D_2A_3D);

        node_1D_2A = new Sequence(node_1D_2A_children);
        node_1D_2B = new CheckNode(CheckMovingPuzzleScoreT4);
        node_1D_2C = new CheckNode(CheckWalkingPuzzleScoreT4);
        node_1D_2D = new CheckNode(CheckBreakWrongMirrorScoreT4);
        node_1D_2E = new CheckNode(CheckOpenBreakDoorScoreT4);
        node_1D_2F = new ActionNode(ChangeMovingPuzzleT4);

        List<Node> node_1D_children = new List<Node>();
        node_1D_children.Add(node_1D_2A);
        node_1D_children.Add(node_1D_2B);
        node_1D_children.Add(node_1D_2C);
        node_1D_children.Add(node_1D_2D);
        node_1D_children.Add(node_1D_2E);
        node_1D_children.Add(node_1D_2F);

        node_1D = new Selector(node_1D_children);


        // Fifth Subtree creation - node_1E (default)
        node_1E = new ActionNode(ChangeMovingPuzzleT5);

        // Create root node and attach its children
        List<Node> rootChildren = new List<Node>();
        rootChildren.Add(node_1A);
        rootChildren.Add(node_1B);
        rootChildren.Add(node_1C);
        rootChildren.Add(node_1D);
        rootChildren.Add(node_1E);

        rootNode = new Selector(rootChildren);
    }

    // Update is called once per frame (Not Used)
    void Update()
    {

    }

    void FindFloorPiecesTranform()
    {
        floorPiece1 = GameObject.Find("FloorPiece1").GetComponent<Transform>();
        floorPiece2 = GameObject.Find("FloorPiece2").GetComponent<Transform>();
        floorPiece3 = GameObject.Find("FloorPiece3").GetComponent<Transform>();
        floorPiece4 = GameObject.Find("FloorPiece4").GetComponent<Transform>();
        floorPiece5 = GameObject.Find("FloorPiece5").GetComponent<Transform>();
        floorPiece6 = GameObject.Find("FloorPiece6").GetComponent<Transform>();
    }

    /* Hit a problem with delegate methods and not being able to pass in paramaters
     * Since every subtree has different thresholds for deciding things I just made
     * new methods that each tree will call.
     */
    // Delegate methods for subtree 1
    private NodeStates CheckNewSpellsScoreT1()
    {
        if (dataManager.SpellSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNonExistRecipeScoreT1()
    {
        if (dataManager.RecipeSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckItemPlacementScoreT1()
    {
        if (dataManager.ItemPlacementSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckMovingPuzzleScoreT1()
    {
        if (dataManager.MovingPuzzleSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckWalkingPuzzleScoreT1()
    {
        if (dataManager.WalkingPuzzleSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckTimeUseSpellScoreT1()
    {
        if (dataManager.TalismanSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates ChangeMovingPuzzleT1()
    {
        // floorPiece1.position = hitInfo.point + new Vector3(0.0f, 0.1f, 0);
        Vector3 temp = floorPiece1.rotation.eulerAngles;
        temp.x = 45f;
        floorPiece1.rotation = Quaternion.Euler(temp);
        return NodeStates.SUCCESS;
    }


    // Delegate methods for subtree 2
    private NodeStates CheckTimeUseSpellScoreT2()
    {
        if (dataManager.TalismanSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNewSpellsScoreT2()
    {
        if (dataManager.SpellSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNonExistRecipeScoreT2()
    {
        if (dataManager.RecipeSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckItemPlacementScoreT2()
    {
        if (dataManager.ItemPlacementSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates ChangeMovingPuzzleT2()
    {
        // TODO: Implement the changes for moving puzzle to change
        return NodeStates.FAILURE;
    }


    // Delegate methods for subtree 3
    private NodeStates CheckTimeUseSpellScoreT3()
    {
        if (dataManager.TalismanSmartness() > 0.7)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNewSpellsScoreT3()
    {
        if (dataManager.SpellSmartness() > 0.7)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNonExistRecipeScoreT3()
    {
        if (dataManager.RecipeSmartness() > 0.7)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckItemPlacementScoreT3()
    {
        if (dataManager.ItemPlacementSmartness() > 0.7)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckMovingPuzzleScoreT3()
    {
        if (dataManager.MovingPuzzleSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckWalkingPuzzleScoreT3()
    {
        if (dataManager.WalkingPuzzleSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates ChangeMovingPuzzleT3()
    {
        // TODO: Implement the changes for moving puzzle to change
        return NodeStates.FAILURE;
    }


    // Delegate methods for subtree 4
    private NodeStates CheckTimeUseSpellScoreT4()
    {
        if (dataManager.TalismanSmartness() > 0.6)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNewSpellsScoreT4()
    {
        if (dataManager.SpellSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNonExistRecipeScoreT4()
    {
        if (dataManager.RecipeSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckItemPlacementScoreT4()
    {
        if (dataManager.ItemPlacementSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckMovingPuzzleScoreT4()
    {
        if (dataManager.MovingPuzzleSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckWalkingPuzzleScoreT4()
    {
        if (dataManager.WalkingPuzzleSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckBreakWrongMirrorScoreT4()
    {
        if (dataManager.MirrorSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckOpenBreakDoorScoreT4()
    {
        // TODO: Implement Open/Break Door smartness checker in AIDataManager
        return NodeStates.FAILURE;
    }

    private NodeStates ChangeMovingPuzzleT4()
    {
        // TODO: Implement the changes for moving puzzle to change
        return NodeStates.FAILURE;
    }


    // Delegate methods for subtree 5
    private NodeStates ChangeMovingPuzzleT5()
    {
        // TODO: Implement the changes for moving puzzle to change
        return NodeStates.FAILURE;
    }

}
