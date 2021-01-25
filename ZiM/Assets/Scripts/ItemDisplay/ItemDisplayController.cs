using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplayController : MonoBehaviour
{
    public Text itemName;
    public Text itemDescription;
    public Text itemCity;
    public Image itemSelected;
    public GameObject UIPrefab;

    void Start()
    {
        GameObject UIShow = Instantiate(UIPrefab, new Vector2(0, 0), Quaternion.identity);
    }

}
