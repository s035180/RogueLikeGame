using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject mainUI;
    public GameObject scoreboardUI;
    public GameObject userDataUI;
    public GameObject achivementUI;

    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    public TMP_Text warningRegisterText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            /*Debug.Log("Instance already exists, destroying object!");
           Destroy(this);*/

            showMain();
        }
        DontDestroyOnLoad(this.gameObject);

    }

    //Functions to change the login screen UI
    public void LoginScreen() //Back button
    {
        loginUI.SetActive(true);
        registerUI.SetActive(false);
        mainUI.SetActive(false);

        warningRegisterText.text = "";
    }
    public void RegisterScreen() // Register button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);

        warningLoginText.text = "";
        confirmLoginText.text = "";
    }

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
    public void showAchievement()
    {
        Debug.Log("Achievement");
        mainUI.SetActive(false);
        achivementUI.SetActive(true);
    }

    public void showUserData()
    {
        Debug.Log("User Data");
        mainUI.SetActive(false);
        userDataUI.SetActive(true);
    }

    public void showMain()
    {
        Debug.Log("Main Menu");
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        userDataUI.SetActive(false);
        scoreboardUI.SetActive(false);
        achivementUI.SetActive(false);
        mainUI.SetActive(true);
    }
}
