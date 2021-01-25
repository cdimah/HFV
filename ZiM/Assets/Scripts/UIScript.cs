using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIScript : MonoBehaviour
{
    public Button homeButton;
    public Button menuButton;
    public Text zombiesAvailable;
    public Text leaderAvailable;
    public Text currency;
    public Image leader;
    public Image zombie;
    public Image coin;

    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "MainSc")
        {
            Destroy(homeButton.gameObject);
        }
        else if (sceneName == "MapSc")
        {

        } else if (sceneName == "MinigameSc")
        {
            Destroy(homeButton.gameObject);
            Destroy(zombiesAvailable.gameObject);
            Destroy(leaderAvailable.gameObject);
            Destroy(zombie.gameObject);
            Destroy(leader.gameObject);
            Destroy(currency.gameObject);
            Destroy(coin.gameObject);
        }
        else if (sceneName == "ItemDisplaySc")
        {
            Destroy(menuButton.gameObject);
            Destroy(zombiesAvailable.gameObject);
            Destroy(leaderAvailable.gameObject);
            Destroy(zombie.gameObject);
            Destroy(leader.gameObject);
            Destroy(currency.gameObject);
            Destroy(coin.gameObject);
        }
        else if (sceneName == "CollectSc")
        {
            Destroy(homeButton.gameObject);
            Destroy(menuButton.gameObject);
            Destroy(zombiesAvailable.gameObject);
            Destroy(leaderAvailable.gameObject);
            Destroy(zombie.gameObject);
            Destroy(leader.gameObject);
        } 

    }

    void Start()
    {
        if(homeButton)
        {
            homeButton.onClick.AddListener(goHome);
        }
        if(menuButton)
        {
            menuButton.onClick.AddListener(openMenu);
        }
    }

    void Update()
    {
        if(zombiesAvailable)
        {
            zombiesAvailable.text = "" + GameController.qZombies;
            if (GameController.minigameRestSecs * 10000000 + GameController.restTimeLeader < (ulong)DateTime.Now.Ticks)
            {
                leaderAvailable.text = "Rested";
            }
            else
            {
                ulong dif = (GameController.minigameRestSecs * 10000000 + GameController.restTimeLeader - (ulong)DateTime.Now.Ticks) / 10000000;
                int seconds = ((int)dif % 60);
                dif -= dif % 60;
                int minutes = ((int)(dif % 3600) / 60);
                dif -= (dif % 3600) / 60;
                int hours = ((int)dif / 3600);
                string t = "Leader will be rested in: " + hours + "h " + minutes + "m " + seconds + "s.";
                leaderAvailable.text = t;
            }
        }

        if(coin)
        {
            currency.text = "" + GameController.currency;
        }

    }

    void goHome()
    {
        SceneManager.LoadScene("MainSc");
    }

    void openMenu()
    {
        SceneManager.LoadScene("ItemDisplaySc");
    }
}
