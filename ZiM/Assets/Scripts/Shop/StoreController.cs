using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    public Button objectsButton;
    public Button petsButton;
    public Button clothesButton;
    public Button moneyButton;
    public Button closeButton;
    public GameObject objectsView;
    public GameObject petsView;
    public GameObject clothesView;
    public GameObject moneyView;
    public Image selectedShop;
    public Image window2;

    GameObject selectedView;


    void Start()
    {
        objectsButton.onClick.AddListener(ObjectsSelected);
        petsButton.onClick.AddListener(PetsSelected);
        clothesButton.onClick.AddListener(ClothesSelected);
        moneyButton.onClick.AddListener(MoneySelected);
        closeButton.onClick.AddListener(Close);
        petsView.SetActive(false);
        clothesView.SetActive(false);
        moneyView.SetActive(false);
        
    }

    void ObjectsSelected()
    {
        petsView.SetActive(false);
        clothesView.SetActive(false);
        moneyView.SetActive(false);
        objectsView.SetActive(true);
        selectedShop.transform.position = objectsButton.transform.position;
    }

    void PetsSelected()
    {
        objectsView.SetActive(false);
        clothesView.SetActive(false);
        moneyView.SetActive(false);
        petsView.SetActive(true);
        selectedShop.transform.position = petsButton.transform.position;

    }

    void ClothesSelected()
    {
        objectsView.SetActive(false);
        petsView.SetActive(false);
        moneyView.SetActive(false);
        clothesView.SetActive(true);
        selectedShop.transform.position = clothesButton.transform.position;
    }

    void MoneySelected()
    {
        objectsView.SetActive(false);
        petsView.SetActive(false);
        clothesView.SetActive(false);
        moneyView.SetActive(true);
        selectedShop.transform.position = moneyButton.transform.position;
    }

    void Close()
        {
        Destroy(gameObject);
        }
}
