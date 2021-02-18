using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationController : MonoBehaviour
{
    public int boughtObject;
    public int price;
    public string description;
    public Text descriptionText;
    public Text confirmationText;
    public Button acceptButton;
    public Button cancelButton;

    void Start()
    {
        descriptionText.text = "" + description;
        confirmationText.text = "Sure you want to buy for " + price + " coins?";
        acceptButton.onClick.AddListener(Accept);
        cancelButton.onClick.AddListener(Cancel);
    }

    void Accept()
    {
        if (boughtObject == 0)
        {
            GameController.energyDrink += 1;
        }
        else if (boughtObject == 1)
        {
            GameController.rentedVHS += 1;
        }
        else if (boughtObject == 2)
        {
            GameController.pizza += 1;
        }
        else if (boughtObject == 3)
        {
            GameController.blackCoffee += 1;
        }
        else if (boughtObject == 4)
        {
            GameController.fashionMagazine += 1;
        }
        else if (boughtObject == 5)
        {
            GameController.geekGlasses += 1;
        }
        else if (boughtObject == 6)
        {
            GameController.recycleBin = true;
            GameObject recycleBin = GameObject.Find("RecycleBin");
            recycleBin.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (boughtObject == 7)
        {
            GameController.speaker = true;
            GameObject speaker = GameObject.Find("Speaker");
            speaker.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (boughtObject == 8)
        {
            GameController.zombieDog = true;
        }
        else if (boughtObject == 9)
        {
            GameController.zombieCat = true;
        }
        else if (boughtObject == 10)
        {
            GameController.zombieRaccoon = true;
        }


        GameController.currency -= price;
        Destroy(gameObject);
    }

    void Cancel()
    {
        gameObject.SetActive(false);
    }

}
