using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
  

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private TMP_Text numberOfBallsText;

    public GameObject ballPrefab;
    [SerializeField] private Ball[] balls;

    [SerializeField] private int _numberOfBalls = 1;

    private float spawnDelay = 0.1f;
    private bool _ballsInPlay = false;

    public int numberOfBalls
    {
        set
        {
            _numberOfBalls = value;
            numberOfBallsText.text = "x" + value.ToString();
        }
        get
        {
            return _numberOfBalls;
        }
    }


    private void Awake()
    {
        numberOfBallsText = (numberOfBallsText != null) ? numberOfBallsText : GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        numberOfBalls = _numberOfBalls;
        SpawnBalls();
    }


    private void SpawnBalls()
    {
        // reset balls
        foreach (Ball ball in balls)
        {
            Destroy(ball.gameObject);
        }

        balls = new Ball[numberOfBalls];

        for (int i = 0; i < numberOfBalls; i++)
        {
            Ball ball = Instantiate(ballPrefab).GetComponent<Ball>();
            balls[i] = ball;
        }
    }



    IEnumerator LaunchBalls()
    {
        _ballsInPlay = true;
        
        for (int i = 0; i < balls.Length; i++)
        {
            Ball ball = balls[i];
            //ball.Launch();
            numberOfBalls--;
            yield return new WaitForSeconds(spawnDelay);
        }
    }


}
