using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    int numBystanders;
    int minZombies;
    int maxZombies;
    float citySize;
    Collider2D coll;           //Colider for interactions.
    public GameObject sceneConfigurator;

    public enum Cities
    {
        Tokio,
        NewYork,
        Mexico
    }

    Cities selectedCity;

    void Start()
    {
        GameController.anotherWindow = false;
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //Register click on player
        if (Input.GetMouseButtonDown(0))
        {
            if (coll == Physics2D.OverlapPoint(mousePos))
            {
                string city = coll.gameObject.name;
                if (city == "Mexico")
                {
                    selectedCity = Cities.Mexico;
                    if(GameController.mexicoInvaded == false)
                    {
                        configCity();
                    }
                } else if (city == "NewYork")
                {
                    selectedCity = Cities.NewYork;
                    if (GameController.newYorkInvaded == false)
                    {
                        configCity();
                    }
                } else if (city == "Tokio")
                {
                    selectedCity = Cities.Tokio;
                    if (GameController.tokioInvaded == false)
                    {
                        configCity();
                    }
                }
            }
        }
    }

    void configCity()
    {
        switch (selectedCity)
        {
            case Cities.Tokio:
                numBystanders = 20;
                minZombies = 10;
                maxZombies = 15;
                citySize = 50f;
                break;
            case Cities.NewYork:
                numBystanders = 14;
                minZombies = 5;
                maxZombies = 10;
                citySize = 40f;
                break;
            case Cities.Mexico:
                numBystanders = 16;
                minZombies = 8;
                maxZombies = 12;
                citySize = 45f;
                break;
        }

        GameObject sceneConf = Instantiate(sceneConfigurator, new Vector2(0, 0), Quaternion.identity);
        sceneConf.GetComponent<ConfirmationWi>().cityName = this.gameObject.name;
        sceneConf.GetComponent<ConfirmationWi>().numBystanders = numBystanders;
        sceneConf.GetComponent<ConfirmationWi>().minZombies = minZombies;
        sceneConf.GetComponent<ConfirmationWi>().maxZombies = maxZombies;
        sceneConf.GetComponent<ConfirmationWi>().citySize = citySize;
    }
}
