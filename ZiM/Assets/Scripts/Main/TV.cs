using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : MonoBehaviour
{
    public GameObject TVAddWindow;
    GameObject AddWindow;
    Collider2D coll;

    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //Register click on player
        if (Input.GetMouseButtonDown(0))
        {
            if (coll == Physics2D.OverlapPoint(mousePos))
            {
                if(GameController.qZombies < 49)
                {
                    GameController.qZombies += 2;
                    GameObject mainController = GameObject.Find("MainController");
                    var mainControllerSC = mainController.GetComponent<MainController>();
                    mainControllerSC.CreateZombie();
                }
                AddWindow = Instantiate(TVAddWindow, new Vector2(0, 0), Quaternion.identity);
            }

        }
    }
}
