using UnityEngine;

public class GameGrid : MonoBehaviour
{

    [SerializeField] private int numberOfRows = 3;
    [SerializeField] private int numberOfColumns = 11;

    private float minX = -2.5f;
    private float maxX = 2.5f;
    private float offset = 0.5f;
  

    public GameObject brickPrefab;

    void Start()
    {
        SpawnBricks();
    }

    private void SpawnBricks()
    {
        for (int row = 1; row < numberOfRows; row++)
        {
            for (int col  = 0; col < numberOfColumns; col++)
            {
              
                float x = minX + (offset * col);
                float y = offset * row + offset;

                GameObject brick = Instantiate(brickPrefab);
                brick.transform.position = new Vector2(x, y);

                brick.transform.SetParent(this.transform);
            }
        }
    }
}
