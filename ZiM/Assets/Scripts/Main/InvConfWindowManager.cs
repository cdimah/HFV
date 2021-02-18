using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvConfWindowManager : MonoBehaviour
{
    public int selectedObject;
    public Button AcceptButton;
    public Button CancelButton;
    public Button ConfButton;
    public Text confirmationText;

    void Start()
    {
        AcceptButton.onClick.AddListener(Accept);
        CancelButton.onClick.AddListener(Cancel);
        ConfButton.onClick.AddListener(Cancel);
        switch (selectedObject)
        {
            case 0:
                confirmationText.text = "Are you sure you want to use an Energy Can?";
                break;
            case 1:
                confirmationText.text = "Are you sure you want to use a Rented VHS?";
                break;
            case 2:
                confirmationText.text = "Are you sure you want to use a Pizza?";
                break;
            case 3:
                confirmationText.text = "Are you sure you want to use a Black Coffee?";
                break;
            case 4:
                confirmationText.text = "Are you sure you want to use a Fashion Magazine?";
                break;
            case 5:
                confirmationText.text = "Are you sure you want to use a pair of Geek Glasses?";
                break;
        }

    }

    void Accept()
    {
        GameObject invWin = GameObject.Find("InventoryWindow");
        switch (selectedObject)
        {
            case 0:
                if (GameController.minigameRestSecs * 10000000 + GameController.restTimeLeader < (ulong)DateTime.Now.Ticks)
                {
                    confirmationText.text = "You don't need that now. Your Leader is rested";
                    ConfButton.gameObject.SetActive(true);
                    AcceptButton.gameObject.SetActive(false);
                    CancelButton.gameObject.SetActive(false);
                }
                else
                {
                    confirmationText.text = "Your Leader is now Rested. Go have fun!";
                    GameController.restTimeLeader = 0;
                    GameController.energyDrink -= 1;
                    invWin.GetComponent<ChestController>().energyDrinkText.text = "x " + GameController.energyDrink;
                    ConfButton.gameObject.SetActive(true);
                    AcceptButton.gameObject.SetActive(false);
                    CancelButton.gameObject.SetActive(false);
                }
                break;
            case 1:
                if (GameController.qZombies >= (GameController.maxZombies - 1))
                {
                    confirmationText.text = "You don´t have room for 2 more zombies.";
                    ConfButton.gameObject.SetActive(true);
                    AcceptButton.gameObject.SetActive(false);
                    CancelButton.gameObject.SetActive(false);
                }
                else
                {
                    GameController.qZombies += 2;
                    GameController.rentedVHS -= 1;
                    invWin.GetComponent<ChestController>().rentVHSText.text = "x " + GameController.rentedVHS;
                    confirmationText.text = "2 new Zombies arrived!.";
                    ConfButton.gameObject.SetActive(true);
                    AcceptButton.gameObject.SetActive(false);
                    CancelButton.gameObject.SetActive(false);
                }
                break;
            case 2:
                bool cityInvaded = false;
                for (int i = 0; i < GameController.numberOfCities; i++)
                {
                    if (GameController.cityInvaded[i] == true)
                    {
                        cityInvaded = true;
                    }
                }
                if (cityInvaded == false)
                {
                    confirmationText.text = "You don't have Zombies invading. Better save this for an another opportunity.";
                    ConfButton.gameObject.SetActive(true);
                    AcceptButton.gameObject.SetActive(false);
                    CancelButton.gameObject.SetActive(false);
                }
                else
                {
                    for(int i = 0; i < GameController.numberOfCities; i++)
                    {
                        GameController.invasionTime[i] = 0;
                    }
                    GameController.pizza -= 1;
                    invWin.GetComponent<ChestController>().pizzaText.text = "x " + GameController.pizza;
                    confirmationText.text = "All your Zombies returned from his Invasion and with new friends! Go to the Map to see the results =).";
                    ConfButton.gameObject.SetActive(true);
                    AcceptButton.gameObject.SetActive(false);
                    CancelButton.gameObject.SetActive(false);
                }
                break;
            case 3:
                bool cityCollected = false;
                for (int i = 0; i < GameController.numberOfCities; i++)
                {
                    if (GameController.cityCollecting[i] == true)
                    {
                        cityCollected = true;
                    }
                }
                if (cityCollected == false)
                {
                    confirmationText.text = "You don't have Zombies collecting. Better save this for an another opportunity.";
                    ConfButton.gameObject.SetActive(true);
                    AcceptButton.gameObject.SetActive(false);
                    CancelButton.gameObject.SetActive(false);
                }
                else
                {
                    for(int i = 0; i < GameController.numberOfCities; i++)
                    {
                        GameController.collectTime[i] = 0;
                    }
                    GameController.blackCoffee -= 1;
                    invWin.GetComponent<ChestController>().blackCoffeText.text = "x " + GameController.blackCoffee;
                    confirmationText.text = "All your Zombies finished collecting.";
                    ConfButton.gameObject.SetActive(true);
                    AcceptButton.gameObject.SetActive(false);
                    CancelButton.gameObject.SetActive(false);
                }
                break;
            case 4:
                GameController.fashionMagazineUsed = true;
                GameController.fashionMagazine -= 1;
                invWin.GetComponent<ChestController>().fashionMagazineText.text = "x " + GameController.fashionMagazine;
                confirmationText.text = "Your Zombies have read the fashion magazine and will focus more on getting Clothes during the next collecting trip =).";
                ConfButton.gameObject.SetActive(true);
                AcceptButton.gameObject.SetActive(false);
                CancelButton.gameObject.SetActive(false);
                break;
            case 5:
                GameController.geekGlassesUsed = true;
                GameController.geekGlasses -= 1;
                invWin.GetComponent<ChestController>().geekGlassesText.text = "x " + GameController.geekGlasses;
                confirmationText.text = "Your Zombies have read the fashion magazine and will focus more on getting Collectibles during the next collecting trip =).";
                ConfButton.gameObject.SetActive(true);
                AcceptButton.gameObject.SetActive(false);
                CancelButton.gameObject.SetActive(false);
                break;
        }
    }

    void Cancel()
    {
        Destroy(gameObject);
    }
}
