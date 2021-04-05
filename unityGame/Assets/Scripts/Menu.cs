using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject mainUI;
    public GameObject scoreboardUI;
    public GameObject userDataUI;
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

    public void showScoreBoard()
    {
        Debug.Log("Scoreboard");
        mainUI.SetActive(false);
        scoreboardUI.SetActive(true);
    }

    public void showUserData()
    {
        Debug.Log("User Data");
        mainUI.SetActive(false);
        userDataUI.SetActive(true);
    }

    public void showMain()
    {
        Debug.Log("User Data");
        userDataUI.SetActive(false);
        scoreboardUI.SetActive(false);
        mainUI.SetActive(true);
    }

    public void returnToMenu()
    {
        Debug.Log("Return To Menu");
        SceneManager.LoadScene(0);
    }

    public void endMenu()
    {
        Debug.Log("End Menu");
        SceneManager.LoadScene(6);
    }

    public void continueGame()
    {
        Debug.Log("Continue Game");

        StaticData.MeleeDamage += 10;
        StaticData.MeleeHP += 50;
        StaticData.MeleeSpeed += 0.5f;
        StaticData.RangeDamage += 15;
        StaticData.RangeHP += 25;
        StaticData.RangeSpeed += 0.5f;

        SceneManager.LoadScene(1);
    }
}
