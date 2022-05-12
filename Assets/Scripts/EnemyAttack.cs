using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public GameObject Projectile;
    public float ProjectileSpeed;
    public float AttackCooldown;
    public bool SpreadAttack;
    private float nextAttack;

    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SetNextAttack(float time)
    {
        nextAttack = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextAttack > Time.time)
            return;

        if (SpreadAttack)
        {
            // Make projectiles fan out
            for (int i = 0; i < 5; i++)
            {
                var position = new Vector2(this.transform.position.x, this.transform.position.y);
                var playerPosition = new Vector2(player.transform.position.x, player.transform.position.y);

                var delta = playerPosition - position;
                delta.Normalize();
                var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
                angle = (angle + Random.Range(-30, 30)) * Mathf.Deg2Rad;

                var projectile = Instantiate(Projectile, this.transform.position, Quaternion.Euler(new Vector3(0, 0, angle * Mathf.Rad2Deg)));
                projectile.GetComponent<ProjectileCollide>().damage = 1;

                var rigidbody = projectile.GetComponent<Rigidbody2D>();
                rigidbody.AddForce(new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * ProjectileSpeed, ForceMode2D.Impulse);
            }
        }
        else
        {
            var delta = new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(this.transform.position.x, this.transform.position.y);
            delta.Normalize();
            var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;

            var projectile = Instantiate(Projectile, this.transform.position, Quaternion.Euler(new Vector3(0, 0, angle - 90)));
            projectile.GetComponent<ProjectileCollide>().damage = 1;

            var rigidbody = projectile.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(delta * ProjectileSpeed, ForceMode2D.Impulse);
        }

        nextAttack = Time.time + AttackCooldown;
    }
}
