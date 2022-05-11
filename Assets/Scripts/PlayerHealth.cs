using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDamage;
    public static event Action OnPlayerDeath;
    public Death Death;


    public int Health, MaxHealth;

    public void ResetHealth(){
        Health = MaxHealth;
    }

    public void TakeDamage(int amount){
        Health -= amount;
        OnPlayerDamage?.Invoke();

        FindObjectOfType<AudioManager>().Play("player_damage");

        if(Health <= 0)
        {
            Health = 0;
            Debug.Log("Death");
            Death.Setup();
            OnPlayerDeath?.Invoke();
            gameObject.GetComponent<Movement>().speed = 0f;
        }
    }
}
