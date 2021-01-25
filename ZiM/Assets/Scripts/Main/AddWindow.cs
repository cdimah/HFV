using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddWindow : MonoBehaviour
{
    public Button acceptButton;

    void Start()
    {
        acceptButton.onClick.AddListener(CloseWindow);
    }

    void CloseWindow()
    {
        Destroy(gameObject);
    }
}
