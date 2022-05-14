using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    public Text waveText;
    public Text enemiesText;
    public GameObject enemy;
    public GameObject skeleton;
    public GameObject boss;
    public SpriteRenderer spawnBox;
    public int wave;

    public Transform ChestSpawn;
    public Chest Chest;

    private bool _waveStarted = true;

    public GameObject Player;

    private void OnEnable()
    {
        PlayerHealth playerHealth = Player.GetComponent<PlayerHealth>();
        PlayerHealth.OnPlayerDeath += ClearWave;
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
                enemiesText.text =  "Spawning bosses";
            }
        }
        if (enemies.Length == 0)
        {
            if(_waveStarted){
                _waveStarted = false;
                FindObjectOfType<AudioManager>().Play("start_wave");
                wave++;
                waveText.text = "Wave " + wave.ToString();

                if(wave > 1){
                    SummonChest();
                    Invoke("SpawnWave", 5);
                }
                else{
                    Invoke("SpawnWave", 2);
                }
            }
        }

    }

    private void SummonChest(){
        Instantiate(Chest, ChestSpawn.position, Quaternion.Euler(new Vector3(0, 0, 0)));
    }

    private Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    private Vector3 RandomPointoOnBoundsEdge(Bounds bounds)
    {
        var side = (int)(Random.value * 3);
        switch (side)
        {
            case 0:
                return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                bounds.min.y,
                Random.Range(bounds.min.z, bounds.max.z));
            case 1:
                return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                bounds.max.y,
                Random.Range(bounds.min.z, bounds.max.z));
            case 2:
                return new Vector3(
                bounds.min.x,
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z));
            case 3:
                return new Vector3(
                bounds.max.x,
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z));
            default:
                throw new System.Exception("A square has 4 sides. how did we end up here?");
        }

        //return new Vector3(
        //    Random.Range(bounds.min.x, bounds.max.x),
        //    Random.Range(bounds.min.y, bounds.max.y),
        //    Random.Range(bounds.min.z, bounds.max.z));
    }

    private void SpawnWave()
    {
        if (wave % 3 == 0)
        {
            // Add a random delay to the attack, as not to attack at the same time as all other enemies
            var newBoss = Instantiate(boss, RandomPointoOnBoundsEdge(spawnBox.bounds), Quaternion.identity);
            newBoss.GetComponent<EnemyAttack>().SetNextAttack(Time.time + 1 + Random.value * 1);

            // Cap enemies at wave / 3
            var enemyCount = (wave - 1) / 3;
            if (enemyCount > 3)
                enemyCount = 3;

            for (int i = 0; i < enemyCount; i++)
            {
                var enemyType = (Random.value >= 0.5) ? enemy : skeleton;
                var newEnemy = Instantiate(enemyType, RandomPointoOnBoundsEdge(spawnBox.bounds), Quaternion.identity);

                // Add a random delay to the attack, as not to attack at the same time as all other enemies
                newEnemy.GetComponent<EnemyAttack>().SetNextAttack(Time.time + 1 + Random.value * 1);
            }
        }
        else
        {
            // Cap enemies at 5
            var enemyCount = wave > 5 ? 5 : wave;
            for (int i = 0; i < enemyCount; i++)
            {
                var enemyType = (Random.value >= 0.5) ? enemy : skeleton;
                var newEnemy = Instantiate(enemyType, RandomPointoOnBoundsEdge(spawnBox.bounds), Quaternion.identity);

                // Add a random delay to the attack, as not to attack at the same time as all other enemies
                newEnemy.GetComponent<EnemyAttack>().SetNextAttack(Time.time + 1 + Random.value * 1);
            }
        }
        _waveStarted = true;
    }

    private void ClearWave(){
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var enemy in enemies)
        {
            Destroy(enemy);
        }
        var Loot = GameObject.FindGameObjectsWithTag("Loot");
        foreach(var loot in Loot)
        {
            Destroy(loot);
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
