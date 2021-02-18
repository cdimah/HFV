using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayManager : MonoBehaviour
{
    int numberOfCities = GameController.numberOfCities;
    int collectiblesPerCity = GameController.collectiblesPerCity;
    float iconSize;
    Vector2 itemPosition;

    [SerializeField]
    GameObject[] collectibles;

    void Start()
    {
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        iconSize = height / 9;
        itemPosition = new Vector2((-width / 2) + (iconSize * 2), Camera.main.transform.position.y + (iconSize * 2.5f));

        for (int i = 0; i < numberOfCities; i++)
        {
            for (int j = 0; j < collectiblesPerCity; j++)
            {
                int index = (i * collectiblesPerCity) + j;
                CreateCollectibles(collectibles[index], i, j);
            }
        }
    }

    void CreateCollectibles(GameObject collectible, int cityIndex, int collectibleIndex)
    {
        if (GameController.collectibles[cityIndex, collectibleIndex] == false)
        {
            collectible.SetActive(false);
        }
    }
}
