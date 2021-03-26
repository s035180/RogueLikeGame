using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void endGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void startGame()
    {
        Debug.Log("StartGame");
        SceneManager.LoadScene(1);
    }

    public void returnToMenu()
    {
        Debug.Log("Return To Menu");
        SceneManager.LoadScene(0);
    }
}
