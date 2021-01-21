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
    public Button AcceptButton;
    public Text ReturnedText;
    public Text TransformedText;
    public Text SentText;
    public Text TotalText;

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
        AcceptButton.onClick.AddListener(CloseWindow);
        ReturnedText.text = "Your Zombies have returned from " + cityName + "!";
        SentText.text = "You sent " + sentZombies + " Zombies.";
        TransformedText.text = transformedZombies + " Zombies returned.";
        if(GameController.qZombies == GameController.maxZombies)
        {
            TotalText.text = "You reached the maximum number of Zombies: " + totalZombies;
        }
        else
        {
            TotalText.text = "You have a total of " + totalZombies + " Zombies.";
        }
    }

    void CloseWindow()
    {
        GameController.anotherWindow = false;
        Destroy(gameObject);
    }
}
