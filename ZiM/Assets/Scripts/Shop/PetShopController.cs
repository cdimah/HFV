using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetShopController : MonoBehaviour
{
    public int boughtPet;
    public int pricePet;
    public Text warningText;
    public Button zDogButton;
    public Button zCatButton;
    public Button zRaccoonButton;
    public Button acceptButton;
    public Button cancelButton;
    public Text descriptionText;
    public Text confirmationText;
    public GameObject confirmationWindow;

    void Start()
    {
        zDogButton.onClick.AddListener(ZDBought);
        zCatButton.onClick.AddListener(ZCBought);
        zRaccoonButton.onClick.AddListener(ZRBought);
        acceptButton.onClick.AddListener(Accept);
        cancelButton.onClick.AddListener(Cancel);
    }

    void ZDBought()
    {
        boughtPet = 0;
        pricePet = 2500;
        if (GameController.zombieDog == false)
        {
            if (GameController.currency >= pricePet)
            {
                descriptionText.text = "Don't buy, adopt (The money is for expenses =p)! If you own a Zombie Dog, it will follow the Zombie Leader anywhere!";
                confirmationText.text = "Sure you want to buy for " + pricePet + " coins?";
                confirmationWindow.SetActive(true);
            }
            else
            {
                string message = "You don´t have enough coins.";
                StartCoroutine(ShowMessage(message));
            }
        }
        else
        {
            string message = "You already have this object.";
            StartCoroutine(ShowMessage(message));
        }
    }

    void ZCBought()
    {
        boughtPet = 1;
        pricePet = 2500;
        if (GameController.zombieCat == false)
        {
            if (GameController.currency >= pricePet)
            {
                descriptionText.text = "An independant Zombie Cat that will be part of any invasion you sent and help turning Zombies!";
                confirmationText.text = "Sure you want to buy for " + pricePet + " coins?";
                confirmationWindow.SetActive(true);
            }
            else
            {
                string message = "You don´t have enough coins.";
                StartCoroutine(ShowMessage(message));
            }
        }
        else
        {
            string message = "You already have this object.";
            StartCoroutine(ShowMessage(message));
        }
    }

    void ZRBought()
    {
        boughtPet = 2;
        pricePet = 2500;
        if (GameController.zombieRaccoon == false)
        {
            if (GameController.currency >= pricePet)
            {
                descriptionText.text = "A Zombie Raccoon? Yes and his name is Ziri... The Raccoon Ziri! Ziri will bring a present on collecting activities.";
                confirmationText.text = "Sure you want to buy for " + pricePet + " coins?";
                confirmationWindow.SetActive(true);
            }
            else
            {
                string message = "You don´t have enough coins.";
                StartCoroutine(ShowMessage(message));
            }
        }
        else
        {
            string message = "You already have this object.";
            StartCoroutine(ShowMessage(message));
        }
    }

    void Accept()
    {
        if (boughtPet == 0)
        {
            GameController.zombieDog = true;
        }
        else if (boughtPet == 1)
        {
            GameController.zombieCat = true;
        }
        else if (boughtPet == 2)
        {
            GameController.zombieRaccoon = true;
        }
        GameController.currency -= pricePet;
        confirmationWindow.SetActive(false);
    }

    void Cancel()
    {
        confirmationWindow.SetActive(false);
    }

    IEnumerator ShowMessage(string msg)
    {
        warningText.text = msg;
        warningText.enabled = true;
        yield return new WaitForSeconds(3f);
        warningText.enabled = false;
    }
}
