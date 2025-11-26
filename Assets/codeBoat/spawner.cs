using UnityEngine;

public class spawner : gameManager
{
    public GameObject fishPrefab;
    public fishType[] fishTypes;

    public float spawnTime = 3f;
    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
    float timer = 0f;

    public float spawnRate = 3f;
    public float spawnRadius = 8f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnFish), 1f, spawnRate);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnTime)
        {
            SpawnFish();
            timer = 0f;
        }
    }

    void SpawnFish()
    {
        Vector2 pos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

        GameObject f = Instantiate(fishPrefab, pos, Quaternion.identity);

        FishAI ai = f.GetComponent<FishAI>();
        ai.fishData = fishTypes[Random.Range(0, fishTypes.Length)];

    }
}
