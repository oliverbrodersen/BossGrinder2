using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public GameObject Projectile;
    public float ProjectileSpeed;
    public float AttackCooldown;
    private float nextAttack;

    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (nextAttack > Time.time)
            return;

        var delta = new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(this.transform.position.x, this.transform.position.y);
        delta.Normalize();
        var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;

        var projectile = Instantiate(Projectile, this.transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 90)));
        projectile.GetComponent<ProjectileCollide>().damage = 1;

        var rigidbody = projectile.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(delta * ProjectileSpeed, ForceMode2D.Impulse);

        nextAttack = Time.time + AttackCooldown;
    }
}
