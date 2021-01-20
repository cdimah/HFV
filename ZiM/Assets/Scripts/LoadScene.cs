using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string scene;                //Var created to work as the name of the Scene to be loaded.

    private void OnMouseDown()
    {
        SceneManager.LoadScene(scene);
    }
    /*
        When touched, the containing object will load the indicated Scene.
    */

    public void destroyButton()
    {
        Destroy(gameObject);
    }
    
}
