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

    private ScoreText scoreText; // Reference to ScoreText script
    private int nextGrowthTime = 4; // First growth at 4 seconds

    void Start()
    {
        Debug.Log("Snake script started!");
        scoreText = FindObjectOfType<ScoreText>(); // Find ScoreText script in scene
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
                nextGrowthTime += 4; // Set the next growth time
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
        Debug.Log("Snake grew! New segment added.");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Evil Blocks") || other.CompareTag("Apple") || other.CompareTag("Wall") || other.CompareTag("Body"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        if (gameOverMenu != null)
        {
            gameOverMenu.SetActive(true);
        }
        SceneManager.LoadScene(0);
    }
}
