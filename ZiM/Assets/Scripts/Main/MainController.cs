using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public int numZombies;
    public float mansionSizeX;
    public ulong secsForNewZombies;
    public ulong lastTimeRecZombs;
    public ulong invasionRestSecs;
    public ulong collectRestSecs;
    public GameObject leaderInPrefab;
    public GameObject zombieInPrefab;       //Gameobject to be able to create the Zombies.
    public GameObject dogInPrefab;
    public GameObject catInPrefab;
    public GameObject raccoonInPrefab;
    public GameObject recycleBin;
    public GameObject speaker;
    public GameObject UIPrefab;             //Create the Game Object that shows the UI.
    public GameObject invasionResultWin;
    public GameObject collectResultWin;

    float height;
    float width;

    GameObject invWin;
    GameObject collWin;

    public enum CityType       //List of citys.
    {
        Mexico,
        NewYork,
        Tokio
    }

    CityType thisCity;

    void Awake()
    {
        numZombies = GameController.qZombies;
        mansionSizeX = GameController.mansionSize;
        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        if (GameController.lastTimeRecZombs == 0)
        {
            GameController.lastTimeRecZombs = (ulong)DateTime.Now.Ticks;
        }

        lastTimeRecZombs = GameController.lastTimeRecZombs;
        Debug.Log(lastTimeRecZombs);

    }

    void Start()
    {
        GameObject UIShow = Instantiate(UIPrefab, new Vector2(0, 0), Quaternion.identity);
        if(GameController.zombieDog == true)
        {
            CreateDog();
        }

        if (GameController.zombieCat == true)
        {
            CreateCat();
        }

        if (GameController.zombieRaccoon == true)
        {
            CreateRaccoon();
        }

        if(GameController.recycleBin == true)
        {
            recycleBin.gameObject.SetActive(true);
        }

        if (GameController.speaker == true)
        {
            speaker.gameObject.SetActive(true);
        }


        for (int zom = numZombies; zom > 0; zom--)
        {
            CreateZombie();
        }
        CreateLeader();
    }

    void Update()
    {
        if(secsForNewZombies * 10000000 + lastTimeRecZombs < (ulong)DateTime.Now.Ticks)
        {
            ulong arrZombs = 2;
            if(GameController.speaker == true)
            {
                arrZombs = 3;
            }
            ulong numOfZomArri = (((ulong)DateTime.Now.Ticks - lastTimeRecZombs) / (secsForNewZombies * 10000000)) * arrZombs;
            Debug.Log(numOfZomArri);
            GameController.qZombies += (int)numOfZomArri;
            if(GameController.qZombies > 50)
            {
                GameController.qZombies = 50;
            }
            lastTimeRecZombs = (ulong)DateTime.Now.Ticks;
            GameController.lastTimeRecZombs = (ulong)DateTime.Now.Ticks;
        }
        checkInvadedCities();
        checkCollectedCities();
    }

    public void CreateZombie()
    {
        float setPositionX = UnityEngine.Random.Range(Camera.main.transform.position.x - (width / 2) + 2f, Camera.main.transform.position.x + mansionSizeX - 2f - width / 2);
        float setPositionY = UnityEngine.Random.Range(-1f, -(height / 2) + 1f);
        Vector2 origin = new Vector2(setPositionX, setPositionY);
        GameObject newZombie = Instantiate(zombieInPrefab, origin, Quaternion.identity);
    }

    void CreateLeader()
    {
        float setPositionX = UnityEngine.Random.Range(Camera.main.transform.position.x - (width / 2) + 2f, Camera.main.transform.position.x + mansionSizeX - 2f - width / 2);
        float setPositionY = UnityEngine.Random.Range(-1f, -(height / 2) + 1f);
        Vector2 origin = new Vector2(setPositionX, setPositionY);
        GameObject newLeader = Instantiate(leaderInPrefab, origin, Quaternion.identity);
    }

    void CreateDog()
    {
        float setPositionX = UnityEngine.Random.Range(Camera.main.transform.position.x - (width / 2) + 2f, Camera.main.transform.position.x + mansionSizeX - 2f - width / 2);
        float setPositionY = UnityEngine.Random.Range(-1f, -(height / 2) + 1f);
        Vector2 origin = new Vector2(setPositionX, setPositionY);
        GameObject newDog = Instantiate(dogInPrefab, origin, Quaternion.identity);
    }

    void CreateCat()
    {
        float setPositionX = UnityEngine.Random.Range(Camera.main.transform.position.x - (width / 2) + 2f, Camera.main.transform.position.x + mansionSizeX - 2f - width / 2);
        float setPositionY = UnityEngine.Random.Range(-1f, -(height / 2) + 1f);
        Vector2 origin = new Vector2(setPositionX, setPositionY);
        GameObject newZombie = Instantiate(catInPrefab, origin, Quaternion.identity);
    }

    void CreateRaccoon()
    {
        float setPositionX = UnityEngine.Random.Range(Camera.main.transform.position.x - (width / 2) + 2f, Camera.main.transform.position.x + mansionSizeX - 2f - width / 2);
        float setPositionY = UnityEngine.Random.Range(-1f, -(height / 2) + 1f);
        Vector2 origin = new Vector2(setPositionX, setPositionY);
        GameObject newZombie = Instantiate(raccoonInPrefab, origin, Quaternion.identity);
    }

    void checkInvadedCities()
    {
        GameObject confWin = GameObject.FindGameObjectWithTag("Window");
        if (confWin)
        {

        }
        else
        {
            for (int i = 0; i < GameController.numberOfCities; i++)
            {
                if (GameController.cityInvaded[i] == true)
                {
                    string cityName = "";
                    thisCity = (CityType)i;
                    switch (thisCity)
                    {
                        case CityType.Mexico:
                            cityName = "Mexico";
                            break;
                        case CityType.NewYork:
                            cityName = "New York";
                            break;
                        case CityType.Tokio:
                            cityName = "Tokio";
                            break;
                    }

                    if (collectRestSecs * 10000000 + GameController.invasionTime[i] < (ulong)DateTime.Now.Ticks)
                    {
                        float multiplier = UnityEngine.Random.Range(1.2f, 1.5f);
                        int newZombies = Mathf.RoundToInt(GameController.zombsSentToInvade[i] * multiplier);
                        GameController.qZombies += (GameController.zombsSentToInvade[i] + newZombies);
                        invWin = Instantiate(invasionResultWin, new Vector2(0, 0), Quaternion.identity);
                        if (GameController.qZombies > GameController.maxZombies)
                        {
                            GameController.qZombies = GameController.maxZombies;
                        }
                        invWin.GetComponent<InvasionReturned>().cityName = cityName;
                        invWin.GetComponent<InvasionReturned>().transformedZombies = newZombies;
                        invWin.GetComponent<InvasionReturned>().sentZombies = GameController.zombsSentToInvade[i];
                        invWin.GetComponent<InvasionReturned>().totalZombies = GameController.qZombies;
                        GameController.cityInvaded[i] = false;
                    }
                }
            }
        }
    }

    void checkCollectedCities()
    {
        GameObject confWin = GameObject.FindGameObjectWithTag("Window");
        if (confWin)
        {

        }
        else
        {
            for (int i = 0; i < GameController.numberOfCities; i++)
            {
                if (GameController.cityCollecting[i] == true)
                {
                    string cityName = "";
                    thisCity = (CityType)i;
                    switch (thisCity)
                    {
                        case CityType.Mexico:
                            cityName = "Mexico";
                            break;
                        case CityType.NewYork:
                            cityName = "New York";
                            break;
                        case CityType.Tokio:
                            cityName = "Tokio";
                            break;
                    }

                    if (collectRestSecs * 10000000 + GameController.collectTime[i] < (ulong)DateTime.Now.Ticks)
                    {
                        collWin = Instantiate(collectResultWin, new Vector2(0, 0), Quaternion.identity);
                        collWin.GetComponent<CollectReturned>().cityIndex = i;
                        collWin.GetComponent<CollectReturned>().cityName = cityName;
                    }
                }
            }
        }
    }
}
