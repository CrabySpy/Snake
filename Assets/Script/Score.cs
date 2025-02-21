using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private float elapsedTime;
    private bool isTimerRunning = true;

    void Update()
    {
        if (isTimerRunning)
        {
            elapsedTime += Time.deltaTime;
            scoreText.text = Mathf.FloorToInt(elapsedTime).ToString();
        }
    }

    // Getter method to access elapsed time
    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
