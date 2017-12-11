using System.Collections;
using UnityEngine;

public class AlternativeEnemySpawner : MonoBehaviour
{
    public float MeanTimeBetweenSpawnInSeconds;
    public GameObject[] AvailableEnemysToSpawn;

    private static GameObject _defendersContainer;
    private static GameTime _gameTimer;
    private bool _isInitialized;
    private bool _isSpawningEnemy;


    void Start()
    {
        if (AvailableEnemysToSpawn == null) throw new MissingComponentException($"Missing available enemys to spawn by the spawner : {name}");
        _defendersContainer = GameObject.FindGameObjectWithTag(Tags.DefendersContainter);
        _gameTimer = FindObjectOfType<GameTime>();

        if (_defendersContainer == null) Debug.LogError("DefendersContainer not found.");
        if (_gameTimer == null) Debug.LogError("GameTimer not found.");

        StartCoroutine(WaitRandomTimeBeforeSpawningFirstEnemy());
    }

    void Update()
    {
        if (_isInitialized && !_isSpawningEnemy)
        {
            _isSpawningEnemy = true;
            StartCoroutine(SpawnEnemy(GetRandomEnemy()));
        }
    }

    private GameObject GetRandomEnemy()
    {
        int randomNumber = Random.Range(0, AvailableEnemysToSpawn.Length);
        return AvailableEnemysToSpawn[randomNumber];
    }

    private IEnumerator WaitRandomTimeBeforeSpawningFirstEnemy()
    {
        yield return new WaitForSeconds(Random.Range(0f, MeanTimeBetweenSpawnInSeconds));
        _isInitialized = true;
    }

    private IEnumerator SpawnEnemy(GameObject gameObject)
    {
        float defendersCount = _defendersContainer.transform.childCount;
        float timeFactor = _gameTimer.Progress / 10;
        float difficultyFactor = Random.Range(1f, PlayerPrefsManager.Instance.GetDifficulty());

        float minValue = Mathf.Clamp(MeanTimeBetweenSpawnInSeconds - timeFactor - 5, 0f, float.MaxValue);
        float maxValue = MeanTimeBetweenSpawnInSeconds + 5 + Spawners.Attackers - defendersCount - difficultyFactor;

        float randomizedTimeSpawnInSeconds = Random.Range(minValue, maxValue);
        yield return new WaitForSeconds(randomizedTimeSpawnInSeconds);
        Spawners.Attackers++;

        GameObject spawnedEnemy = Instantiate(gameObject, transform);
        spawnedEnemy.transform.position = transform.position;
        _isSpawningEnemy = false;
    }
}
