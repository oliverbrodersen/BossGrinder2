using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject DeathMessage;
    public GameObject Player;


    private void OnEnable()
    {
        PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
        PlayerHealth.OnPlayerDeath += ShowMessage;
    }
    
    private void ShowMessage(){
        GameObject message = Instantiate(DeathMessage);
        message.transform.SetParent(transform);
    }
}
