using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 4.0f;          // ad
    public float heightOffset = 10.0f;      // adjusts the 

    private float timer = 0.0f;
    private float baseY;                     // remember where the spawner started  

    // track how many spawners exist (to detect duplicates)
    private static int globalSpawnerCounter = 0;
    private int id;

    void Awake()
    {
        // remember where the spawner is positioned
        // (we'll move it vertically each time we spawn a pipe)
        baseY = transform.position.y;
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
        float randomY = Random.Range(baseY - heightOffset, baseY + heightOffset);

        Vector3 spawnPos = new Vector3(transform.position.x, randomY, transform.position.z);

        Instantiate(pipe, spawnPos, transform.rotation);
    }
}
