using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollide : MonoBehaviour
{
    public int damage;
    public bool PlayerOwned;
    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Dragon")
            return;

        if (PlayerOwned)
        {
            if (collision.gameObject.name == "Player")
                return;

            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                FindObjectOfType<AudioManager>().Play("knife_hit");
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            if (collision.gameObject.tag == "Enemy")
                return;

            if (collision.gameObject.name == "Player")
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
                FindObjectOfType<AudioManager>().Play("knife_hit");
                Destroy(gameObject);
                return;
            }
        }

        // Hit wall
        Destroy(gameObject);
    }
}
