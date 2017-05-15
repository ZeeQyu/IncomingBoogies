using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRulesBehaviour : MonoBehaviour {
    [SerializeField] private int numSpawners = 12;
    // Distance from border of playing field
    [SerializeField] private float spawnerPadding = 0.5f;
    [SerializeField] private GameObject spawnerPrefab;
    [SerializeField] private GameObject[] enemyPrefabs;

    private float arenaRadius;
    private GameObject[] spawners;
    private int waveSize;
    private bool isFirstWave;

    private int numEnemies;
    private bool wasPlayerHit;

    // Behaviour
    void Start() {
        // Find out how big the radius of the circle that is the arena is by finding the white background size and measuring that.
        arenaRadius = FindObjectOfType<ArenaRadiusMarker>().GetComponent<SpriteRenderer>().sprite.bounds.extents.x;

        // Create the spawning points
        spawners = new GameObject[numSpawners];
        for (int i = 0; i < numSpawners; i++)
        {
            float spawnerX = Mathf.Cos(2 * Mathf.PI * i / numSpawners) * (arenaRadius - spawnerPadding);
            float spawnerY = Mathf.Sin(2 * Mathf.PI * i / numSpawners) * (arenaRadius - spawnerPadding);
            spawners[i] = Instantiate(spawnerPrefab);
            spawners[i].transform.Translate(spawnerX, spawnerY, 0.0f);
        }

        waveSize = enemyPrefabs.Length;
        isFirstWave = true;

        StartNextWave();
	}

    void StartNextWave()
    {
        wasPlayerHit = false;
        // Find out how many of each enemy we should spawn
        int numEnemyTypes = enemyPrefabs.Length;
        int[] enemyAmounts = new int[numEnemyTypes];
        if (isFirstWave)
        {
            for (int i = 0; i < numEnemyTypes; i++)
            {
                enemyAmounts[i] = 1;
            }
        }
        else
        {
            for (int i = 0; i < waveSize; i++)
            {
                int randomEnemyType = Random.Range(0, numEnemyTypes);
                enemyAmounts[randomEnemyType] += 1;
            }
        }

        // Distribute enemies on the different spawn points
        List<int> remainingSpawners = new List<int>(numSpawners);
        for (int i = 0; i < numSpawners; i++)
        {
            remainingSpawners.Add(i);
        }

        numEnemies = 0;
        for (int enemyIndex = 0; enemyIndex < numEnemyTypes; enemyIndex++)
        {
            for (int i = 0; i < enemyAmounts[enemyIndex]; i++)
            {
                // Find the next spawner
                int spawnerIndexIndex = Random.Range(0, remainingSpawners.Count);
                int spawnerIndex = remainingSpawners[spawnerIndexIndex];
                remainingSpawners.RemoveAt(spawnerIndexIndex);
                GameObject spawner = spawners[spawnerIndex];

                // Create an enemy
                GameObject newEnemy = Instantiate(enemyPrefabs[enemyIndex]);
                newEnemy.SendMessage("SetPosition", spawner.transform.position);
                numEnemies += 1;

            }
        }
    }

    void NextDifficultyLevel()
    {
        if (isFirstWave)
            isFirstWave = false;
        else
            waveSize += 1;
        FindObjectOfType<PlayerBehaviour>().SendMessage("OnNextDifficultyLevel");
        ScoreKeeper.difficultyLevelReached += 1;
    }

    // Getters/Setters
    public float GetEnemyTravelDistance()
    {
        return (arenaRadius - spawnerPadding);
    }

    public float GetArenaRadius()
    {
        return arenaRadius;
    }

    // Events
    void OnEnemyDestroyed()
    {
        numEnemies -= 1;
        if (numEnemies <= 0)
        {
            if (!wasPlayerHit)
            {
                NextDifficultyLevel();
            }
            ScoreKeeper.wavesSurvived += 1;
            StartNextWave();
        }
    }
    void OnPlayerHit()
    {
        wasPlayerHit = true;
    }
    void OnPlayerDead()
    // This means we lost
    {
        SceneManager.LoadScene(2);
    }
}
