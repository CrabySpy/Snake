using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    public GameObject applePrefab;   // Drag the apple prefab here
    public Vector2Int gridSize = new Vector2Int(15, 15); // Map size
    public float gridUnit = 1f;       // Distance between grid units
    public float spawnInterval = 4f;  // Spawn interval in seconds

    private float timer = 0f;         // Timer to track spawn interval
    private GameObject currentApple;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f; // Reset timer

            if (currentApple == null)  // Only spawn if there isn't an apple already
            {
                SpawnApple();
            }
        }
    }

    public void SpawnApple()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-gridSize.x / 2, gridSize.x / 2) * gridUnit,
            Random.Range(-gridSize.y / 2, gridSize.y / 2) * gridUnit,
            0f
        );

        currentApple = Instantiate(applePrefab, spawnPosition, Quaternion.identity);
    }
}
