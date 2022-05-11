using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    public Text waveText;
    public Text enemiesText;
    public GameObject enemy;
    public SpriteRenderer spawnBox;
    public int wave;
    private bool _waveStarted = true;

    void Start()
    {
    }

    void Update()
    {
        // Check if there are any enmmes left.
        // if not spawn the next wave
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(wave == 0){
                waveText.text = "Boss Grinder";
            enemiesText.text = "Kill skeleton to start wave 1";
        }
        else{
            if(_waveStarted){
                enemiesText.text =  enemies.Length + (enemies.Length == 1 ? " boss" : " bosses") + " remaining";
            }
            else{
                enemiesText.text =  wave + (wave == 1 ? " boss" : " bosses") + " spawning";
            }
        }
        if (enemies.Length == 0)
        {
            if(_waveStarted){
                _waveStarted = false;
                FindObjectOfType<AudioManager>().Play("start_wave");
                wave++;
                waveText.text = "Wave " + wave.ToString();
                
                //Dramatic effect
                Invoke("SpawnWave", 2);
            }
        }

    }

    private Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private void SpawnWave()
    {
        for (int i = 0; i < wave; i++)
        {
            var newEnemy = Instantiate(enemy, RandomPointInBounds(spawnBox.bounds), Quaternion.identity);
        }
        _waveStarted = true;
    }

    private void ClearWave(){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemies)
        {
            Destroy(enemy);
        }
        _waveStarted = false;
    }

    public void StartWaves()
    {
        ClearWave();
        wave = 0;
        _waveStarted = true;
        waveText.text = "Wave " + wave.ToString();
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesText.text =  enemies.Length + (enemies.Length == 1 ? " boss" : " bosses") + " remaining";
    }
}
