using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetail : MonoBehaviour
{
    public Text itemName;
    public Text itemDescription;
    public Text itemCity;
    public Image itemDetailed;
    public Button closeButton;

    void Start()
    {
        closeButton.onClick.AddListener(Close);
    }

    void Close()
    {
        Destroy(gameObject);
    }
}
