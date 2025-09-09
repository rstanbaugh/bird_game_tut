using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 4.0f;
    public float heightOffset = 10.0f;

    private float timer = 0.0f; 

    // track how many spawners exist (to detect duplicates)
    private static int globalSpawnerCounter = 0;
    private int id;

    void Awake()
    {
        id = ++globalSpawnerCounter;
        Debug.Log($"[Spawner #{id}] Awake on {gameObject.name}. spawnRate={spawnRate}, timeScale={Time.timeScale}");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnPipe();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            // Debug.Log($"SPAWN at t={Time.time:F2}s (timer={timer:F2})");
            SpawnPipe();
            timer = 0.0f;
        }


    }
    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        float randomY = Random.Range(lowestPoint, highestPoint);
        transform.position = new Vector3(transform.position.x, randomY, transform.position.z);

        Instantiate(pipe, transform.position, transform.rotation);
    }
}
