using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Item : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public string itemCity;
    public Sprite itemSelected;
    public GameObject itemDisplay;

    bool onCollection;
    GameObject itemDispConf;
    GameObject itemCollectionWindow;
    Collider2D coll;           //Colider for interactions.
    SpriteRenderer spriteRenderer;

    [SerializeField]
    ItemData itemData;

    void Start()
    {
        GameObject itemDisp = GameObject.Find("ItemCollectionView");
        if(itemDisp)
        {
            onCollection = true;
        }
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
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (sceneName == "CollectSc")
            {
                return;
            }
            else
            {
                if (coll == Physics2D.OverlapPoint(mousePos))
                {
                    GameObject itemDisp = GameObject.Find("ItemCollectionView");
                    if (onCollection)
                    {
                        itemDisp.GetComponent<ItemCollection>().UpdateSelectedInfo(itemName, itemCity, itemDescription, itemSelected);
                    }
                    else
                    {
                        GameObject window = GameObject.FindWithTag("Window");
                        if (window)
                        {

                        }
                        else
                        {
                            itemDispConf = GameObject.Find("ItemDetailWindow");
                            if (itemDispConf)
                            {

                            }
                            else
                            {
                                itemDispConf = Instantiate(itemDisplay, new Vector3(0f, 0f, -9f), Quaternion.identity);
                                itemDispConf.name = "ItemDetailWindow";
                                itemDispConf.GetComponent<ItemDetail>().itemName.text = itemName;
                                itemDispConf.GetComponent<ItemDetail>().itemCity.text = itemCity;
                                itemDispConf.GetComponent<ItemDetail>().itemDescription.text = itemDescription;
                                itemDispConf.GetComponent<ItemDetail>().itemDetailed.sprite = itemSelected;
                            }
                        }
                    }
                }
            }
        }
    }
}
