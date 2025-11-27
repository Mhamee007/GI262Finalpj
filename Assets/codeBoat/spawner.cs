using UnityEngine;

public class spawner :MonoBehaviour
{
    public GameObject fishNormalPrefab;   
    public GameObject fishShyPrefab;
    public fishType[] fishTypes;

    public float fishSpawnInterval = 20f;
    public float fishShySpawnInterval = 30f;
    public float spawnRadius = 8f;

    float fishTimer = 0f;
    float fishShyTimer = 0f;
    void Update()
    {
        fishTimer += Time.deltaTime;
        fishShyTimer += Time.deltaTime;

        
        if (fishTimer >= fishSpawnInterval)
        {
            int count = Random.Range(2, 4); 
            SpawnMultipleFish(fishNormalPrefab, count);
            fishTimer = 0f;
        }

       
        if (fishShyTimer >= fishShySpawnInterval)
        {
            SpawnMultipleFish(fishShyPrefab, 1);
            fishShyTimer = 0f;
        }
    }

    void SpawnMultipleFish(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnFish(prefab);
        }
    }

    void SpawnFish(GameObject prefab)
    {
        Vector2 pos = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

        GameObject f = Instantiate(prefab, pos, Quaternion.identity);
        fishType data = fishTypes[Random.Range(0, fishTypes.Length)];

        FishManager fm = f.GetComponent<FishManager>();
        fm.fishData = data;
    }
}
