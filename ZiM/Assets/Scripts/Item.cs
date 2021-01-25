using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool found = false;
    public bool selected = false;
    public string itemName;
    public string itemDescription;
    public string itemCity;
    public Sprite itemSelected;

    Collider2D coll;           //Colider for interactions.
    SpriteRenderer spriteRenderer;

    [SerializeField]
    ItemData itemData;

    void Start()
    {
        coll = GetComponent<Collider2D>();
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        float iconSize = height / 8;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemData.Icon;
        transform.localScale = new Vector3(iconSize, iconSize, 1);
        itemName = itemData.ItemName;
        itemDescription = itemData.Description;
        itemCity = itemData.City;
        itemSelected = itemData.Selected;
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (coll == Physics2D.OverlapPoint(mousePos))
            {
                GameObject itemDisp = GameObject.Find("ItemDisplayController");
                itemDisp.GetComponent<ItemDisplayController>().itemName.text = itemName;
                itemDisp.GetComponent<ItemDisplayController>().itemDescription.text = itemDescription;
                itemDisp.GetComponent<ItemDisplayController>().itemCity.text = itemCity;
                itemDisp.GetComponent<ItemDisplayController>().itemSelected.sprite = itemSelected;
            }
        }
    }
}
