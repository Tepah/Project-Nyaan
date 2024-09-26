using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] fallingItems; 
    public float spawnInterval = 1f; // Time between Spawns
    public float ySpawnPosition = 6f; // position of spawn from above screen

    private float screenMinX;
    private float screenMaxX;

    void Start()
    {
        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.transform.position.z));

        screenMinX = screenBottomLeft.x;
        screenMaxX = screenTopRight.x;

        StartCoroutine(SpawnFallingItems());
    }

    IEnumerator SpawnFallingItems()
    {
        while (true)
        {
            GameObject itemToSpawn = fallingItems[Random.Range(0, fallingItems.Length)];

            float randomX = Random.Range(screenMinX, screenMaxX);
            Vector3 spawnPosition = new Vector3(randomX, ySpawnPosition, 0);
            
            Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}