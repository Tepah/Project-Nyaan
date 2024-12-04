using System.Collections;
using UnityEngine;

public class HSpawner : MonoBehaviour
{
    public GameObject[] fallingItems; 
    public float spawnInterval = 3f; // Time between Spawns
    public float horizontalForce = 10f; // Horizontal force to apply to the spawned items

    private float screenMinX;
    private float screenMaxX;
    private float screenMinY;
    private float screenMaxY;

    void Start()
    {
        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.transform.position.z));

        screenMinX = screenBottomLeft.x;
        screenMaxX = screenTopRight.x;
        screenMinY = screenBottomLeft.y;
        screenMaxY = screenTopRight.y;

        StartCoroutine(SpawnFallingItems());
    }

    IEnumerator SpawnFallingItems()
    {
        while (true)
        {
            GameObject itemToSpawn = fallingItems[Random.Range(0, fallingItems.Length)];

            // Randomly choose to spawn from the left or right side
            bool spawnFromLeft = Random.value > 0.5f;
            float spawnX = spawnFromLeft ? screenMinX : screenMaxX;
            float randomY = Random.Range(screenMinY, screenMaxY);
            Vector3 spawnPosition = new Vector3(spawnX, randomY, 0);
            
            GameObject spawnedItem = Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.gravityScale = 1;
                float forceDirection = spawnFromLeft ? 1 : -1;
                rb.AddForce(new Vector2(horizontalForce * forceDirection, 0), ForceMode2D.Impulse);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}