using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyShopController : MonoBehaviour
{
    public int boughtMoney;
    public Text confText;
    public Button currency200Button;
    public Button currency500Button;
    public Button currency1000Button;
    public Button currency4500Button;
    public Button currency10000Button;
    public Button acceptButton;
    public Button cancelButton;
    public GameObject confirmationWindow;

    void Start()
    {
        currency200Button.onClick.AddListener(Bought200C);
        currency500Button.onClick.AddListener(Bought500C);
        currency1000Button.onClick.AddListener(Bought1000C);
        currency4500Button.onClick.AddListener(Bought4500C);
        currency10000Button.onClick.AddListener(Bought10000C);
        acceptButton.onClick.AddListener(Accept);
        cancelButton.onClick.AddListener(Cancel);
    }

    void Bought200C()
    {
        boughtMoney = 200;
        confWinShow(boughtMoney);
    }

    void Bought500C()
    {
        boughtMoney = 500;
        confWinShow(boughtMoney);
    }

    void Bought1000C()
    {
        boughtMoney = 1000;
        confWinShow(boughtMoney);
    }

    void Bought4500C()
    {
        boughtMoney = 4500;
        confWinShow(boughtMoney);
    }

    void Bought10000C()
    {
        boughtMoney = 10000;
        confWinShow(boughtMoney);
    }

    void confWinShow(int bought)
    {
        confText.text = "Are you sure you want to buy " + bought + " Coins?";
        confirmationWindow.SetActive(true);
    }

    void Accept()
    {
        GameController.currency += boughtMoney;
        confirmationWindow.SetActive(false);
    }

    void Cancel()
    {
        confirmationWindow.SetActive(false);
    }

}
