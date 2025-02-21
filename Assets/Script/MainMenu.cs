
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SnakeHead;

    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
        if (SnakeHead != null)
        {
            SnakeHead.GetComponent<Snake>().enabled = true; // Enable movement
        }
    }



    public void QuitGame()
    {
        Debug.Log("Game Quit!"); 
        Application.Quit();      
    }
}
