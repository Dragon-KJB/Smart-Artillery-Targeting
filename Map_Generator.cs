using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int gridWidth = 30;
    public int gridHeight = 30;
    public int numEnemies = 10;
    public int numFriendlies = 10;
    public List<Vector2> enemyPositions = new List<Vector2>();
    public List<Vector2> friendlyPositions = new List<Vector2>();
    public GameObject tile;
    public GameObject enemy;
    public GameObject ally;
    public GameObject mortal;

    private Vector2 mortalPosition;

    void Start()
    {
        GenerateGrid();
        Visualize();
        PlaceMortar();
    }

    private void PlaceMortar()
    {
        // Calculate the center position of the grid
        mortalPosition = new Vector2(gridWidth / 2f, gridHeight / 2f);
        Vector3 centerPosition = new Vector3(mortalPosition.x, 9, mortalPosition.y);
        Instantiate(mortal, centerPosition, Quaternion.identity);
    }

    void GenerateGrid()
    {
        enemyPositions.Clear();
        friendlyPositions.Clear();

        Quaternion rotation = Quaternion.Euler(-90, 0, 0); // Define the rotation of -90 degrees on the Y-axis

        for (int i = 0; i < numEnemies; i++)
        {
            Vector2 pos = GetRandomPosition();
            Instantiate(enemy, new Vector3(pos.x, 1, pos.y), rotation);
            enemyPositions.Add(pos);
        }

        for (int i = 0; i < numFriendlies; i++)
        {
            Vector2 pos = GetRandomPosition();
            Instantiate(ally, new Vector3(pos.x, 1, pos.y), rotation);
            friendlyPositions.Add(pos);
        }
    }

    Vector2 GetRandomPosition()
    {
        Vector2 pos;
        do
        {
            pos = new Vector2(Random.Range(0, gridWidth), Random.Range(0, gridHeight));
        } while (pos == mortalPosition);
        return pos;
    }

    private void Visualize()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector3 position = new Vector3(x, 0, y);
                Instantiate(tile, position, Quaternion.identity);
            }
        }
    }
}
