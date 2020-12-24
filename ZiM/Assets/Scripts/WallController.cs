using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    void Awake()
    {
        GameObject sceneController = GameObject.Find("SceneController");                    //Obtaining SceneController Script to know the Scene Size.
        SceneController sceneContScript = sceneController.GetComponent<SceneController>();
        float height = 2 * Camera.main.orthographicSize;                                   //Calculating the size of the Camera View.
        float width = height * Camera.main.aspect;
        if (gameObject.name == "BorderL")           //Seting size and position of the Left border from the scene size.
        {
            transform.position = new Vector3((Camera.main.transform.position.x - width/2), 0, 0);
            transform.localScale = new Vector3(1f, height * 1.1f, 1);
        }

        if (gameObject.name == "BorderR")           //Seting size and position of the Right border from the scene size.
        {
            transform.position = new Vector3((Camera.main.transform.position.x + sceneContScript.sceneSizeX - width /2), 0, 0);
            transform.localScale = new Vector3(1f, height * 1.1f, 1);
        }

        if (gameObject.name == "BorderU")           //Seting size and position of the Upper border from the scene size.
        {
            transform.position = new Vector3((sceneContScript.sceneSizeX / 2) - width / 2, height / 2, 0);
            transform.localScale = new Vector3(sceneContScript.sceneSizeX + 2, 2, 1);
        }

        if (gameObject.name == "BorderD")           //Seting size and position of the Lower border from the scene size.
        {
            transform.position = new Vector3((sceneContScript.sceneSizeX / 2) - width / 2, -height / 2, 0);
            transform.localScale = new Vector3(sceneContScript.sceneSizeX + 2, 1, 1);
        }
    }

}
