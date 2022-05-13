using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType
{
    Health,
    Speed
}

public class Potion : MonoBehaviour
{
    public PotionType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        switch (type)
        {
            case PotionType.Health:
                FindObjectOfType<AudioManager>().Play("drink");
                collision.gameObject.GetComponent<PlayerHealth>().Heal(8, true);
                Destroy(gameObject);
                break;
            case PotionType.Speed:
                FindObjectOfType<AudioManager>().Play("drink");
                collision.gameObject.GetComponent<Movement>().GrantSpeedBoost();
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
