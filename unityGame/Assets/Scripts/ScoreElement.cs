using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{

    public TMP_Text usernameText;
    public TMP_Text killsText;
    public TMP_Text deathsText;
    public TMP_Text scoreText;

    public void NewScoreElement(string _username, string _kills, string _deaths, string _score)
    {
        usernameText.text = _username;
        killsText.text = _kills.ToString();
        deathsText.text = _deaths.ToString();
        scoreText.text = _score.ToString();
    }

}
