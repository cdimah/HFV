using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothes : MonoBehaviour
{
    public int origin;
    public int type;
    public int index;
    public Sprite selectedCloth;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    public ClothData clothData;

    void Awake()
    {
        /* spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = clothData.Wearing; */
    }

    public Sprite ClothesUpdate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = clothData.Wearing;
        return spriteRenderer.sprite;
    }
}
