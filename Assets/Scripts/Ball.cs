using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;
    private readonly Vector2 LAUNCH_VELOCITY = new Vector2(0f, 10f);

    private bool isMoving;
    private float _ballBounce = 0.03f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isMoving = false;
        rb.isKinematic = true;
        rb.drag = 0;
        rb.angularDrag = 0;
        rb.gravityScale = 0;        // turn off gravity
    }


    public void Land()
    {
        isMoving = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
    }

    public void Bounce()
    {
        rb.AddForce(rb.velocity.normalized * _ballBounce, ForceMode2D.Impulse);
    }
    internal void Launch(float ballSpeed)
    {
        rb.isKinematic = false;
        isMoving = true;
        rb.AddForce(transform.up *ballSpeed);
    }
}
