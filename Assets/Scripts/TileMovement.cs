﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TileMovement : MonoBehaviour
{
    public Sprite finalTile; // The last piece to the puzzle

    private Transform tileToMove;
    private Transform invisTile;

    private float speed = 100.0f;
    public static int mixTimes = 30;

    Dictionary<int, string> dict = new Dictionary<int, string>();
    static Dictionary<Tuple<int, int>, Vector3> startPos = new Dictionary<Tuple<int, int>, Vector3>();
    int[,] Puzzle;

    // Start is called before the first frame update
    void Start()
    {
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
        print_puzzle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("scene3");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("scene0");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Tuple<bool, string>[] tiles = GetTiles();
            if (tiles[0].Item1 == true)
            {
                tileToMove = GameObject.Find(tiles[0].Item2).GetComponent<Transform>();
                MoveTile(tileToMove, invisTile);
                UpdatePuzzle("up");
                CheckDone();
            }
            else Debug.Log("No Valid up movement");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Tuple<bool, string>[] tiles = GetTiles();
            if (tiles[1].Item1 == true)
            {
                tileToMove = GameObject.Find(tiles[1].Item2).GetComponent<Transform>();
                MoveTile(tileToMove, invisTile);
                UpdatePuzzle("down");
                CheckDone();
            }
            else Debug.Log("No valid down movement");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Tuple<bool, string>[] tiles = GetTiles();
            if (tiles[2].Item1 == true)
            {
                tileToMove = GameObject.Find(tiles[2].Item2).GetComponent<Transform>();
                MoveTile(tileToMove, invisTile);
                UpdatePuzzle("left");
                CheckDone();
            }
            else Debug.Log("No valid left movement");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Tuple<bool, string>[] tiles = GetTiles();
            if (tiles[3].Item1 == true)
            {
                tileToMove = GameObject.Find(tiles[3].Item2).GetComponent<Transform>();
                MoveTile(tileToMove, invisTile);
                UpdatePuzzle("right");
                CheckDone();
            }
            else Debug.Log("No valid right movement");
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
        for (int i = 0; i < mixTimes; i++)
        {
            Tuple<bool, string>[] tiles = GetTiles();
            int number = UnityEngine.Random.Range(1, 5);
            if (number == 1 && tiles[0].Item1 == true) UpdatePuzzle("up");
            else if (number == 2 && tiles[1].Item1 == true) UpdatePuzzle("down");
            else if (number == 3 && tiles[2].Item1 == true) UpdatePuzzle("left");
            else if (number == 4 && tiles[3].Item1 == true) UpdatePuzzle("right");
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

    // Check to see if puzzle is finished
    private void CheckDone()
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
            // TODO: Implement element combination to make last piece of puzzle
            // For now spawn in last piece
            GameObject lastTile = GameObject.Find("Tile9");
            lastTile.GetComponent<Image>().sprite = finalTile;
            var tempColor = lastTile.GetComponent<Image>().color;
            tempColor.a = 1.0f;
            lastTile.GetComponent<Image>().color = tempColor;
            Debug.Log("Finished Puzzle");
            SceneManager.LoadScene("scene3");
        }
    }

    // Move tile on the screen
    private void MoveTile(Transform movingTile, Transform invTile)
    {
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
