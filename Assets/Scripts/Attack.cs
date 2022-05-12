using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public Transform Origin;
    public Camera Camera;

    public GameObject Knife;
    public float KnifeSpeed;
    public float KnifeCooldown;
    private float NextKnifeAttack = 0;

    public GameObject KnifeImage;

    public GameObject Fireball;
    public float FireballSpeed;
    public float FireballCooldown;
    private float NextFireballAttack = 0;
    public GameObject FireballImage;

    Vector2 mousePosition;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.ScreenToWorldPoint(Input.mousePosition);
        if (NextKnifeAttack < Time.time)
        {
            KnifeImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            if(Input.GetButtonDown("Fire1")){
                NextKnifeAttack = Time.time + KnifeCooldown;
                KnifeImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
                Shoot();
            }
        }
        if (NextFireballAttack < Time.time)
        {
            FireballImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            if(Input.GetButtonDown("Fire2")){
                NextFireballAttack = Time.time + FireballCooldown;
                FireballImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.2f);
                ThrowFireball();
            }
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
        rigidbody.AddForce(delta * KnifeSpeed, ForceMode2D.Impulse);
    }

    void ThrowFireball()
    {
        var delta = mousePosition - new Vector2(Origin.position.x, Origin.position.y);
        delta.Normalize();
        var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;

        var fireball = Instantiate(Fireball, Origin.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        fireball.GetComponent<ProjectileCollide>().damage = 3;

        var rigidbody = fireball.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(delta * FireballSpeed, ForceMode2D.Impulse);
    }

}
