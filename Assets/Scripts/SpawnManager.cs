using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] powerups;
    private GameObject player;
    public bool inBetweenWaves = false;
    public bool canSpawnPowerup = true;
    public int waveCount = 1;
    public int powerupCount;
    public int enemyCount;
    private float xRange = 48.0f;
    private float yRange = 5.0f;
    private float zRange = 48.0f;
    // Start is called before the first frame update
    void Start()
    {
        InitializeGame();
        powerupCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        player = GameObject.Find("Player");
    }

    void InitializeGame()
    {
        SpawnWave();
        SpawnPowerup();
        Debug.Log("Waves begin!");
    }

    // Update is called once per frame
    void Update()
    {
        if(!inBetweenWaves)
        {
            CheckWaveStatus();
            if(canSpawnPowerup)
            {
                SpawnPowerup();
            }
        }
    }

    // Chooses a random location within the arena to spawn something
    Vector3 ChooseSpawnLocation()
    {
        float randomX = Random.Range(-xRange,xRange);
        float randomY = Random.Range(0.5f,yRange);
        float randomZ = Random.Range(-zRange,zRange);

        Vector3 spawnPos = new Vector3(randomX,randomY,randomZ);

        return spawnPos;
    }

    // Spawns a new wave of enemies based on the wavecount
    void SpawnWave()
    {
        for(int i = 0; i < waveCount; i++)
        {
            int enemyIndex = Random.Range(0,enemies.Length);
            Vector3 spawnPos = ChooseSpawnLocation();
            Instantiate(enemies[enemyIndex],spawnPos,enemies[enemyIndex].gameObject.transform.rotation);
        }
    }

    // Spawns a powerup if the previous powerup has been consumed
    void SpawnPowerup()
    {
        powerupCount = GameObject.FindGameObjectsWithTag("Powerup").Length;
        if(powerupCount == 0)
        {
            canSpawnPowerup = false;
            StartCoroutine(PowerupSpawnCooldown());
            int powerupIndex = Random.Range(0,powerups.Length);
            float randomX = Random.Range(-xRange,xRange);
            float randomZ = Random.Range(-zRange,zRange);

            Vector3 spawnPos = new Vector3(randomX,0.5f,randomZ);
            Instantiate(powerups[powerupIndex],spawnPos,powerups[powerupIndex].gameObject.transform.rotation);
        }
    }

    // Delay for powerup spawning so they don't spawn as soon as old one is consumed
    IEnumerator PowerupSpawnCooldown()
    {
        yield return new WaitForSeconds(20);
        canSpawnPowerup = true;
    }

    void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0,0.5f,0);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    void CompleteWave()
    {
        Debug.Log("Wave " + waveCount + " complete!");
        waveCount++;
        inBetweenWaves = true;
        ResetPlayerPosition();
    }

    IEnumerator BetweenWaveCooldown()
    {
        yield return new WaitForSeconds(1);
        inBetweenWaves = false;
        SpawnWave();
    }

    void CheckWaveStatus()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemyCount == 0)
        {
            CompleteWave();
            StartCoroutine(BetweenWaveCooldown());
        }
    }
}
