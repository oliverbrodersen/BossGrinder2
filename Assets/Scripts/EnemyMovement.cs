using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementPattern
{
    Seek,
    Wander,
}

public class EnemyMovement : MonoBehaviour
{
    public float Speed;
    public Rigidbody2D Rb;
    public SpriteRenderer Sr;

    public MovementPattern MovementPattern;

    private Vector2 wanderTarget = Vector2.zero;

    Vector2 movement;

    private Bounds mapBounds;
    private void Start()
    {
        mapBounds = GameObject.FindGameObjectWithTag("WaveController").GetComponent<SpriteRenderer>().bounds;
    }

    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        switch (MovementPattern)
        {
            case MovementPattern.Seek:
                {
                    var delta = player.transform.position - this.transform.position;
                    movement = new Vector2(delta.x, delta.y).normalized;
                }
                break;
            case MovementPattern.Wander:
                {
                    if (wanderTarget == Vector2.zero)
                        wanderTarget = RandomPointInBounds(mapBounds);

                    var position = new Vector2(this.transform.position.x, this.transform.position.y);
                    var delta = position - wanderTarget;
                    if (delta.magnitude < 2)
                    {
                        wanderTarget = RandomPointInBounds(mapBounds);
                    }

                    delta =  wanderTarget - position;
                    movement = delta.normalized;
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        Rb.MovePosition(Rb.position + movement * Speed * Time.fixedDeltaTime);

        // Make player face movement direction
        if (movement.x > 0)
        {
            Sr.flipX = false;
        }
        else if (movement.x < 0)
        {
            Sr.flipX = true;
        }
    }

    private Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
