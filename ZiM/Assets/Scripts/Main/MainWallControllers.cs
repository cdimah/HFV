using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWallControllers : MonoBehaviour
{
    void Start()
    {
        GameObject mainSceneController = GameObject.Find("MainController");                    //Obtaining SceneController Script to know the Scene Size.
        MainController mainSceneContScript = mainSceneController.GetComponent<MainController>();
        float height = 2 * Camera.main.orthographicSize;                                   //Calculating the size of the Camera View.
        float width = height * Camera.main.aspect;
        if (gameObject.name == "BorderL")           //Seting size and position of the Left border from the scene size.
        {
            transform.position = new Vector3((Camera.main.transform.position.x - width / 2), 0, 0);
            transform.localScale = new Vector3(1f, height * 1.1f, 1);
        }

        if (gameObject.name == "BorderR")           //Seting size and position of the Right border from the scene size.
        {
            transform.position = new Vector3((Camera.main.transform.position.x + mainSceneContScript.mansionSizeX - width / 2), 0, 0);
            transform.localScale = new Vector3(1f, height * 1.1f, 1);
        }

        if (gameObject.name == "BorderU")           //Seting size and position of the Upper border from the scene size.
        {
            transform.position = new Vector3((mainSceneContScript.mansionSizeX / 2) - width / 2, (height / 2), 0);
            transform.localScale = new Vector3(mainSceneContScript.mansionSizeX + 2, height , 1);
        }

        if (gameObject.name == "BorderD")           //Seting size and position of the Lower border from the scene size.
        {
            transform.position = new Vector3((mainSceneContScript.mansionSizeX / 2) - width / 2, -height / 2, 0);
            transform.localScale = new Vector3(mainSceneContScript.mansionSizeX + 2, 1, 1);
        }
    }
}
