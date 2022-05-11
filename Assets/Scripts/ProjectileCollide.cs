using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollide : MonoBehaviour
{
    public int damage;

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {

        }
        else if (collision.gameObject.name == "Dragon")
        {

        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            FindObjectOfType<AudioManager>().Play("knife_hit");
            Destroy(gameObject);
        }
        else
        {
            // Hit wall
            Destroy(gameObject);
        }
    }
}
