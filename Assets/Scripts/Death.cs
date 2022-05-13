using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    public Text waveText;
    public GameObject Player;
    public WaveController WaveController;

    private bool _gameOver;

    private void OnEnable()
    {
        PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
        PlayerHealth.OnPlayerDeath += Setup;
    }
    
    public void Setup(){
        if(!_gameOver){
            _gameOver = true;
            gameObject.SetActive(true);
            waveText.text = "Wave " + WaveController.wave.ToString();
            FindObjectOfType<AudioManager>().Play("game_over");
        }
    }

    public void Restart(){
        _gameOver = false;
        gameObject.SetActive(false);
        PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
        playerHealth.Health = playerHealth.MaxHealth;
        Player.GetComponent<Movement>().speed = 8f;
        Player.transform.position = new Vector3(0,0,0);
        WaveController.StartWaves();
    }
}
