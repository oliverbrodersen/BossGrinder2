using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    public float speed;
    public float deadzoneOuter;
    public float deadzoneInner;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Transform playerOrigin;

    private Vector2 movement;

    void Update()
    {
        var playerPosition = new Vector2(playerOrigin.position.x, playerOrigin.position.y);

        // Follow the player if outside the deadzone
        var delta = playerPosition - rb.position;
        movement = (delta.magnitude > deadzoneOuter && delta.magnitude > deadzoneInner) ? delta.normalized : Vector2.zero;


    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // Make dragon face movement direction
        if (movement.x > 0)
        {
            sr.flipX = false;
        }
        else if (movement.x < 0)
        {
            sr.flipX = true;
        }
    }
}
