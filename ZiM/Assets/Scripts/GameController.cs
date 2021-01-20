using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController
{
    public static int qZombies = 20;
    public static int maxZombies = 50;

    //Variables to handle Main Scene.
    public static int mansionSize = 30;

    //Variables to handle Map Scene.
    public static ulong invTimeMexico;
    public static ulong invTimeNewYork;
    public static ulong invTimeTokio;
    public static bool mexicoInvaded = false;
    public static bool newYorkInvaded = false;
    public static bool tokioInvaded = false;
    public static bool anotherWindow;
    public static int zombSentInvMexico;
    public static int zombSentInvNewYork;
    public static int zombSentInvTokio;
    public static int zombSentCollMexico;
    public static int zombSentCollNewYork;
    public static int zombSentCollTokio;
    public static ulong minigameRestSecs = 180;
    public static ulong restTimeLeader = 637456487006406610;
    public static ulong collTimeMexico;
    public static ulong collTimeNewYork;
    public static ulong collTimeTokio;

    //Variables to create the Minigame Scene.
    public static string cityName;
    public static int zombToSend;
    public static int numBystanders;
    public static float citySize;
}
