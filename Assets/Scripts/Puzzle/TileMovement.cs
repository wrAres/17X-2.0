using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TileMovement : MonoBehaviour
{
    //Scale: 1,1,1 | Position: 4.6, -4.5, -1
    public Sprite finalTile; // The last piece to the puzzle
    //Scale: 0.235, 0.233, 1 | Position 4.67, -4.625, -1
    public Animation glowingTile; // Tile to indicate last piece

    public Animator animator;
	public GameObject loadingScreen;
    private Transform tileToMove;
    private Transform invisTile;
    private GameObject eightTiles;
    private GameObject fullPicture; 

    private float speed = 100.0f;
    public int mixTimes = 30;

    private float start;
    private bool finished = false;

    Dictionary<int, string> dict = new Dictionary<int, string>();
    static Dictionary<Tuple<int, int>, Vector3> startPos = new Dictionary<Tuple<int, int>, Vector3>();
    int[,] Puzzle;
    Stack movements; // Stores the moves to solve the puzzle

    // Start is called before the first frame update
    void Start()
    {
        // Set up componants for TrigramManager
        GameObject manager = GameObject.Find("TrigramManager");
        //Find the full 8 tiles object
        fullPicture = GameObject.Find("Full_Picture");  // TODO uncommment after adding in full_picture
        fullPicture.active = false;
        eightTiles = GameObject.Find("Full_8_Tiles");
        eightTiles.active = false;
		loadingScreen.GetComponent<Renderer>().enabled = false;
        movements = new Stack();

        // Debug.Log("Data Manager object name: " + manager.name);

        start = Time.time;

        // Set StartPos dictionary
        int limit = 1;
        for (int j = 0; j < 3; j++)
        {
            for (int k = 0; k < 3; k++)
            {
                Vector3 tilePos = GameObject.Find("Tile" + limit++).GetComponent<Transform>().position;
                Tuple<int, int> tup = new Tuple<int, int>(j, k);
                startPos.Add(tup, tilePos);
            }
        }

        invisTile = GameObject.Find("Tile9").GetComponent<Transform>();

        // Set Dictionary to map Puzzle to screen
        for (int i = 1; i <= 9; i++)
        {
            dict.Add(i, "Tile" + i);
        }

        Puzzle = StartPuzzle();
        MixPuzzle();

        CheckSolveable();

        TipsDialog.PrintDialog("Moving Puzzle Tip");
        EarthSoundManager.StopPlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
			GameObject.Find("MainUI").GetComponent<Show>().ToggleLock(true);
            loadingScreen.GetComponent<Renderer>().enabled = true;
            SceneManager.LoadScene("scene3");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Tuple<bool, string>[] tiles = GetTiles();
            if (tiles[0].Item1 == true && !finished)
            {
                tileToMove = GameObject.Find(tiles[0].Item2).GetComponent<Transform>();
                MoveTile(tileToMove, invisTile);
                UpdatePuzzle("up");
                AIDataManager.movingPuzzleMoves++;
                CombineTiles();
                //CheckDone();
            }
            // else Debug.Log("No Valid up movement");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Tuple<bool, string>[] tiles = GetTiles();
            if (tiles[1].Item1 == true && !finished)
            {
                tileToMove = GameObject.Find(tiles[1].Item2).GetComponent<Transform>();
                MoveTile(tileToMove, invisTile);
                UpdatePuzzle("down");
                AIDataManager.movingPuzzleMoves++;
                CombineTiles();
                //CheckDone();
            }
            // else Debug.Log("No valid down movement");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Tuple<bool, string>[] tiles = GetTiles();
            if (tiles[2].Item1 == true && !finished)
            {
                tileToMove = GameObject.Find(tiles[2].Item2).GetComponent<Transform>();
                MoveTile(tileToMove, invisTile);
                UpdatePuzzle("left");
                AIDataManager.movingPuzzleMoves++;
                CombineTiles();
                //CheckDone();
            }
            // else Debug.Log("No valid left movement");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            Tuple<bool, string>[] tiles = GetTiles();
            if (tiles[3].Item1 == true && !finished)
            {
                tileToMove = GameObject.Find(tiles[3].Item2).GetComponent<Transform>();
                MoveTile(tileToMove, invisTile);
                UpdatePuzzle("right");
                AIDataManager.movingPuzzleMoves++;
                CombineTiles();
                //CheckDone();
            }
            // else Debug.Log("No valid right movement");
        }
    }

    private int[,] StartPuzzle()
    {
        // Create the completed puzzle
        int[,] puzzle = new int[3, 3];
        int num = 1;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                puzzle[i, j] = num++;
            }
        }
        return puzzle;
    }

    private void MixPuzzle()
    {
        // Mix the completed puzzle
        int numMix = 0;
        string lastMove = "";
        while (numMix < mixTimes)
        {
            Tuple<bool, string>[] tiles = GetTiles();
            int number = UnityEngine.Random.Range(1, 5);
            if (number == 1 && tiles[0].Item1 == true && lastMove != "up")
            {
                lastMove = "up";
                movements.Push("down");
                UpdatePuzzle("up");
                numMix++;
            }
            else if (number == 2 && tiles[1].Item1 == true && lastMove != "down")
            {
                lastMove = "down";
                movements.Push("up");
                UpdatePuzzle("down");
                numMix++;
            }
            else if (number == 3 && tiles[2].Item1 == true && lastMove != "left")
            {
                lastMove = "left";
                movements.Push("right");
                UpdatePuzzle("left");
                numMix++;
            }
            else if (number == 4 && tiles[3].Item1 == true && lastMove != "right")
            {
                lastMove = "right";
                movements.Push("left");
                UpdatePuzzle("right");
                numMix++;
            }
        }
        // Update the tiles in 3D space
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GameObject tileToChange = GameObject.Find("Tile" + Puzzle[i, j]);
                Tuple<int, int> location = new Tuple<int, int>(i, j);
                tileToChange.GetComponent<Transform>().position = startPos[location];
            }
        }
    }

    private Tuple<bool, string>[] GetTiles()
    {
        Tuple<bool, string> up = new Tuple<bool, string>(false, "noTile");
        Tuple<bool, string> down = new Tuple<bool, string>(false, "noTile");
        Tuple<bool, string> left = new Tuple<bool, string>(false, "noTile");
        Tuple<bool, string> right = new Tuple<bool, string>(false, "noTile");
        // Find the empty tile
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Puzzle[i, j] == 9)
                {
                    // Check if what moves are possible
                    if ((i + 1) <= 2)
                    {
                        up = new Tuple<bool, string>(true, dict[Puzzle[i + 1, j]]);
                    }
                    if ((i - 1) >= 0)
                    {
                        down = new Tuple<bool, string>(true, dict[Puzzle[i - 1, j]]);
                    }
                    if ((j + 1) <= 2)
                    {
                        left = new Tuple<bool, string>(true, dict[Puzzle[i, j + 1]]);
                    }
                    if ((j - 1) >= 0)
                    {
                        right = new Tuple<bool, string>(true, dict[Puzzle[i, j - 1]]);
                    }
                }
            }
        }
        // Create Tuple array and return
        Tuple<bool, string>[] arr = new Tuple<bool, string>[4];
        arr[0] = up;
        arr[1] = down;
        arr[2] = left;
        arr[3] = right;
        return arr;
    }

    private void CombineTiles()
    {
        int num = 1;
        bool flag = true;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Puzzle[i, j] != num++) flag = false;
            }
        }
        if (flag)
        {
            eightTiles.active = true;
            finished = true;

            //Make 9th tile visable
            GameObject lastTile = GameObject.Find("Tile9");
            Transform lastTileTransform = lastTile.GetComponent<Transform>();
            lastTileTransform.localScale = new Vector3(0.235f, 0.233f, 1.0f);
            lastTileTransform.position = new Vector3(4.67f, -4.625f, -1.0f);

            //lastTile.GetComponent<SpriteRenderer>().sprite = finalTile;
            var tempColor = lastTile.GetComponent<SpriteRenderer>().color;
            tempColor.a = 1.0f;
            lastTile.GetComponent<SpriteRenderer>().color = tempColor;

            animator.SetBool("Missing9th", true);

            Debug.Log("Did the glow thing");
        }
    }

    // Check to see if puzzle is finished
    public void CheckDone()
    {
        int num = 1;
        bool flag = true;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Puzzle[i, j] != num++) flag = false;
            }
        }
        if (flag)
        {
            fullPicture.active = true;
            // TODO: Implement element combination to make last piece of puzzle
            // For now spawn in last piece
            animator.SetBool("Missing9th", false);
            animator.SetBool("FullPicDone", true);

            GameObject lastTile = GameObject.Find("Tile9");
            /*Transform lastTileTransform = lastTile.GetComponent<Transform>();
            lastTileTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            lastTileTransform.position = new Vector3(4.6f, -4.5f, -1.0f);
            lastTile.GetComponent<SpriteRenderer>().sprite = finalTile;*/
            var tempColor = lastTile.GetComponent<SpriteRenderer>().color;
            tempColor.a = 0.0f;
            lastTile.GetComponent<SpriteRenderer>().color = tempColor;
            // Debug.Log("Finished Puzzle");
            // Debug.Log("Number of moves: " + AIDataManager.movingPuzzleMoves);

            float totalTime = Time.time - start;
            // Debug.Log("Time: " + totalTime);
            AIDataManager.movingPuzzleTime = totalTime;
			GameObject.Find("MainUI").GetComponent<Show>().ToggleLock(true);
            loadingScreen.GetComponent<Renderer>().enabled = true;
            SceneManager.LoadScene("scene3");
        }
    }

    // Move tile on the screen
    private void MoveTile(Transform movingTile, Transform invTile)
    {
        //AI moving puzzle move tile
        Vector3 Start = movingTile.position;
        Vector3 Destination = invisTile.position;

        while (Vector3.Distance(movingTile.position, Destination) > 0)
        {
            movingTile.position = Vector3.MoveTowards(movingTile.position, Destination, Time.deltaTime * speed);
        }
        while (Vector3.Distance(invTile.position, Start) > 0)
        {
            invTile.position = Vector3.MoveTowards(invTile.position, Start, Time.deltaTime * speed);
        }
    }

    // Update puzzle map
    private void UpdatePuzzle(string move)
    {
        // Need this here to not move multiple times on a press for "up" and "left"
        bool movedAlready = false;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Puzzle[i, j] == 9)
                {
                    if (move == "up" && movedAlready == false)
                    {
                        int temp = Puzzle[i, j];
                        Puzzle[i, j] = Puzzle[i + 1, j];
                        Puzzle[i + 1, j] = temp;
                        movedAlready = true;
                    }
                    else if (move == "down")
                    {
                        int temp = Puzzle[i, j];
                        Puzzle[i, j] = Puzzle[i - 1, j];
                        Puzzle[i - 1, j] = temp;
                    }
                    else if (move == "left" && movedAlready == false)
                    {
                        int temp = Puzzle[i, j];
                        Puzzle[i, j] = Puzzle[i, j + 1];
                        Puzzle[i, j + 1] = temp;
                        movedAlready = true;
                    }
                    else if (move == "right")
                    {
                        int temp = Puzzle[i, j];
                        Puzzle[i, j] = Puzzle[i, j - 1];
                        Puzzle[i, j - 1] = temp;
                    }
                }
            }
        }
    }


    // Debug methods delete later...

    private void CheckSolveable()
    {
        // Get current board
        // Loop: till stack is empty
        // Pop movement off stack
        // Change board
        // Check if board is in starting position

        int[,] CheckPuzzle = StartPuzzle();
        int[,] StartPosition = StartPuzzle();
        bool solution = true;
        string moveSet = "";

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                CheckPuzzle[i, j] = Puzzle[i, j];
            }
        }

        while (movements.Count != 0)
        {
            string move = (string) movements.Pop();
            moveSet += move + ", ";
            applyMove(CheckPuzzle, move);
        }

        // Check if board is in starting positon
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (CheckPuzzle[i, j] != StartPosition[i, j]) solution = false;
            }
        }

        Debug.Log(moveSet);

        // if (solution) Debug.Log("Solvable");
        // else Debug.Log("No solution");
    }

    private void applyMove(int[,] p, string move)
    {
        // Need this here to not move multiple times on a press for "up" and "left"
        bool movedAlready = false;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (p[i, j] == 9)
                {
                    if (move == "up" && movedAlready == false)
                    {
                        int temp = p[i, j];
                        p[i, j] = p[i + 1, j];
                        p[i + 1, j] = temp;
                        movedAlready = true;
                    }
                    else if (move == "down")
                    {
                        int temp = p[i, j];
                        p[i, j] = p[i - 1, j];
                        p[i - 1, j] = temp;
                    }
                    else if (move == "left" && movedAlready == false)
                    {
                        int temp = p[i, j];
                        p[i, j] = p[i, j + 1];
                        p[i, j + 1] = temp;
                        movedAlready = true;
                    }
                    else if (move == "right")
                    {
                        int temp = p[i, j];
                        p[i, j] = p[i, j - 1];
                        p[i, j - 1] = temp;
                    }
                }
            }
        }
    }

    private void print_puzzle()
    {
        string toPrint = "";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                toPrint = toPrint + Puzzle[i, j] + ", ";
            }
            Debug.Log(toPrint);
            toPrint = "\n\t    ";
        }
    }
}
