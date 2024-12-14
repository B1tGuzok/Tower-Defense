using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static int modeChoice; //1: chose campaign | -1: chose butchery
    private static int lvlChoice; //for quantity of lives

    public static int ModeChoice { get { return modeChoice; } set { modeChoice = value; } }
    public static int LvlChoice { get { return lvlChoice; } set { lvlChoice = value; } }
}
