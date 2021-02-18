using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWindow : MonoBehaviour
{
    public GameObject windowToOpen;

    GameObject window;
    Collider2D coll;


    void Start()
    {
        coll = GetComponent<Collider2D>();


    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (coll == Physics2D.OverlapPoint(mousePos))
            {
                window = GameObject.FindWithTag("Window");
                if (window)
                {
                    return;
                }
                else
                {
                    GameObject newWindow = Instantiate(windowToOpen, new Vector2(0, 0), Quaternion.identity);
                }
            }
        }
    }
}
