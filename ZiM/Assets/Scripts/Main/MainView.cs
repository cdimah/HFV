using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainView : MonoBehaviour
{
    bool drag;              //Bool to know if the view is being dragged.
    float minX;             //Left limit of the stage in the "x" axis.
    float maxX;             //Right limit of the stage in the "y" axis.
    float sceneSizeX;       //The maximum value at which the main camera will go.
    Vector3 origin;         //Point in whiche the movement begins.
    Vector3 diference;      //Distance that the view travels.

    void Start()
    {
        sceneSizeX = GameController.mansionSize;
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        minX = transform.position.x;
        maxX = minX + sceneSizeX - width;
    }

    void LateUpdate()
    {
        
        if (Input.GetMouseButton(0))
        {
            GameObject window = GameObject.FindWithTag("Window");
            if (window)
            {

            }
            else
            {
                diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
                if (drag == false)
                {
                    drag = true;
                    origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
        }
        else
        {
            drag = false;
        }
        if (drag == true)
        {
            if (minX < (origin.x - diference.x) && maxX > (origin.x - diference.x))
            {
                Camera.main.transform.position = new Vector3((origin.x - diference.x), transform.position.y, transform.position.z);

            }
        }
    }
}
