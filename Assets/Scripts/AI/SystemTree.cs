using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemTree : MonoBehaviour
{
    public GameObject floorPiece1;
    public GameObject floorPiece2;
    public GameObject floorPiece3;
    public GameObject floorPiece4;
    public GameObject floorPiece5;
    public GameObject floorPiece6;
    public GameObject floorPiece7;
    public Transform transformPiece1;
    public Transform transformPiece2;
    public Transform transformPiece3;
    public Transform transformPiece4;
    public Transform transformPiece5;
    public Transform transformPiece6;
    public Transform transformPiece7;

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
        public Sequence node_1B_REAL;
            public Selector node_1B;
                public Sequence node_1B_2A;
                    public CheckNode node_1B_2A_3A;
                    public CheckNode node_1B_2A_3B;
                    public CheckNode node_1B_2A_3C;
                public CheckNode node_1B_2B;
            public CheckNode node_1B_2D;
            public ActionNode node_1B_2C;
        public Sequence node_1C_REAL;
            public Selector node_1C;
                public Sequence node_1C_2A;
                    public CheckNode node_1C_2A_3A;
                    public CheckNode node_1C_2A_3B;
                    public CheckNode node_1C_2A_3C;
                    public CheckNode node_1C_2A_3D;
                public CheckNode node_1C_2B;
                public CheckNode node_1C_2C;
            public ActionNode node_1C_2D;
        public Sequence node_1D_REAL;
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
    void Awake()
    {
        print("start system");

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
        node_1B_2D = new CheckNode(CheckWalkingPuzzleScoreT2);

        List<Node> node_1B_children = new List<Node>();
        node_1B_children.Add(node_1B_2A);
        node_1B_children.Add(node_1B_2B);
        node_1B = new Selector(node_1B_children);

        List<Node> node_1B_REAL_children = new List<Node>();
        node_1B_REAL_children.Add(node_1B);
        node_1B_REAL_children.Add(node_1B_2D);
        node_1B_REAL_children.Add(node_1B_2C);

        node_1B_REAL = new Sequence(node_1B_REAL_children);


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
        node_1C = new Selector(node_1C_children);

        List<Node> node_1C_REAL_children = new List<Node>();
        node_1C_REAL_children.Add(node_1C);
        node_1C_REAL_children.Add(node_1C_2D);

        node_1C_REAL = new Sequence(node_1C_REAL_children);


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

        node_1D = new Selector(node_1D_children);

        List<Node> node_1D_REAL_children = new List<Node>();
        node_1D_REAL_children.Add(node_1D);
        node_1D_REAL_children.Add(node_1D_2F);

        node_1D_REAL = new Sequence(node_1D_REAL_children);


        // Fifth Subtree creation - node_1E (default)
        node_1E = new ActionNode(ChangeMovingPuzzleT5);

        // Create root node and attach its children
        List<Node> rootChildren = new List<Node>();
        rootChildren.Add(node_1A);
        rootChildren.Add(node_1B_REAL);
        rootChildren.Add(node_1C_REAL);
        rootChildren.Add(node_1D_REAL);
        rootChildren.Add(node_1E);

        rootNode = new Selector(rootChildren);
    }

    // Update is called once per frame (Not Used)
    void Update()
    {

    }

    // Call this function to run the behavior tree
    public void Execute() {
        AIDataManager.Print();
        rootNode.Evaluate();
    }
    
    public void FindFloorPiecesTranform()
    {
        floorPiece1 = GameObject.Find("FloorPiece1");
        floorPiece2 = GameObject.Find("FloorPiece2");
        floorPiece3 = GameObject.Find("FloorPiece3");
        floorPiece4 = GameObject.Find("FloorPiece4");
        floorPiece5 = GameObject.Find("FloorPiece5");
        floorPiece6 = GameObject.Find("FloorPiece6");
        floorPiece7 = GameObject.Find("End");

        transformPiece1 = floorPiece1.GetComponent<Transform>();
        transformPiece2 = floorPiece2.GetComponent<Transform>();
        transformPiece3 = floorPiece3.GetComponent<Transform>();
        transformPiece4 = floorPiece4.GetComponent<Transform>();
        transformPiece5 = floorPiece5.GetComponent<Transform>();
        transformPiece6 = floorPiece6.GetComponent<Transform>();
        transformPiece7 = floorPiece7.GetComponent<Transform>();
    }

    /* Hit a problem with delegate methods and not being able to pass in paramaters
     * Since every subtree has different thresholds for deciding things I just made
     * new methods that each tree will call.
     */
    // Delegate methods for subtree 1
    private NodeStates CheckNewSpellsScoreT1()
    {
        if (AIDataManager.SpellSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNonExistRecipeScoreT1()
    {
        if (AIDataManager.RecipeSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckItemPlacementScoreT1()
    {
        if (AIDataManager.ItemPlacementSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckMovingPuzzleScoreT1()
    {
        if (AIDataManager.MovingPuzzleSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckWalkingPuzzleScoreT1()
    {
        if (AIDataManager.WalkingPuzzleSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckTimeUseSpellScoreT1()
    {
        if (AIDataManager.TalismanSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates ChangeMovingPuzzleT1()
    {
        // TODO: Implement the changes for moving puzzle to change
        print("T1 EXE");
        SetWalkingPuzzle();
        transformPiece1.position = new Vector3(0f, 0.35f, -41.33f);
        Vector3 temp = transformPiece1.rotation.eulerAngles;
        temp.y = 0f;
        transformPiece1.rotation = Quaternion.Euler(temp);

        transformPiece2.position = new Vector3(0f, 0.34f, -36.72f);
        transformPiece2.rotation = Quaternion.Euler(temp);

        transformPiece3.position = new Vector3(0f, 0.341f, -31.45f);
        transformPiece3.rotation = Quaternion.Euler(temp);

        transformPiece4.position = new Vector3(0f, 0.34f, -28.14f);
        transformPiece4.rotation = Quaternion.Euler(temp);

        transformPiece5.position = new Vector3(0f, 0.35f, -20f);
        transformPiece5.rotation = Quaternion.Euler(temp);

        floorPiece6.SetActive(false);
        floorPiece7.SetActive(false);

        return NodeStates.SUCCESS;
    }

    // Delegate methods for subtree 2
    private NodeStates CheckTimeUseSpellScoreT2()
    {
        if (AIDataManager.TalismanSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNewSpellsScoreT2()
    {
        if (AIDataManager.SpellSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNonExistRecipeScoreT2()
    {
        if (AIDataManager.RecipeSmartness() > 0.8)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckItemPlacementScoreT2()
    {
        if (AIDataManager.ItemPlacementSmartness() > 0.7)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckWalkingPuzzleScoreT2()
    {
        if (AIDataManager.WalkingPuzzleSmartness() > 0.7)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates ChangeMovingPuzzleT2()
    {
        print("T2 EXE");
        SetWalkingPuzzle();
        transformPiece1.position = new Vector3(2.5f, 0.35f, -41.33f);
        Vector3 temp = transformPiece1.rotation.eulerAngles;
        temp.y = 48f;
        transformPiece1.rotation = Quaternion.Euler(temp);

        transformPiece2.position = new Vector3(6.53f, 0.34f, -37.72f);
        transformPiece2.rotation = Quaternion.Euler(temp);

        transformPiece3.position = new Vector3(10.2f, 0.35f, -34.45f);
        transformPiece3.rotation = Quaternion.Euler(temp);

        temp.y = -48f;

        transformPiece4.position = new Vector3(10.2f, 0.36f, -30.4f);
        transformPiece4.rotation = Quaternion.Euler(temp);

        transformPiece5.position = new Vector3(4.2f, 0.35f, -25f);
        transformPiece5.rotation = Quaternion.Euler(temp);

        transformPiece6.position = new Vector3(-2.1f, 0.33f, -19.35f);
        transformPiece6.rotation = Quaternion.Euler(temp);

        transformPiece7.position = new Vector3(-5.8f, 0.35f, -16f);
        transformPiece7.rotation = Quaternion.Euler(temp);
        transformPiece7.localScale = new Vector3(1.5f, 0.01f, 2.524f);

        return NodeStates.SUCCESS;
    }

    // Delegate methods for subtree 3
    private NodeStates CheckTimeUseSpellScoreT3()
    {
        if (AIDataManager.TalismanSmartness() > 0.6)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNewSpellsScoreT3()
    {
        if (AIDataManager.SpellSmartness() > 0.6)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNonExistRecipeScoreT3()
    {
        if (AIDataManager.RecipeSmartness() > 0.6)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckItemPlacementScoreT3()
    {
        if (AIDataManager.ItemPlacementSmartness() > 0.6)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckMovingPuzzleScoreT3()
    {
        if (AIDataManager.MovingPuzzleSmartness() > 0.6)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckWalkingPuzzleScoreT3()
    {
        if (AIDataManager.WalkingPuzzleSmartness() > 0.6)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates ChangeMovingPuzzleT3()
    {
        // TODO: Implement the changes for moving puzzle to change
        print("T3 EXE");
        SetWalkingPuzzle();
        transformPiece1.position = new Vector3(3f, 0.35f, -43.33f);
        Vector3 temp = transformPiece1.rotation.eulerAngles;
        temp.y = 90f;
        transformPiece1.rotation = Quaternion.Euler(temp);

        transformPiece2.position = new Vector3(6f, 0.34f, -40.72f);
        temp.y = 0f;
        transformPiece2.rotation = Quaternion.Euler(temp);

        transformPiece3.position = new Vector3(6f, 0.341f, -35.45f);
        transformPiece3.rotation = Quaternion.Euler(temp);

        temp.y = -40f;
        transformPiece4.position = new Vector3(3.8f, 0.36f, -30.5f);
        transformPiece4.rotation = Quaternion.Euler(temp);

        transformPiece5.position = new Vector3(-1.25f, 0.35f, -24.48f);
        transformPiece5.rotation = Quaternion.Euler(temp);

        transformPiece6.position = new Vector3(-7f, 0.33f, -17.63f);
        transformPiece6.rotation = Quaternion.Euler(temp);

        floorPiece7.SetActive(false);

        return NodeStates.SUCCESS;
    }


    // Delegate methods for subtree 4
    private NodeStates CheckTimeUseSpellScoreT4()
    {
        if (AIDataManager.TalismanSmartness() > 0.6)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNewSpellsScoreT4()
    {
        if (AIDataManager.SpellSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckNonExistRecipeScoreT4()
    {
        if (AIDataManager.RecipeSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckItemPlacementScoreT4()
    {
        if (AIDataManager.ItemPlacementSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckMovingPuzzleScoreT4()
    {
        if (AIDataManager.MovingPuzzleSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckWalkingPuzzleScoreT4()
    {
        if (AIDataManager.WalkingPuzzleSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckBreakWrongMirrorScoreT4()
    {
        if (AIDataManager.MirrorSmartness() > 0.5)
            return NodeStates.SUCCESS;
        else
            return NodeStates.FAILURE;
    }

    private NodeStates CheckOpenBreakDoorScoreT4()
    {
        // TODO: Implement Open/Break Door smartness checker in AIAIDataManager
        if (AIDataManager.gentlypassthedoor)
            return NodeStates.FAILURE;
        else 
            return NodeStates.SUCCESS;
    }

    private NodeStates ChangeMovingPuzzleT4()
    {
        // TODO: Implement the changes for moving puzzle to change
        print("T4 EXE");
        SetWalkingPuzzle();
        transformPiece1.position = new Vector3(0f, 0.35f, -41.33f);
        Vector3 temp = transformPiece1.rotation.eulerAngles;
        temp.y = 0f;
        transformPiece1.rotation = Quaternion.Euler(temp);

        transformPiece2.position = new Vector3(1.32f, 0.34f, -35.5f);
        temp.y = 30f;
        transformPiece2.rotation = Quaternion.Euler(temp);

        transformPiece3.position = new Vector3(-3.6f, 0.35f, -25f);
        temp.y = -40f;
        transformPiece3.rotation = Quaternion.Euler(temp);
        transformPiece3.localScale = new Vector3(1.5f, 0.01f, 20f);

        transformPiece4.position = new Vector3(2.96f, 0.36f, -29.7f);
        temp.y = 5f;
        transformPiece4.rotation = Quaternion.Euler(temp);

        transformPiece5.position = new Vector3(3.6f, 0.35f, -22.48f);
        transformPiece5.rotation = Quaternion.Euler(temp);

        transformPiece6.position = new Vector3(-2.1f, 0.33f, -19.35f);
        temp.y = 40f;
        transformPiece6.rotation = Quaternion.Euler(temp);
        transformPiece6.localScale = new Vector3(1.5f, 0.01f, 20f);

        transformPiece7.position = new Vector3(-9.85f, 0.35f, -15.8f);
        transformPiece7.localScale = new Vector3(1.5f, 0.01f, 4f);

        return NodeStates.SUCCESS;
    }


    // Delegate methods for subtree 5
    private NodeStates ChangeMovingPuzzleT5()
    {
        // TODO: Implement the changes for moving puzzle to change
        print("T5 EXE");
        SetWalkingPuzzle();
        transformPiece1.position = new Vector3(-3.7f, 0.35f, -42.3f);
        Vector3 temp = transformPiece1.rotation.eulerAngles;
        temp.y = 0f;
        transformPiece1.rotation = Quaternion.Euler(temp);

        transformPiece2.position = new Vector3(-1.8f, 0.34f, -44.8f);
        temp.y = 50f;
        transformPiece2.rotation = Quaternion.Euler(temp);

        transformPiece5.position = new Vector3(-3.28f, 0.35f, -34f);
        temp.y = 5f;
        transformPiece5.rotation = Quaternion.Euler(temp);

        transformPiece3.position = new Vector3(-9.6f, 0.33f, -25f);
        temp.y = -40f;
        transformPiece3.rotation = Quaternion.Euler(temp);
        transformPiece3.localScale = new Vector3(1.5f, 0.01f, 20f);

        transformPiece4.position = new Vector3(-2.54f, 0.34f, -25.5f);
        temp.y = 5f;
        transformPiece4.rotation = Quaternion.Euler(temp);

        transformPiece6.position = new Vector3(3f, 0.33f, -25.5f);
        temp.y = 40f;
        transformPiece6.rotation = Quaternion.Euler(temp);
        transformPiece6.localScale = new Vector3(1.5f, 0.01f, 20f);

        transformPiece7.position = new Vector3(7.9f, 0.35f, -15.2f);
        transformPiece7.rotation = Quaternion.Euler(temp);
        transformPiece7.localScale = new Vector3(1.5f, 0.01f, 4f);

        return NodeStates.SUCCESS;
    }

    private void SetWalkingPuzzle() {
        floorPiece6.SetActive(true);
        floorPiece7.SetActive(true);
    }
}
