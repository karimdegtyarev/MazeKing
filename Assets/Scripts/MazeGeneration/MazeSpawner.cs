using System;
using UnityEngine;
using UnityEngine.UI;


public class Furthest
{
    public int X;
    public int Y;
}

public class MazeSpawner : MonoBehaviour
{
    public static int score;

    public GameObject CellPrefab;
    public GameObject FinishFlag;

    private void Start()
    {
        score = PlayerPrefs.GetInt("Score");

        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze();
        
        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                if (x == MazeGenerator.exit.X && y == MazeGenerator.exit.Y)
                {
                    Instantiate(FinishFlag, new Vector2(x, y), Quaternion.identity).transform.position = new Vector3(x - 1.8f, y - 3.5f, - 1); ;
                }
                // Instantiate(FinishFlag, new Vector2(x - 2, y - 4), Quaternion.identity).transform.position = new Vector3(x - 1.8f, y - 2.5f, -1);
                Cell c = Instantiate(CellPrefab, new Vector2(x - 2, y - 4), Quaternion.identity).GetComponent<Cell>();

                c.WallLeft.SetActive(maze[x, y].WallLeft);
                c.WallBottom.SetActive(maze[x, y].WallBottom);

            }
        }

    }
}



 
