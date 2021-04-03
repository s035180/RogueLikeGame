
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 2f;
    public float newLevelDelay = 5f;
    public GameObject dieLevelUI;
    public GameObject nextLevelUI;

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            dieLevelUI.SetActive(true);
            Invoke("Restart", restartDelay);
        }
    }

    public void LoadNewLevel()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Congratulations!");
            nextLevelUI.SetActive(true);
            Invoke("NewLevel", newLevelDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void NewLevel()
    {
        StaticData.Score += 1000;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
