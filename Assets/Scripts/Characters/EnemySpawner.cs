using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private int? _spawnersCount;

    public float TimeBetweenSpawn;
    public GameObject[] AvailableEnemysToSpawn;

    void Start()
    {
        if (AvailableEnemysToSpawn == null) throw new MissingComponentException($"Missing available enemys to spawn by the spawner : {name}");
        _spawnersCount = GameObject.FindGameObjectsWithTag(Tags.Spawner)?.Length;
    }

    void Update()
    {
        if (AvailableEnemysToSpawn.Length == 0) return;
        if (ShouldSpawnAtCurrentFrame())
        {
            GameObject enemy = GetRandomEnemy();
            Spawn(enemy);
        }
    }

    private bool ShouldSpawnAtCurrentFrame()
    {
        int spawnersCountFactor = _spawnersCount.HasValue ? _spawnersCount.Value : 1;
        float spawnRate = 1 / TimeBetweenSpawn;
        float spawnThreshold = spawnRate * Time.deltaTime / spawnersCountFactor;
        return Random.value < spawnThreshold;
    }

    private GameObject GetRandomEnemy()
    {
        int randomNumber = Random.Range(0, AvailableEnemysToSpawn.Length);
        return AvailableEnemysToSpawn[randomNumber];
    }

    void Spawn(GameObject gameObject)
    {
        if (gameObject == null) return;
        GameObject spawnedEnemy = Instantiate(gameObject, transform);
        spawnedEnemy.transform.position = transform.position;
    }
}
