using System.Collections.Generic;
using UnityEngine;

public class Gameboard : MonoBehaviour
{

    public float startY = 5f;
    public float maxX = 2f;
    public float minX = 2f;

    public float dX = 0.5f;
    public float dY = 0.5f;

    public int numberOfRows = 1;
    public int bricksPerRow = 9;
    public GameObject brickPrefab;

    private List<GameObject> bricks = new List<GameObject>();

    private void Start()
    {
        SpawnBricks();

        PlayerController.OnNextShot += PlayerController_OnNextShot;
    }

    private void OnDestroy()
    {
        PlayerController.OnNextShot -= PlayerController_OnNextShot;
    }

    private void SpawnBricks()
    {

        bricks.Clear();
        for (int row = 0; row < numberOfRows; row++)
        {
            for (int b = 0; b < bricksPerRow; b++)
            {
                GameObject go = Instantiate(brickPrefab, new Vector3((b * dX) - maxX, startY + (dY * row), 0), Quaternion.identity);
                bricks.Add(go);
            }
        }
    }

    private void DropBricks()
    {
  
    }
    void PlayerController_OnNextShot()
    {
        DropBricks();
    }

}
