using UnityEngine;

public class Border : MonoBehaviour
{
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball") {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            ball.Bounce();
        }
    }
}
