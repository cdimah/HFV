using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class MapController : MonoBehaviour
{
    public ulong invasionRestSecs;
    public GameObject UIPrefab;
    public GameObject invasionResultWin;

    GameObject invWin;
    [SerializeField] TMP_Text mexicoCountdown;
    [SerializeField] TMP_Text newYorkCountdown;
    [SerializeField] TMP_Text tokioCountdown;


    void Start()
    {
        GameObject UIShow = Instantiate(UIPrefab, new Vector2(0, 0), Quaternion.identity);
    }

    void Update()
    {
        checkInvadedCities();
    }

    void checkInvadedCities()
    {
        
        if (GameController.anotherWindow == false)
        {
            if (GameController.mexicoInvaded == true)
            {
                mexicoCountdown.enabled = true;
                if (invasionRestSecs * 10000000 + GameController.invTimeMexico < (ulong)DateTime.Now.Ticks)
                {
                    if (invWin)
                    {
                        
                    }
                    else
                    {
                        float multiplier = UnityEngine.Random.Range(1.2f, 1.5f);
                        int newZombies = Mathf.RoundToInt(GameController.zombSentInvMexico * multiplier);
                        GameController.qZombies += (GameController.zombSentInvMexico + newZombies);
                        invWin = Instantiate(invasionResultWin, new Vector2(0, 0), Quaternion.identity);
                        if (GameController.qZombies > GameController.maxZombies)
                        {
                            GameController.qZombies = GameController.maxZombies;
                        }
                        invWin.GetComponent<InvasionReturned>().cityName = "Mexico";
                        invWin.GetComponent<InvasionReturned>().transformedZombies = newZombies;
                        invWin.GetComponent<InvasionReturned>().sentZombies = GameController.zombSentInvMexico;
                        invWin.GetComponent<InvasionReturned>().totalZombies = GameController.qZombies;
                        GameController.mexicoInvaded = false;
                        mexicoCountdown.enabled = false;
                    }
                }
                else
                {
                    ulong dif = (invasionRestSecs * 10000000 + GameController.invTimeMexico - (ulong)DateTime.Now.Ticks) / 10000000;
                    int seconds = ((int)dif % 60);
                    dif -= dif % 60;
                    int minutes = ((int)(dif % 3600) / 60);
                    dif -= (dif % 3600) / 60;
                    int hours = ((int)dif / 3600);
                    string t = "" + hours + "h " + minutes + "m " + seconds + "s.";
                    mexicoCountdown.text = t;
                }
            }

            if (GameController.newYorkInvaded == true)
            {
                newYorkCountdown.enabled = true;
                if (invasionRestSecs * 10000000 + GameController.invTimeNewYork < (ulong)DateTime.Now.Ticks)
                {
                    if(invWin)
                    {

                    }
                    else
                    {
                        float multiplier = UnityEngine.Random.Range(1.2f, 1.5f);
                        int newZombies = Mathf.RoundToInt(GameController.zombSentInvNewYork * multiplier);
                        GameController.qZombies += (GameController.zombSentInvNewYork + newZombies);
                        invWin = Instantiate(invasionResultWin, new Vector2(0, 0), Quaternion.identity);
                        if (GameController.qZombies > GameController.maxZombies)
                        {
                            GameController.qZombies = GameController.maxZombies;
                        }
                        invWin.GetComponent<InvasionReturned>().cityName = "New York";
                        invWin.GetComponent<InvasionReturned>().transformedZombies = newZombies;
                        invWin.GetComponent<InvasionReturned>().sentZombies = GameController.zombSentInvNewYork;
                        invWin.GetComponent<InvasionReturned>().totalZombies = GameController.qZombies;
                        GameController.newYorkInvaded = false;
                        newYorkCountdown.enabled = false;
                    }
                }
                    
                else
                {
                    ulong dif = (invasionRestSecs * 10000000 + GameController.invTimeNewYork - (ulong)DateTime.Now.Ticks) / 10000000;
                    int seconds = ((int)dif % 60);
                    dif -= dif % 60;
                    int minutes = ((int)(dif % 3600) / 60);
                    dif -= (dif % 3600) / 60;
                    int hours = ((int)dif / 3600);
                    string t = "" + hours + "h " + minutes + "m " + seconds + "s.";
                    newYorkCountdown.text = t;
                }
            }

            if (GameController.tokioInvaded == true)
            {
                tokioCountdown.enabled = true;
                if (invasionRestSecs * 10000000 + GameController.invTimeTokio < (ulong)DateTime.Now.Ticks)
                {
                    if (invWin)
                    {

                    }
                    else
                    {
                        float multiplier = UnityEngine.Random.Range(1.2f, 1.5f);
                        int newZombies = Mathf.RoundToInt(GameController.zombSentInvTokio * multiplier);
                        GameController.qZombies += (GameController.zombSentInvTokio + newZombies);
                        invWin = Instantiate(invasionResultWin, new Vector2(0, 0), Quaternion.identity);
                        if (GameController.qZombies > GameController.maxZombies)
                        {
                            GameController.qZombies = GameController.maxZombies;
                        }
                        invWin.GetComponent<InvasionReturned>().cityName = "Tokio";
                        invWin.GetComponent<InvasionReturned>().transformedZombies = newZombies;
                        invWin.GetComponent<InvasionReturned>().sentZombies = GameController.zombSentInvTokio;
                        invWin.GetComponent<InvasionReturned>().totalZombies = GameController.qZombies;
                        GameController.tokioInvaded = false;
                        tokioCountdown.enabled = false;
                    }
                }
                else
                {
                    ulong dif = (invasionRestSecs * 10000000 + GameController.invTimeTokio - (ulong)DateTime.Now.Ticks) / 10000000;
                    int seconds = ((int)dif % 60);
                    dif -= dif % 60;
                    int minutes = ((int)(dif % 3600) / 60);
                    dif -= (dif % 3600) / 60;
                    int hours = ((int)dif / 3600);
                    string t = "" + hours + "h " + minutes + "m " + seconds + "s.";
                    tokioCountdown.text = t;
                }
            }
        }
    }
}
