using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MapController : MonoBehaviour
{
    public ulong invasionRestSecs;
    public ulong collectRestSecs;
    public GameObject UIPrefab;
    public GameObject invasionResultWin;
    public GameObject collectResultWin;

    GameObject invWin;
    GameObject collWin;
    [SerializeField] TMP_Text mexicoCountdown;
    [SerializeField] TMP_Text newYorkCountdown;
    [SerializeField] TMP_Text tokioCountdown;

    public enum CityType       //List of citys.
    {
        Mexico,
        NewYork,
        Tokio
    }

    CityType thisCity;

    void Start()
    {
        GameObject UIShow = Instantiate(UIPrefab, new Vector2(0, 0), Quaternion.identity);
    }

    void Update()
    {
        checkInvadedCities();
        checkCollectedCities();

    }

    void checkInvadedCities()
    {
        GameObject confWin = GameObject.FindGameObjectWithTag("Window");
        if (confWin)
        {

        }
        else
        {
            for(int i = 0; i < GameController.numberOfCities; i++)
            {
                if(GameController.cityInvaded[i] == true)
                {
                    string cityName = "";
                    TMP_Text cityCountdown = mexicoCountdown;
                    thisCity = (CityType)i;
                    switch (thisCity)
                    {
                        case CityType.Mexico:
                            cityCountdown = mexicoCountdown;
                            cityName = "Mexico";
                            break;
                        case CityType.NewYork:
                            cityCountdown = newYorkCountdown;
                            cityName = "New York";
                            break;
                        case CityType.Tokio:
                            cityCountdown = tokioCountdown;
                            cityName = "Tokio";
                            break;
                    }

                    cityCountdown.enabled = true;

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
                        cityCountdown.enabled = false;
                    }
                    else
                    {
                        ulong dif = (collectRestSecs * 10000000 + GameController.invasionTime[i] - (ulong)DateTime.Now.Ticks) / 10000000;
                        int seconds = ((int)dif % 60);
                        dif -= dif % 60;
                        int minutes = ((int)(dif % 3600) / 60);
                        dif -= (dif % 3600) / 60;
                        int hours = ((int)dif / 3600);
                        string t = "" + hours + "h " + minutes + "m " + seconds + "s.";
                        cityCountdown.text = t;
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
            for(int i = 0; i < GameController.numberOfCities; i++)
            {
                if (GameController.cityCollecting[i] == true)
                {
                    string cityName = "";
                    TMP_Text cityCountdown = mexicoCountdown;
                    thisCity = (CityType)i;
                    switch(thisCity)
                    {
                        case CityType.Mexico:
                            cityCountdown = mexicoCountdown;
                            cityName = "Mexico";
                            break;
                        case CityType.NewYork:
                            cityCountdown = newYorkCountdown;
                            cityName = "New York";
                            break;
                        case CityType.Tokio:
                            cityCountdown = tokioCountdown;
                            cityName = "Tokio";
                            break;
                    }

                    cityCountdown.enabled = true;

                    if (collectRestSecs * 10000000 + GameController.collectTime[i] < (ulong)DateTime.Now.Ticks)
                    {
                        collWin = Instantiate(collectResultWin, new Vector2(0, 0), Quaternion.identity);
                        collWin.GetComponent<CollectReturned>().cityIndex = i;
                        collWin.GetComponent<CollectReturned>().cityName = cityName;
                        cityCountdown.enabled = false;
                    }
                    else
                    {
                        ulong dif = (collectRestSecs * 10000000 + GameController.collectTime[i] - (ulong)DateTime.Now.Ticks) / 10000000;
                        int seconds = ((int)dif % 60);
                        dif -= dif % 60;
                        int minutes = ((int)(dif % 3600) / 60);
                        dif -= (dif % 3600) / 60;
                        int hours = ((int)dif / 3600);
                        string t = "" + hours + "h " + minutes + "m " + seconds + "s.";
                        cityCountdown.text = t;
                    }
                }
            }
        }
    }
}
