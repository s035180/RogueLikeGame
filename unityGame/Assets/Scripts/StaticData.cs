using UnityEngine;

public static class StaticData
{
    private static float _meleeSpeed;
    private static float _meleeHP;
    private static int _meleeDamage;

    private static float _rangeSpeed;
    private static float _rangeHP;
    private static int _rangeDamage;

    private static int _score;
    private static int _kills;
    private static int _deaths;

    public static int Score
    {
        get { return _score; }
        set { _score = value; }
    }

    public static int Kills
    {
        get { return _kills; }
        set { _kills = value; }
    }

    public static int Deaths
    {
        get { return _deaths; }
        set { _deaths = value; }
    }

    public static float MeleeSpeed
    {
        get { return _meleeSpeed; }
        set { _meleeSpeed = value; }
    }

    public static float MeleeHP
    {
        get { return _meleeHP; }
        set { _meleeHP = value; }
    }

    public static int MeleeDamage
    {
        get { return _meleeDamage; }
        set { _meleeDamage = value; }
    }

    public static float RangeSpeed
    {
        get { return _rangeSpeed; }
        set { _rangeSpeed = value; }
    }

    public static float RangeHP
    {
        get { return _rangeHP; }
        set { _rangeHP = value; }
    }

    public static int RangeDamage
    {
        get { return _rangeDamage; }
        set { _rangeDamage = value; }
    }

}
        