using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public Text waveText;
    public GameObject Player;

    private void OnEnable()
    {
        PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
        PlayerHealth.OnPlayerDeath += Setup;
    }
    
    public void Setup(){
        gameObject.SetActive(true);
        waveText.text = "Wave 1";
    }
}
