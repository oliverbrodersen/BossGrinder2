using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
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
        if (enemies.Length == 0)
        {
            wave++;
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

    public void StartWaves()
    {
        wave = 1;
    }

}
