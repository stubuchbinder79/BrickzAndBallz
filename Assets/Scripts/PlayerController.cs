using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerController : MonoBehaviour
{
    public static event Action OnNextShot = delegate { };
    public BallDisplay pitcher;
    public BallDisplay catcher;
    public BallTrajectory ballTrajectory;

    public GameObject ballPrefab;
    public Transform ballSpawn;

    public int maxBalls = 20;
    public float ballSpeed = 10f;
    public int ballsLanded = 0;
    private Ball[] balls;
    private float ballDelay = 0.1f;
    private float speed = 10f;
    private bool _ballsLaunched;

    private void Start()
    {
        catcher.numberOfBalls = 0;
        catcher.gameObject.SetActive(false);
        ballTrajectory.OnBallFire += BallTrajectory_OnBallFire;
        FloorTrigger.OnFloorTrigger += FloorTrigger_OnFloorTrigger;
        SpawnBalls();
    }

    private void OnDisable()
    {
        ballTrajectory.OnBallFire -= BallTrajectory_OnBallFire;
    }


    void BallTrajectory_OnBallFire(Vector3 pos)
    {
        if (_ballsLaunched)
            return;
        
        Vector2 p1 = new Vector2(pos.x, pos.y);
        Vector2 p2 = new Vector2(ballSpawn.position.x, ballSpawn.position.y);
        float angle = Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
        ballSpawn.Rotate(Vector3.forward, (angle + 90f) );
        StartCoroutine(LaunchBalls());
    }

    void FloorTrigger_OnFloorTrigger(Vector3 obj)
    {
        ballsLanded++;
        catcher.numberOfBalls = ballsLanded;

        if(ballsLanded == 1) {
            catcher.transform.position = new Vector2(obj.x, pitcher.transform.position.y);
            catcher.gameObject.SetActive(true);
            pitcher.gameObject.SetActive(false);
        } else if (ballsLanded == maxBalls)
            NextSnot();
    }

    private void NextSnot()
    {
        _ballsLaunched = false;
        Vector3 startPos = new Vector2(catcher.transform.position.x, 0);
        ballSpawn.transform.rotation = Quaternion.identity;

        pitcher.transform.position = startPos;
        ballSpawn.transform.position = startPos;
        ballTrajectory.startPos = startPos;

        pitcher.gameObject.SetActive(true);
        catcher.gameObject.SetActive(false);

        pitcher.numberOfBalls = maxBalls;
        catcher.numberOfBalls = 0;
        ballsLanded = 0;

        OnNextShot();

    }

    private void SpawnBalls()
    {
        pitcher.numberOfBalls = maxBalls;
        ballTrajectory.startPos = pitcher.transform.position;
        ballsLanded = 0;

        balls = new Ball[maxBalls];
        for (int i = 0; i < maxBalls; i++)
        {
            Ball ball = Instantiate(ballPrefab, ballSpawn.position, ballSpawn.rotation).GetComponent<Ball>();
            ball.transform.SetParent(ballSpawn);
            balls[i] = ball;
        }
    }

    private IEnumerator LaunchBalls()
    {
        _ballsLaunched = true;
        for (int i = 0; i < balls.Length; i++)
        {
            Ball ball = balls[i];
            ball.gameObject.SetActive(true);
            ball.Launch(ballSpeed);
            pitcher.numberOfBalls--;

            if(pitcher.numberOfBalls <= 0) {
                pitcher.gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(ballDelay);

        }

        yield return null;
    }

}

