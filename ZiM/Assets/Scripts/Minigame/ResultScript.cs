using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ResultScript : MonoBehaviour
{
    public Button acceptButton;
    public Text finishedText;
    public Text transformedText;
    public Text lostText;
    public Text totalText;

    public int zombiesLeft;
    public int transformedZombies;
    public int lostZombies;
    public int totalOfZombies;

    GameObject cameraLock;

    void Awake()
    {
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        transform.localScale = new Vector3(width, height, 1f);
        Vector2 pos = Camera.main.transform.position;
        transform.position = new Vector3(pos.x, pos.y, -1f);
    }

    void Start()
    {

        acceptButton.onClick.AddListener(Accept);
        finishedText.text = "Congratulations! You finished this level with " + zombiesLeft + " Zombies";
        transformedText.text = "You transformed " + transformedZombies + " Zombies";
        lostText.text = "You lost " + lostZombies + " Zombies";
        if (GameController.qZombies == GameController.maxZombies)
        {
            totalText.text = "Your reached the maximum number of zombies: " + totalOfZombies;
        } else
        {
            totalText.text = "Your total of zombies is: " + totalOfZombies;
        }
        cameraLock = GameObject.Find("Main Camera");
        cameraLock.GetComponent<CameraPosition>().finishedLevel = true;
    }

    void Accept()
    {
        
        SceneManager.LoadScene("MapSc");
    }

}
