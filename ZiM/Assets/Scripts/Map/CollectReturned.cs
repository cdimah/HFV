using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectReturned : MonoBehaviour
{
    public string cityName;
    public Text collectReturnText;
    public Button acceptButton;

    Button AcceptButton;

    void Start()
    {
        collectReturnText.text = "Your zombies returned from " + cityName + "!\n\nLet's see what they found...";
        acceptButton.onClick.AddListener(GoCollectScene);
    }

    void GoCollectScene()
    {
        GameController.collectedCity = cityName;
        SceneManager.LoadScene("CollectSc");
    }
}
