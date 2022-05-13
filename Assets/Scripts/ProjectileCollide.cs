using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollide : MonoBehaviour
{
    public int damage;
    public bool PlayerOwned;

    public GameObject Blood;

    public void SetDamage(int dmg)
    {
        damage = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
            return;

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

                var blood = Instantiate(Blood, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

                Destroy(gameObject);
                Destroy(blood, .5f);
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

                var blood = Instantiate(Blood, gameObject.transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));

                Destroy(gameObject);
                Destroy(blood, .5f);
                return;
            }
        }

        // Hit wall
        Destroy(gameObject);
    }
}
