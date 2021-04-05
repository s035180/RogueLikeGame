using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;
    // Update is called once per frame
    void FixedUpdate()
    {
        score.text = "Score: " + StaticData.Score;
    }
}
