using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemdisplayManager : MonoBehaviour
{
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
        iconSize = height / 9;
        itemPosition = new Vector2((-width / 2) + (iconSize * 2), Camera.main.transform.position.y + (iconSize * 2.5f));
        for (int i = 0; i < itemsPerCity; i++)
        {
            CreateMexicoItem(mexicoItems[i], i);
            CreateNewYorkItem(mexicoItems[i], i);
            CreateTokioItem(mexicoItems[i], i);
        }
    }

    void CreateMexicoItem(GameObject item, int index)
    {
        if (GameController.mexicoItems[index] == false)
        {
            mexicoItems[index].SetActive(false);
        }
    }

    void CreateNewYorkItem(GameObject item, int index)
    {
        if (GameController.newYorkItems[index] == false)
        {
            newYorkItems[index].SetActive(false);
        }
    }

    void CreateTokioItem(GameObject item, int index)
    {
        if (GameController.tokioItems[index] == false)
        {
            tokioItems[index].SetActive(false);
        }
    }
}
