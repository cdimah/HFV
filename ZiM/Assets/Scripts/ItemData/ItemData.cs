using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Item Data", order = 51)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string description;
    [SerializeField]
    private string city;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private Sprite selected;

    public string ItemName
    {
        get
        {
            return itemName;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
    }

    public string City
    {
        get
        {
            return city;
        }
    }

    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    public Sprite Selected
    {
        get
        {
            return selected;
        }
    }
}
