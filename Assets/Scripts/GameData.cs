using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static int modeChoice; //1: chose campaign | -1: chose butchery
    private static int lvlChoice; //for quantity of lives on lvl
    private static bool openLvls = false; //for open lvlButtons after "Ok" in campaign
    private static bool pauseAndResume = false; //for pause and resume game

    public static int ModeChoice { get { return modeChoice; } set { modeChoice = value; } }
    public static int LvlChoice { get { return lvlChoice; } set { lvlChoice = value; } }
    public static bool OpenLvls { get { return openLvls; } set { openLvls = value; } }
    public static bool PauseAndResume { get { return pauseAndResume; } set { pauseAndResume = value; } }
}
