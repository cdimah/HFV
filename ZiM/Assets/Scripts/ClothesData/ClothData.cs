using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ClothData", menuName = "Cloth Data", order = 52)]
public class ClothData : ScriptableObject
{
    [SerializeField]
    private int origin;
    [SerializeField]
    private int type;
    [SerializeField]
    private int index;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private Sprite wearing;

    public int Origin
    {
        get
        {
            return origin;
        }
    }

    public int Type
    {
        get
        {
            return type;
        }
    }

    public int Index
    {
        get
        {
            return index;
        }
    }

    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    public Sprite Wearing
    {
        get
        {
            return wearing;
        }
    }
}
