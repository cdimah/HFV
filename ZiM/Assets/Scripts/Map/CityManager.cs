using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    public int cityIndex;
    public int numBystanders;
    public int minZombies;
    public int maxZombies;
    public float citySize;
    Collider2D coll;           //Colider for interactions.
    public GameObject sceneConfigurator;

    public enum Cities
    {
        Mexico,
        NewYork,
        Tokio
    }

    Cities selectedCity;

    void Start()
    {
        GameController.anotherWindow = false;
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject window = GameObject.FindWithTag("Window");
            if(window)
            {

            }
            else
            {
                if(GameController.cityInvaded[cityIndex] == true || GameController.cityCollecting[cityIndex] == true)
                {
                    
                }
                else
                {
                    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //Register click on player
                    if (coll == Physics2D.OverlapPoint(mousePos))
                    {
                        selectedCity = (Cities)cityIndex;
                        GameController.cityInvaded[cityIndex] = false;
                        configCity();
                    }
                }
            }
        }
    }

    void configCity()
    {
        GameObject sceneConf = Instantiate(sceneConfigurator, new Vector2(0, 0), Quaternion.identity);
        sceneConf.GetComponent<ConfirmationWi>().cityIndex = cityIndex;
        sceneConf.GetComponent<ConfirmationWi>().cityName = this.gameObject.name;
        sceneConf.GetComponent<ConfirmationWi>().numBystanders = numBystanders;
        sceneConf.GetComponent<ConfirmationWi>().minZombies = minZombies;
        sceneConf.GetComponent<ConfirmationWi>().maxZombies = maxZombies;
        sceneConf.GetComponent<ConfirmationWi>().citySize = citySize;
    }
}
