using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{

    public float speed = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Camera cam;

    public Animator animator;

    Vector2 movement;
    Vector2 mousePosition;

    public Tilemap tilemap;
    bool generate = false;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        mousePosition = cam.ScreenToViewportPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.G))
        {
            generate = true;
        }

    }

    void FixedUpdate()
    {
        // Move player
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        // Make player face movement direction
        if (movement.x > 0)
        {
            sr.flipX = false;
        } 
        else if (movement.x < 0)
        {
            sr.flipX = true;
        }

        // Set animation parameters
        animator.SetFloat("Speed", movement.SqrMagnitude());

        // Calculate aim direction
        var delta = mousePosition - rb.position;
        var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;

        if (generate)
        {
            generate = false;

        }

    }

}
