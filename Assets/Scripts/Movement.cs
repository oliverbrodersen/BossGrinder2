using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{

    public float speed = 5f;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Camera cam;

    public Animator animator;

    Vector2 movement;

    public Tilemap tilemap;

    public float DashSpeed;
    public float DashCooldown;
    public float DashDuration;
    private float DashEnd = 0;
    private float NextDash = 0;

    private float SpeedBoostEnd = 0;

    public GameObject DashIcon;

    public void GrantSpeedBoost()
    {
        SpeedBoostEnd = Time.time + 5;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (NextDash < Time.time)
        {
            DashIcon.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            if(Input.GetKeyDown(KeyCode.Space)){
                DashIcon.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
                FindObjectOfType<AudioManager>().Play("dash");
                DashEnd = Time.time + DashDuration;
                NextDash = Time.time + DashCooldown;
            }
        }

    }

    void FixedUpdate()
    {
        // Move player
        var movespeed = (Time.time > DashEnd) ? speed : DashSpeed;
        if (Time.time < SpeedBoostEnd)
            movespeed = speed + 3;

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
    }

}
