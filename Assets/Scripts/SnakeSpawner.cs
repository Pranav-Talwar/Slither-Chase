using UnityEngine;
using System.Collections;

public class SnakeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] SnakeReference; // Array of snake prefabs

    private GameObject spawnedSnake;

    // Spawn positions for left and right
    private Vector3 leftPosition = new Vector3(-25f, -2.6f, 0f);
    private Vector3 rightPosition = new Vector3(48f, -2.6f, 0f);

    private int randomIndex;
    private int randomSide;

    [SerializeField]
    private int maxSnakesToSpawn = 10; // Maximum number of snakes to spawn

    private int snakeCount = 0; // Counter to track the number of snakes spawned

    void Start()
    {
        // Start separate coroutines for each side
        StartCoroutine(SpawnSnakesLeft());
        StartCoroutine(SpawnSnakesRight());
    }

    IEnumerator SpawnSnakesLeft()
    {
        // Ensure the SnakeReference array has at least one snake prefab
        if (SnakeReference.Length == 0) yield break;

        // Continue spawning until maxSnakesToSpawn is reached
        while (snakeCount < maxSnakesToSpawn)
        {
            // Check if there are already snakes in the scene
            if (GameObject.FindGameObjectsWithTag("Snake").Length >= maxSnakesToSpawn)
            {
                yield return null; // Wait and check again in the next frame
                continue; // Skip spawning for now
            }

            // Wait for a random time interval before spawning a new snake
            yield return new WaitForSeconds(Random.Range(3f, 6f));

            randomIndex = Random.Range(0, SnakeReference.Length);

            // Spawn on the left side at the specified position
            spawnedSnake = Instantiate(SnakeReference[randomIndex]);
            spawnedSnake.transform.position = leftPosition;
            spawnedSnake.GetComponent<Snake>().speed = Random.Range(1, 3); // Keep speed positive for rightward movement
            spawnedSnake.tag = "Snake"; // Add a tag to identify spawned snakes

            snakeCount++; // Increment snake count
        }
    }

    IEnumerator SpawnSnakesRight()
    {
        // Ensure the SnakeReference array has at least one snake prefab
        if (SnakeReference.Length == 0) yield break;

        // Continue spawning until maxSnakesToSpawn is reached
        while (snakeCount < maxSnakesToSpawn)
        {
            // Check if there are already snakes in the scene
            if (GameObject.FindGameObjectsWithTag("Snake").Length >= maxSnakesToSpawn)
            {
                yield return null; // Wait and check again in the next frame
                continue; // Skip spawning for now
            }

            // Wait for a random time interval before spawning a new snake
            yield return new WaitForSeconds(Random.Range(3f, 6f));

            randomIndex = Random.Range(0, SnakeReference.Length);

            // Spawn on the right side at the specified position and flip direction
            spawnedSnake = Instantiate(SnakeReference[randomIndex]);
            spawnedSnake.transform.position = rightPosition;
            spawnedSnake.GetComponent<Snake>().speed = -Random.Range(1, 3); // Keep negative speed for leftward movement
            spawnedSnake.transform.localScale = new Vector3(-1f, 1f, 1f); // Flip the scale to make the snake face the other way
            spawnedSnake.tag = "Snake"; // Add a tag to identify spawned snakes

            snakeCount++; // Increment snake count
        }
    }

}
