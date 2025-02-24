using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    private Vector2 direction = Vector2.right;
    private List<Transform> snakeBody = new List<Transform>();
    public GameObject bodyPrefab;
    public GameObject gameOverMenu;

    private ScoreText scoreText;
    private int nextGrowthTime = 4;

    void Start()
    {
        scoreText = FindObjectOfType<ScoreText>();
        InvokeRepeating(nameof(Move), moveSpeed, moveSpeed);
    }

    void Update()
    {
        if (scoreText != null)
        {
            int elapsedSeconds = Mathf.FloorToInt(scoreText.GetElapsedTime());
            if (elapsedSeconds >= nextGrowthTime)
            {
                Grow();
                nextGrowthTime += 2;
                Debug.Log($"Snake grew at {elapsedSeconds} seconds. Next growth at {nextGrowthTime}s.");
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down) direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up) direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right) direction = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left) direction = Vector2.right;
    }

    void Move()
    {
        Vector2 newPosition = (Vector2)transform.position + direction;

        for (int i = snakeBody.Count - 1; i > 0; i--)
        {
            snakeBody[i].position = snakeBody[i - 1].position;
        }

        if (snakeBody.Count > 0)
            snakeBody[0].position = transform.position;

        transform.position = newPosition;
    }

    public void Grow()
    {
        GameObject bodyPart = Instantiate(bodyPrefab, transform.position, Quaternion.identity);
        snakeBody.Add(bodyPart.transform);
    }

    //  FIXED OnTriggerEnter2D Function with Parameter
    /*void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"Collided with: {other.gameObject.name}"); // Debug collision

        if (other.CompareTag("Wall"))
        {
            Debug.Log("Collided with the wall! Game Over.");
            GameOver();
        }
    }
*/
    void GameOver()
    {
        if (gameOverMenu != null)
        {
            Time.timeScale = 0f; // Pause the game
            gameOverMenu.SetActive(true);
        }
        SceneManager.LoadScene(0);
    }
}
