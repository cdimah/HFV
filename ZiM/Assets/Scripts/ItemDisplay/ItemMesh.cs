using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMesh : MonoBehaviour
{
    int yPos = 0;
    float iconSize;
    Vector2 itemPosition;

    [SerializeField]
    GameObject[] mexicoItems;
    [SerializeField]
    GameObject[] newYorkItems;
    [SerializeField]
    GameObject[] tokioItems;

    void Start()
    {
        int itemsPerCity = GameController.itemsPerCity;
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        iconSize = height / 8;
        itemPosition = new Vector2((- width/ 2) + (iconSize * 2), Camera.main.transform.position.y + (iconSize * 2.5f));
        for(int i = 0; i < itemsPerCity; i++)
        {
            CreateMexicoItem(mexicoItems[i], itemPosition, i);
        }
        itemPosition = new Vector2(itemPosition.x + iconSize, Camera.main.transform.position.y + (iconSize * 2.5f));
        yPos = 0;
        for (int i = 0; i < itemsPerCity; i++)
        {
            CreateNewYorkItem(newYorkItems[i], itemPosition, i);
        }
        itemPosition = new Vector2(itemPosition.x + iconSize, Camera.main.transform.position.y + (iconSize * 2.5f));
        yPos = 0;

        for (int i = 0; i < itemsPerCity; i++)
        {
            CreateTokioItem(tokioItems[i], itemPosition, i);
        }
        itemPosition = new Vector2(itemPosition.x + iconSize, Camera.main.transform.position.y + (iconSize * 2.5f));
        yPos = 0;
    }

    void CreateMexicoItem(GameObject item, Vector2 origin, int index)
    {
        if (GameController.mexicoItems[index] == true)
        {
            GameObject newItem = Instantiate(item, origin, Quaternion.identity);

        }
        yPos += 1;
        if(yPos == 3)
        {
            itemPosition = new Vector2(itemPosition.x + iconSize, itemPosition.y + (iconSize * 2));
        }
        else
        {
            itemPosition = new Vector2(itemPosition.x, itemPosition.y - iconSize);
        }
    }

    void CreateNewYorkItem(GameObject item, Vector2 origin, int index)
    {
        if (GameController.newYorkItems[index] == true)
        {
            GameObject newItem = Instantiate(item, origin, Quaternion.identity);

        }
        yPos += 1;
        if (yPos == 3)
        {
            itemPosition = new Vector2(itemPosition.x + iconSize, itemPosition.y + (iconSize * 2));
        }
        else
        {
            itemPosition = new Vector2(itemPosition.x, itemPosition.y - iconSize);
        }
    }

    void CreateTokioItem(GameObject item, Vector2 origin, int index)
    {
        if (GameController.tokioItems[index] == true)
        {
            GameObject newItem = Instantiate(item, origin, Quaternion.identity);

        }
        yPos += 1;
        if (yPos == 3)
        {
            itemPosition = new Vector2(itemPosition.x + iconSize, itemPosition.y + (iconSize * 2));
        }
        else
        {
            itemPosition = new Vector2(itemPosition.x, itemPosition.y - iconSize);
        }
    }
}
