using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController
{
    public static int qZombies = 20;
    public static int maxZombies = 50;
    public static int currency = 0;

    //Variables to handle Main Scene.
    public static int mansionSize = 30;

    //Variables to handle Map Scene Invasion.
    public static ulong invTimeMexico;
    public static ulong invTimeNewYork;
    public static ulong invTimeTokio;
    public static bool mexicoInvaded = false;
    public static bool newYorkInvaded = false;
    public static bool tokioInvaded = false;
    public static int zombSentInvMexico;
    public static int zombSentInvNewYork;
    public static int zombSentInvTokio;

    //Variables to handle Map Scene Collect.
    public static string collectedCity;
    public static ulong collTimeMexico;
    public static ulong collTimeNewYork;
    public static ulong collTimeTokio;
    public static bool mexicoCollecting = false;
    public static bool newYorkCollecting = false;
    public static bool tokioCollecting = false;
    public static int zombSentCollMexico;
    public static int zombSentCollNewYork;
    public static int zombSentCollTokio;

    //Variables to handle Map Scene Minigame.
    public static bool anotherWindow;
    public static ulong minigameRestSecs = 180;
    public static ulong restTimeLeader = 637456487006406610;


    //Variables to create the Minigame Scene.
    public static string cityName;
    public static int zombToSend;
    public static int numBystanders;
    public static float citySize;

    //Variables to make the items work.
    public static int itemsPerCity = 5;

    //Items found.
    public static bool[] mexicoItems = new bool[5];
    public static bool[] newYorkItems = new bool[5];
    public static bool[] tokioItems = new bool[5];

}
