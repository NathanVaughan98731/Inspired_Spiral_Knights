using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Transform enemy;
    public TextMeshProUGUI enemyCountText;
    // We want to delay our code at certain times
    public float timeBeforeSpawning = 1.5f;
    public float timeBetweenEnemies = .25f;
    public float timeBeforeWaves = 2.0f;

    public int enemiesPerWave = 10;
    private int currentNumberOfEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        enemyCountText.text = "Enemies: " + currentNumberOfEnemies.ToString() + "/" + enemiesPerWave.ToString();

    }

    IEnumerator SpawnEnemies()
    {
        // Give the player time before we start the game
        yield return new WaitForSeconds(timeBeforeSpawning);

        // After timeBeforeSpawning has elapsed, we will enter this loop
        while(true)
        {
            // Don't spawn anything new until all the previous waves enemies are dead
            if (currentNumberOfEnemies <= 0)
            {
                float randDirection;
                float randDistance;

                // Spawn 10 enemies in a random position
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    randDirection = Random.Range(0, 360);
                    randDistance = Random.Range(40, 60);

                    float posX = this.transform.position.x + (Mathf.Cos((randDirection) * Mathf.Deg2Rad) * randDistance);
                    float posZ = this.transform.position.z + (Mathf.Sin((randDirection) * Mathf.Deg2Rad) * randDistance);

                    // Spawn the enemy and increment the number of enemies spawned
                    Instantiate(enemy, new Vector3(posX, 1, posZ), this.transform.rotation);
                    currentNumberOfEnemies++;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }

            yield return new WaitForSeconds(timeBeforeWaves);
        }
    }

    // Allows classes outside of GameController to say when we killed 
    // an enemy.
    public void KilledEnemy()
    {
        currentNumberOfEnemies--;
    }


}
