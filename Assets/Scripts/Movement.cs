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

    public float DashSpeed;
    public float DashCooldown;
    public float DashDuration;
    private float DashEnd = 0;
    private float NextDash = 0;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        mousePosition = cam.ScreenToViewportPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space) && NextDash < Time.time)
        {
            DashEnd = Time.time + DashDuration;
            NextDash = Time.time + DashCooldown;
        }

    }

    void FixedUpdate()
    {
        // Move player
        var movespeed = (Time.time > DashEnd) ? speed : DashSpeed;
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);

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
    }

}
