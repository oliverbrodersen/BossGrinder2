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

    void Start()
    {
    }

    void Update()
    {
        // Check if there are any enmmes left.
        // if not spawn the next wave
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(wave == 0){
            enemiesText.text = "Kill boss to start wave 1";
        }
        else{
            enemiesText.text =  enemies.Length + (enemies.Length == 1 ? " boss" : " bosses") + " remaining";
        }
        if (enemies.Length == 0)
        {
            wave++;
            waveText.text = "Wave " + wave.ToString();
            SpawnWave();
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
    }

    private void ClearWave(){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void StartWaves()
    {
        wave = 1;
        waveText.gameObject.SetActive(true);
        waveText.text = "Wave " + wave.ToString();
        ClearWave();
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesText.text =  enemies.Length + (enemies.Length == 1 ? " boss" : " bosses") + " remaining";
    }
}
