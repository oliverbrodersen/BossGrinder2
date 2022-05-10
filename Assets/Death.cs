using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public Text waveText;
    public GameObject Player;
    public WaveController WaveController;

    private void OnEnable()
    {
        PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
        PlayerHealth.OnPlayerDeath += Setup;
    }
    
    public void Setup(){
        gameObject.SetActive(true);
        waveText.text = "Wave " + WaveController.wave.ToString();
    }

    public void Restart(){
        gameObject.SetActive(false);
        PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
        playerHealth.Health = playerHealth.MaxHealth;
        WaveController.StartWaves();
    }
}
