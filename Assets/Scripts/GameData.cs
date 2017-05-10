using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static int score1, score2;
    private static string name1, name2;
    private static bool multiplayer;

    public static bool Multiplayer
    {
        get
        {
            return multiplayer;
        }
        set
        {
            multiplayer = value;
        }
    }


    public static int Score1
    {
        get
        {
            return score1;
        }
        set
        {
            score1 = value;
        }
    }

    public static string Name1
    {
        get
        {
            return name1;
        }
        set
        {
            name1 = value;
        }
    }

    public static int Score2
    {
        get
        {
            return score2;
        }
        set
        {
            score2 = value;
        }
    }

    public static string Name2
    {
        get
        {
            return name2;
        }
        set
        {
            name2 = value;
        }
    }
}
