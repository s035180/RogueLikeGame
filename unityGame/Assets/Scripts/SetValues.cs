using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetValues : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StaticData.MeleeDamage = 15;
        StaticData.MeleeHP = 100;
        StaticData.MeleeSpeed = 3f;
        StaticData.RangeDamage = 30;
        StaticData.RangeHP = 50;
        StaticData.RangeSpeed = 2f;
        StaticData.Score = 0;
    }
}

