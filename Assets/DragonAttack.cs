using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    public GameObject fireball;
    public float fireballSpeed;
    public float fireballCooldown;
    private float nextAttack;

    public WaveController waveController;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0)
            return;

        if (waveController.wave == 0)
            return;

        if (nextAttack > Time.time)
            return;

        nextAttack = Time.time + fireballCooldown;

        // Find enemey closest to the dragon and throw a fireball
        Vector3 bestDelta = Vector3.zero;
        float bestMagnitude = float.MaxValue;
        foreach (var enemy in enemies)
        {
            var delta = (enemy.transform.position - this.transform.position);
            if (delta.magnitude < bestMagnitude)
            {
                bestDelta = delta;
                bestMagnitude = delta.magnitude;
            }
        }

        bestDelta.Normalize();
        var angle = Mathf.Atan2(bestDelta.y, bestDelta.x) * Mathf.Rad2Deg;

        var knife = Instantiate(fireball, this.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));
        knife.GetComponent<ProjectileCollide>().damage = 1;

        var rigidbody = knife.GetComponent<Rigidbody2D>();
        rigidbody.AddForce(bestDelta * fireballSpeed, ForceMode2D.Impulse);

    }
}
