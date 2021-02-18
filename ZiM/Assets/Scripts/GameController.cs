using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController
{
    public static int qZombies = 15;
    public static int maxZombies = 50;
    public static int currency = 200;
    public static ulong lastTimeRecZombs = 0;

    //Variables to handle Main Scene.
    public static int mansionSize = 30;

    //Variables to handle Map Scene Invasion.
    public static ulong[] invasionTime = new ulong[3];
    public static bool[] cityInvaded = new bool[3];
    public static int[] zombsSentToInvade = new int[3];


    //Variables to handle Map Scene Collect.

    public static ulong[] collectTime = new ulong[3];
    public static bool[] cityCollecting = new bool[3];
    public static int collectedCity;
    public static int[] zombsSentToCollect = new int[3];
    public static bool fashionMagazineUsed;
    public static bool[] fashionMagazineCity = new bool[3];
    public static bool geekGlassesUsed;
    public static bool[] geekGlassesCity = new bool[3];

    //Variables to handle Map Scene Minigame.
    public static bool anotherWindow;
    public static ulong minigameRestSecs = 180;
    public static ulong restTimeLeader = 637456487006406610;


    //Variables to create the Minigame Scene.
    public static string cityName;
    public static int zombToSend;
    public static int numBystanders;
    public static float citySize;

    //Variables to make the collectibles work.
    public static int numberOfCities = 3;
    public static int collectiblesPerCity = 5;

    //Items found and cities.
    public static bool[,] collectibles = new bool[3, 5];

    //Variables to control items.
    public static bool zombieDog;
    public static bool zombieCat;
    public static bool zombieRaccoon;
    public static bool recycleBin;
    public static bool speaker;
    public static int binCapacity;
    public static int energyDrink;
    public static int rentedVHS;
    public static int pizza;
    public static int blackCoffee;
    public static int fashionMagazine;
    public static int geekGlasses;

    //Clothes variable
    public static int clothesPerOrigin = 3;
    public static bool[,,] clothFound = new bool[4, 3, 3];
    public static float headIndex = -1f;
    public static float bodyIndex = -1f;
    public static float legsIndex = -1f;
    public static GameObject head;
    public static GameObject body;
    public static GameObject legs;
}
