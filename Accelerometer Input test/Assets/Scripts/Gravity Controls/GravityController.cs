using System;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    // private void Update()
    // {
    //     if (Input.GetKey(KeyCode.W)) // Gravity direction up
    //     {
    //         Physics2D.gravity = new Vector2(0, 9.8f);
    //     }
    //     if (Input.GetKey(KeyCode.S)) // Gravity direction down
    //     {
    //         Physics2D.gravity = new Vector2(0, -9.8f);
    //     }
    //     if (Input.GetKey(KeyCode.A)) // Gravity direction left
    //     {
    //         Physics2D.gravity = new Vector2(-9.8f, 0);
    //     }
    //     if (Input.GetKey(KeyCode.D)) // Gravity direction right
    //     {
    //         Physics2D.gravity = new Vector2(9.8f, 0);
    //     }
    // }

    private void FixedUpdate()
    {
        var xValue = Input.acceleration.x;
        var yValue = Input.acceleration.y;

        Physics2D.gravity = new Vector2(9.8f * xValue, 9.8f * yValue);
    }
}
