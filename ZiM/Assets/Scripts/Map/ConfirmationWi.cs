using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConfirmationWi : MonoBehaviour
{
    public string cityName;
    public int numBystanders;
    public int minZombies;
    public int maxZombies;
    public float citySize;
    public Button invasionButton;
    public Button collectButton;
    public Button minigameButton;
    public Button closeButton;
    public Button plusButton;
    public Button minusButton;
    public Button acceptButton;
    public Text cityText;
    public Text availableZombiesText;
    public Text minZombText;
    public Text maxZombText;
    public Text bystanderText;
    public Text numberOfZombies;
    public Text leaderStatusText;



    bool invasionSelected = false;
    bool collectSelected = false;
    bool minigameSelected = false;
    int zombsToSend;
    int availableZombies;

    void Awake()
    {
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        transform.localScale = new Vector3(width, height, 1f);
        Vector2 pos = Camera.main.transform.position;
        transform.position = new Vector3(pos.x, pos.y, -1f);
        availableZombies = GameController.qZombies;
        GameController.anotherWindow = true;
        checkMinigameStatus();
    }

    void Start()
    {

        invasionButton.onClick.AddListener(InvasionOn);
        collectButton.onClick.AddListener(CollectOn);
        minigameButton.onClick.AddListener(MinigameOn);
        plusButton.onClick.AddListener(AddZombie);
        minusButton.onClick.AddListener(LessZombie);
        closeButton.onClick.AddListener(CloseWi);
        acceptButton.onClick.AddListener(Accept);
        cityText.text = cityName;
        availableZombiesText.text = "You have " + availableZombies + " avalables";
        minZombText.text = "Required Zombies: " + minZombies;
        maxZombText.text = "Maximum of Zombies: " + maxZombies;
        bystanderText.text = "Number of Humans: " + numBystanders;
        numberOfZombies.text = "" + minZombies;
        zombsToSend = minZombies;
    }
        
    void Update()
    {
        checkMinigameStatus();
    }

    void checkMinigameStatus()
    {
        if (GameController.minigameRestSecs * 10000000 + GameController.restTimeLeader < (ulong)DateTime.Now.Ticks)
        {
            minigameButton.interactable = true;
            leaderStatusText.text = "";

        }
        else
        {
            minigameButton.interactable = false;
            ulong dif = (GameController.minigameRestSecs * 10000000 + GameController.restTimeLeader - (ulong)DateTime.Now.Ticks) / 10000000;
            int seconds = ((int)dif % 60);
            dif -= dif % 60;
            int minutes = ((int)(dif % 3600) / 60);
            dif -= (dif % 3600) / 60;
            int hours = ((int)dif / 3600);
            string t = "Leader will be rested in: " + hours + "h " + minutes + "m " + seconds + "s.";
            leaderStatusText.text = t;
        }
    }

    void CloseWi()
    {
        GameController.anotherWindow = false;
        Destroy(gameObject);
    }

    void InvasionOn()
    { 
        invasionSelected = true;
        collectSelected = false;
        minigameSelected = false;
        ColorBlock iColors = invasionButton.colors;
        iColors.normalColor = Color.red;
        iColors.selectedColor = Color.red;
        invasionButton.GetComponent<Button>().colors = iColors;
        ColorBlock cColors = collectButton.colors;
        cColors.normalColor = Color.white;
        cColors.selectedColor = Color.white;
        collectButton.GetComponent<Button>().colors = cColors;
        ColorBlock mColors = minigameButton.colors;
        mColors.normalColor = Color.white;
        mColors.selectedColor = Color.white;
        minigameButton.GetComponent<Button>().colors = mColors;
        plusButton.interactable = true;
        minusButton.interactable = true;
        acceptButton.interactable = true;
    }

    void CollectOn()
    {
        invasionSelected = false;
        collectSelected = true;
        minigameSelected = false;
        ColorBlock iColors = invasionButton.colors;
        iColors.normalColor = Color.white;
        iColors.selectedColor = Color.white;
        invasionButton.GetComponent<Button>().colors = iColors;
        ColorBlock cColors = collectButton.colors;
        cColors.normalColor = Color.red;
        cColors.selectedColor = Color.red;
        collectButton.GetComponent<Button>().colors = cColors;
        ColorBlock mColors = minigameButton.colors;
        mColors.normalColor = Color.white;
        mColors.selectedColor = Color.white;
        minigameButton.GetComponent<Button>().colors = mColors;
        plusButton.interactable = true;
        minusButton.interactable = true;
        acceptButton.interactable = true;
    }

    void MinigameOn()
    {
        invasionSelected = false;
        collectSelected = false;
        minigameSelected = true;
        ColorBlock iColors = invasionButton.colors;
        iColors.normalColor = Color.white;
        iColors.selectedColor = Color.white;
        invasionButton.GetComponent<Button>().colors = iColors;
        ColorBlock cColors = collectButton.colors;
        cColors.normalColor = Color.white;
        cColors.selectedColor = Color.white;
        collectButton.GetComponent<Button>().colors = cColors;
        ColorBlock mColors = minigameButton.colors;
        mColors.normalColor = Color.red;
        mColors.selectedColor = Color.red;
        minigameButton.GetComponent<Button>().colors = mColors;
        
        plusButton.interactable = true;
        minusButton.interactable = true;
        acceptButton.interactable = true;
    }

    void AddZombie()
    {
        if(zombsToSend >= availableZombies)
        {
            Debug.Log("You don't control enough Zombies");

        } else if(zombsToSend == maxZombies)
        {
            Debug.Log("You reached the maximum number of Zombies");
        } else
        {
            zombsToSend += 1;
        }
        numberOfZombies.text = "" + zombsToSend;
    }

    void LessZombie()
    {
        if (zombsToSend == minZombies)
        {
            Debug.Log("You can´t send less Zombies");
        }
        else
        {
            zombsToSend -= 1;
            numberOfZombies.text = "" + zombsToSend;
        }
    }

    void Accept()
    {
        if(availableZombies >= zombsToSend)
        {
            if (invasionSelected == true)
            {
                if(cityName == "Mexico")
                {
                    GameController.invTimeMexico = (ulong)DateTime.Now.Ticks;
                    GameController.mexicoInvaded = true;
                    GameController.zombSentInvMexico = zombsToSend;
                } else if(cityName == "NewYork")
                {
                    GameController.invTimeNewYork = (ulong)DateTime.Now.Ticks;
                    GameController.newYorkInvaded = true;
                    GameController.zombSentInvNewYork = zombsToSend;
                }
                else if(cityName == "Tokio")
                {
                    GameController.invTimeTokio = (ulong)DateTime.Now.Ticks;
                    GameController.tokioInvaded = true;
                    GameController.zombSentInvTokio = zombsToSend;
                }

                Debug.Log("Send Zombies to invade");
                GameController.qZombies -= zombsToSend;
                GameController.anotherWindow = false;
                Destroy(gameObject);
            }
            else if (collectSelected == true)
            {
                if (cityName == "Mexico")
                {
                    GameController.collTimeMexico = (ulong)DateTime.Now.Ticks;
                }
                else if (cityName == "NewYork")
                {
                    GameController.collTimeNewYork = (ulong)DateTime.Now.Ticks;
                }
                else if (cityName == "Tokio")
                {
                    GameController.collTimeTokio = (ulong)DateTime.Now.Ticks;
                }
                Debug.Log("Send Zombies to collect");
                GameController.qZombies -= zombsToSend;
                //Include time to wait.
                Debug.Log("" + zombsToSend + " left to collect");
                Debug.Log("You have a total of " + GameController.qZombies + " left");

                Destroy(gameObject);
            }
            else if (minigameSelected == true)
            {
                minigameButton.interactable = false;
                GameController.restTimeLeader = (ulong)DateTime.Now.Ticks;
                GameController.qZombies -= zombsToSend;
                GameController.cityName = cityName;
                GameController.zombToSend = zombsToSend;
                GameController.numBystanders = numBystanders;
                GameController.citySize = citySize;
                GameController.anotherWindow = false;
                SceneManager.LoadScene("MinigameSC");
            }
        } else
        {
            Debug.Log("You need to control more zombies");
        }
    }
}
