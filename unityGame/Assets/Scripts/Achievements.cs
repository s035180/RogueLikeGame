using Firebase.Firestore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Achievements : MonoBehaviour
{
    public string achievementName;
    public string achievementType;
    public int value;
    public bool unlocked;
    public void NewAchivElement(string _achivName, string _achivType, int _value, bool _unlocked)
    {
        achievementName = _achivName;
        achievementType = _achivType;
        value = _value;
        unlocked = _unlocked;
    }
}
