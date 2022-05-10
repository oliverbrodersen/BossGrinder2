using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCollide : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(5);
            FindObjectOfType<AudioManager>().Play("knife_hit");
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
