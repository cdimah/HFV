using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvasionReturned : MonoBehaviour
{
    public string cityName;
    public int transformedZombies;
    public int sentZombies;
    public int totalZombies;
    public Button acceptButton;
    public Text returnedText;
    public Text transformedText;
    public Text catText;
    public Text sentText;
    public Text totalText;

    void Awake()
    {
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        transform.localScale = new Vector3(width, height, 1f);
        Vector2 pos = Camera.main.transform.position;
        transform.position = new Vector3(pos.x, pos.y, -1f);
        GameController.anotherWindow = true;
    }

    void Start()
    {
        acceptButton.onClick.AddListener(CloseWindow);
        returnedText.text = "Your Zombies have returned from " + cityName + "!";
        sentText.text = "You sent " + sentZombies + " Zombies.";
        transformedText.text = transformedZombies + " Zombies returned.";
        if(GameController.zombieCat == true)
        {
            int catTransformed = Random.Range(1, 3);
            catText.text = "And look, your cat brougt " + catTransformed + " more!";
            totalZombies += catTransformed;
            GameController.qZombies += catTransformed;
        }
        else
        {
            catText.text = "";
        }
        if (GameController.qZombies >= GameController.maxZombies)
        {
            totalText.text = "You reached the maximum number of Zombies: " + GameController.maxZombies;
            GameController.qZombies = GameController.maxZombies;
        }
        else
        {
            totalText.text = "You have a total of " + totalZombies + " Zombies.";
        }
    }

    void CloseWindow()
    {
        GameController.anotherWindow = false;
        Destroy(gameObject);
    }
}
