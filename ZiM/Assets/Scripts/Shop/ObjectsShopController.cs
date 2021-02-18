using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsShopController : MonoBehaviour
{
    public int boughtObj;
    public int priceObj;
    public Text warningText;
    public Button energyDrinkButton;
    public Button rentVHSButton;
    public Button pizzaButton;
    public Button blackCoffeButton;
    public Button fashionMagazineButton;
    public Button geekGlassesButton;
    public Button recycleBinButton;
    public Button speakerButton;
    public Button acceptButton;
    public Button cancelButton;
    public Text descriptionText;
    public Text confirmationText;
    public GameObject confirmationWindow;


    void Start()
    {
        energyDrinkButton.onClick.AddListener(EDBought);
        rentVHSButton.onClick.AddListener(RVBought);
        pizzaButton.onClick.AddListener(PBought);
        blackCoffeButton.onClick.AddListener(BCBought);
        fashionMagazineButton.onClick.AddListener(FMBought);
        geekGlassesButton.onClick.AddListener(GGBought);
        recycleBinButton.onClick.AddListener(RBBought);
        speakerButton.onClick.AddListener(SBought);
        acceptButton.onClick.AddListener(Accept);
        cancelButton.onClick.AddListener(Cancel);
    }

    void EDBought()
    {
        warningText.enabled = false;
        boughtObj = 0;
        priceObj = 150;
        if(GameController.currency >= priceObj)
        {
            descriptionText.text = "The Energy Drink will give back all the lost Energy, leaving any strong Leader ready for the action!";
            confirmationText.text = "Sure you want to buy for " + priceObj + " coins?";
            confirmationWindow.SetActive(true);
        }
        else
        {
            string message = "You don´t have enough coins.";
            StartCoroutine(ShowMessage(message));
        }

    }

    void RVBought()
    {
        warningText.enabled = false;
        boughtObj = 1;
        priceObj = 150;
        if (GameController.currency >= priceObj)
        {
            descriptionText.text = "Zombies love a good vintage movie! With this you can atract a couple of wandering Zombies.";
            confirmationText.text = "Sure you want to buy for " + priceObj + " coins?";
            confirmationWindow.SetActive(true);
        }
        else
        {
            string message = "You don´t have enough coins.";
            StartCoroutine(ShowMessage(message));
        }

    }

    void PBought()
    {
        warningText.enabled = false;
        boughtObj = 2;
        priceObj = 200;
        if (GameController.currency >= priceObj)
        {
            descriptionText.text = "It's not brains but Zombies love Pizza too. With this you can get back your Zombies and some more from a City.";
            confirmationText.text = "Sure you want to buy for " + priceObj + " coins?";
            confirmationWindow.SetActive(true);
        }
        else
        {
            string message = "You don´t have enough coins.";
            StartCoroutine(ShowMessage(message));
        }
    }

    void BCBought()
    {
        warningText.enabled = false;
        boughtObj = 3;
        priceObj = 200;
        if (GameController.currency >= priceObj)
        {
            descriptionText.text = "Hyperactive Zombies find presents faster! You can almost say it's intstantaneous.";
            confirmationText.text = "Sure you want to buy for " + priceObj + " coins?";
            confirmationWindow.SetActive(true);
        }
        else
        {
            string message = "You don´t have enough coins.";
            StartCoroutine(ShowMessage(message));
        }

    }

    void FMBought()
    {
        warningText.enabled = false;
        boughtObj = 4;
        priceObj = 200;
        if (GameController.currency >= priceObj)
        {
            descriptionText.text = "Get your Zombies interested in the latest fashion trends with this magazine! Increase the probability to find clothes when collecting.";
            confirmationText.text = "Sure you want to buy for " + priceObj + " coins?";
            confirmationWindow.SetActive(true);
        }
        else
        {
            string message = "You don´t have enough coins.";
            StartCoroutine(ShowMessage(message));
        }

    }

    void GGBought()
    {
        warningText.enabled = false;
        boughtObj = 5;
        priceObj = 200;
        if (GameController.currency >= priceObj)
        {
            descriptionText.text = "This glasses have a lifetime of experience analizing collectibles. Increase the probability to find Collecitbles.";
            confirmationText.text = "Sure you want to buy for " + priceObj + " coins?";
            confirmationWindow.SetActive(true);
        }
        else
        {
            string message = "You don´t have enough coins.";
            StartCoroutine(ShowMessage(message));
        }

    }

    void RBBought()
    {
        warningText.enabled = false;
        boughtObj = 6;
        priceObj = 2000;
        if (GameController.recycleBin == false)
        {
            if (GameController.currency >= priceObj)
            {
                descriptionText.text = "You need this item to become a friend of the envirroment, get some little money from any trash, repeated collectibles and clothes... but mostly de enviroment.";
                confirmationText.text = "Sure you want to buy for " + priceObj + " coins?";
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

    void SBought()
    {
        warningText.enabled = false;
        boughtObj = 7;
        priceObj = 2000;
        if (GameController.speaker == false)
        {
            if (GameController.currency >= priceObj)
            {
                descriptionText.text = "This speaker will attract more zombies automatically. You will get 3 zombies instead of 2 each 30 mins.";
                confirmationText.text = "Sure you want to buy for " + priceObj + " coins?";
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
        if (boughtObj == 0)
        {
            GameController.energyDrink += 1;
        }
        else if (boughtObj == 1)
        {
            GameController.rentedVHS += 1;
        }
        else if (boughtObj == 2)
        {
            GameController.pizza += 1;
        }
        else if (boughtObj == 3)
        {
            GameController.blackCoffee += 1;
        }
        else if (boughtObj == 4)
        {
            GameController.fashionMagazine += 1;
        }
        else if (boughtObj == 5)
        {
            GameController.geekGlasses += 1;
        }
        else if (boughtObj == 6)
        {
            GameController.recycleBin = true;
            GameObject recycleBin = GameObject.Find("RecycleBin");
            recycleBin.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (boughtObj == 7)
        {
            GameController.speaker = true;
            GameObject speaker = GameObject.Find("Speaker");
            speaker.GetComponent<SpriteRenderer>().enabled = true;
        }
        GameController.currency -= priceObj;
        confirmationWindow.SetActive(false);
    }

    void Cancel()
    {
        confirmationWindow.SetActive(false);
    }

    IEnumerator ShowMessage (string msg)
    {
        warningText.text = msg;
        warningText.enabled = true;
        yield return new WaitForSeconds(3f);
        warningText.enabled = false;
    }
}
