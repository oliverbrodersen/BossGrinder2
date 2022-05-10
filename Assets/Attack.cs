using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform Origin;
    public GameObject Knife;
    public GameObject Fireball;
    public Camera Camera;
    public float projectileSpeed;
    public float attackCooldown;

    private float nextAttack = 0;

    Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetButtonDown("Fire1") && nextAttack < Time.time)
        {
            nextAttack = Time.time + attackCooldown;
            Shoot();
        }
        if (Input.GetButtonDown("Fire2") && nextAttack < Time.time)
        {
            nextAttack = Time.time + attackCooldown;
            ThrowFireball();
        }
    }

    void Shoot()
    {
        var delta = mousePosition - new Vector2(Origin.position.x, Origin.position.y);
        delta.Normalize();
        var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;

        var knife = Instantiate(Knife, Origin.position, Quaternion.Euler(new Vector3(0, 0, angle - 90)));
        knife.GetComponent<ProjectileCollide>().damage = 1;

        var rigidbody = knife.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(delta * projectileSpeed, ForceMode2D.Impulse);
    }

    void ThrowFireball()
    {
        var delta = mousePosition - new Vector2(Origin.position.x, Origin.position.y);
        delta.Normalize();
        var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;

        var fireball = Instantiate(Fireball, Origin.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        fireball.GetComponent<ProjectileCollide>().damage = 3;

        var rigidbody = fireball.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(delta * projectileSpeed, ForceMode2D.Impulse);
    }

}
