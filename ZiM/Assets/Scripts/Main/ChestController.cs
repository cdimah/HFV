using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestController : MonoBehaviour
{
    public Button closeButton;
    public Button energyDrinkButton;
    public Button rentVHSButton;
    public Button pizzaButton;
    public Button blackCoffeButton;
    public Button fashionMagazineButton;
    public Button geekGlassesButton;
    public Text energyDrinkText;
    public Text rentVHSText;
    public Text pizzaText;
    public Text blackCoffeText;
    public Text fashionMagazineText;
    public Text geekGlassesText;
    public Text warningText;
    public GameObject confirmationWindow;

    int energyDrinkInv;
    int rentVHSInv;
    int pizzaInv;
    int blackCoffeInv;
    int fashionMagazineInv;
    int geekGlassesInv;

    void Start()
    {
        gameObject.name = "InventoryWindow";
        closeButton.onClick.AddListener(Close);
        energyDrinkButton.onClick.AddListener(EDButton);
        rentVHSButton.onClick.AddListener(RVButton);
        pizzaButton.onClick.AddListener(PButton);
        blackCoffeButton.onClick.AddListener(BCButton);
        fashionMagazineButton.onClick.AddListener(FMButton);
        geekGlassesButton.onClick.AddListener(GGButton);
        energyDrinkInv = GameController.energyDrink;
        rentVHSInv = GameController.rentedVHS;
        pizzaInv = GameController.pizza;
        blackCoffeInv = GameController.blackCoffee;
        fashionMagazineInv = GameController.fashionMagazine;
        geekGlassesInv = GameController.geekGlasses;
        energyDrinkText.text = "x " + energyDrinkInv;
        rentVHSText.text = "x " + rentVHSInv;
        pizzaText.text = "x " + pizzaInv;
        blackCoffeText.text = "x " + blackCoffeInv;
        fashionMagazineText.text = "x " + fashionMagazineInv;
        geekGlassesText.text = "x " + geekGlassesInv;
    }

    void Close()
    {
        Destroy(gameObject);
    }

    void EDButton()
    {
        if(energyDrinkInv == 0)
        {
            string wText = "You don't have any Energy Drink Left =(.";
            StartCoroutine(ShowMessage(wText));
        }
        else if (GameController.minigameRestSecs * 10000000 + GameController.restTimeLeader < (ulong)DateTime.Now.Ticks)
        {
            string wText = "You don´t need that right now. Your leader is rested =).";
            StartCoroutine(ShowMessage(wText));
        }
        else
        {
            GameObject confWin = Instantiate(confirmationWindow, gameObject.transform.position, Quaternion.identity);
            confWin.GetComponent<InvConfWindowManager>().selectedObject = 0;
        }

    }

    void RVButton()
    {   
        if (rentVHSInv == 0)
        {
            string wText = "You don't have any Rented VHS Left =(.";
            StartCoroutine(ShowMessage(wText));
        }
        else if(GameController.qZombies >= (GameController.maxZombies - 1))
        {
            string wText = "You don´t have room for 2 more zombies.";
            StartCoroutine(ShowMessage(wText));
        }
        else
        {
            GameObject confWin = Instantiate(confirmationWindow, gameObject.transform.position, Quaternion.identity);
            confWin.GetComponent<InvConfWindowManager>().selectedObject = 1;
        }
    }

    void PButton()
    {
        bool cityInvaded = false;
        for (int i = 0; i < GameController.numberOfCities; i++)
        {
            if (GameController.cityInvaded[i] == true)
            {
                cityInvaded = true;
            }
        }
        if (pizzaInv == 0)
        {
            string wText = "You don't have any Pizza Left =(.";
            StartCoroutine(ShowMessage(wText));
        }
        else if (cityInvaded == false)
        {
                string wText = "You don't have zombies invading. Better save this for an another opportunity.";
                StartCoroutine(ShowMessage(wText));
        }
        else
        {
            GameObject confWin = Instantiate(confirmationWindow, gameObject.transform.position, Quaternion.identity);
            confWin.GetComponent<InvConfWindowManager>().selectedObject = 2;
        }
    }

    void BCButton()
    {
        bool cityCollected = false;
        for (int i = 0; i < GameController.numberOfCities; i++)
        {
            if (GameController.cityCollecting[i] == true)
            {
                cityCollected = true;
            }
        }
        if (blackCoffeInv == 0)
        {
            string wText = "You don't have any more Black Coffee Left =(.";
            StartCoroutine(ShowMessage(wText));
        }
        else if (cityCollected == false)
        {
            string wText = "You don't have Zombies collecting. Better save this for an another opportunity.";
            StartCoroutine(ShowMessage(wText));
        }
        else
        {
            GameObject confWin = Instantiate(confirmationWindow, gameObject.transform.position, Quaternion.identity);
            confWin.GetComponent<InvConfWindowManager>().selectedObject = 3;
        }
    }

    void FMButton()
    {
        if (fashionMagazineInv == 0)
        {
            string wText = "You don't have any Fashion Magazine Left =(.";
            StartCoroutine(ShowMessage(wText));
        }
        else if(GameController.fashionMagazineUsed == true)
        {
            string wText = "Your Zombies already read this Magazine. Use another after you send some Zombies to collect.";
            StartCoroutine(ShowMessage(wText));
        }
        else
        {
            GameObject confWin = Instantiate(confirmationWindow, gameObject.transform.position, Quaternion.identity);
            confWin.GetComponent<InvConfWindowManager>().selectedObject = 4;
        }
    }

    void GGButton()
    {
        if (geekGlassesInv == 0)
        {
            string wText = "You don't have any Geek Glases Left =(.";
            ShowMessage(wText);
        }
        else if (GameController.geekGlassesUsed == true)
        {
            string wText = "Your Zombies already have their glasses ready. Try using them again after you send some to collect.";
            StartCoroutine(ShowMessage(wText));
        }
        else
        {
            GameObject confWin = Instantiate(confirmationWindow, gameObject.transform.position, Quaternion.identity);
            confWin.GetComponent<InvConfWindowManager>().selectedObject = 5;
        }
    }

    IEnumerator ShowMessage(string msg)
    {
        warningText.text = msg;
        warningText.enabled = true;
        yield return new WaitForSeconds(3f);
        warningText.enabled = false;
    }
}
