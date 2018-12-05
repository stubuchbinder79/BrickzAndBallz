using System;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public static event Action<Vector3> OnFloorTrigger = delegate { };
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogFormat("FloorTrigger.OnTriggerEnter2D: {0}", collision.tag);
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if(ball != null) {
            OnFloorTrigger(collision.gameObject.transform.position);
            ball.Land();
        }
    }
}
