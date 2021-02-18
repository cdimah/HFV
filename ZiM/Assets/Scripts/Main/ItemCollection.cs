using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class ItemCollection : MonoBehaviour
{
    public GameObject itemSelected;
    public GameObject closeButton;
    public GameObject window;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemCity;
    [SerializeField] TMP_Text itemDescription;

    int columnPos = 0;
    int itemsPerColumn = 4;
    int numberOfCities;
    int collectiblesPerCity;
    float xPos;
    float yPos;
    float yInitPos;
    float iconSize;
    Vector2 itemPosition;
    Collider2D coll;

    [SerializeField]
    GameObject[] collectibles;

    void Awake()
    {
        gameObject.name = "ItemCollectionView";
    }

    void Start()
    {
        coll = closeButton.GetComponent<Collider2D>();
        float height = (2 * Camera.main.orthographicSize) * 0.8f;
        float width = (2 * Camera.main.orthographicSize) * Camera.main.aspect * 0.9f;
        iconSize = width / 12;
        window.transform.localScale = new Vector2(width, height);
        window.transform.position = new Vector3(Camera.main.transform.position.x, -(2 * Camera.main.orthographicSize) * 0.1f, -8f);
        itemSelected.transform.localScale = new Vector2(height * 0.25f, height * 0.25f);
        itemSelected.transform.position = new Vector3(window.transform.position.x - (width / 4f), window.transform.position.y - (height / 3f), -9f);
        itemName.transform.position = new Vector3(window.transform.position.x - (width / 6f), (window.transform.position.y - (height / 4f)), -9f);
        itemCity.transform.position = new Vector3(window.transform.position.x + (width / 4f), (window.transform.position.y - (height / 4f)), -9f);
        itemDescription.transform.position = new Vector3(window.transform.position.x - (width / 6f), (window.transform.position.y - (height / 3f)), -9f);
        closeButton.transform.localScale = new Vector2(iconSize, iconSize);
        closeButton.transform.position = new Vector3(window.transform.position.x + (width / 2) - (iconSize), window.transform.position.y + (height / 2) - (iconSize), -9f);
        xPos = Camera.main.transform.position.x -(width / 2f) + (iconSize * 1.2f);
        yInitPos = (height / 2f) - (iconSize * 1.2f);
        yPos = yInitPos;
        numberOfCities = GameController.numberOfCities;
        collectiblesPerCity = GameController.collectiblesPerCity;
        for (int i = 0; i < numberOfCities; i++)
        {
            for (int j = 0; j < collectiblesPerCity; j++)
            {
                int index = (i * collectiblesPerCity) + j;
                CreateCollectibles(collectibles[index], i, j);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (coll == Physics2D.OverlapPoint(mousePos))
            {
                Close();
            }
        }
    }

    void CreateCollectibles(GameObject collectible, int cityIndex, int collectibleIndex)
    {
        if (GameController.collectibles[cityIndex,collectibleIndex] == true)
        {
            collectible.transform.position = new Vector3(xPos, yPos, -9f);
        }
        else
        {
            collectible.SetActive(false);
        }
        columnPos += 1;
        if (columnPos == itemsPerColumn)
        {
            columnPos = 0;
            xPos += iconSize * 1.1f;
            yPos = yInitPos;
        }
        else
        {
            yPos -= iconSize * 1.1f;
        }
    }

    public void UpdateSelectedInfo(string iName, string iCity, string iDescription, Sprite iSelected)
    {
        itemName.text = iName;
        itemCity.text = iCity;
        itemDescription.text = iDescription;
        itemSelected.GetComponent<SpriteRenderer>().sprite = iSelected;
    }

    void Close()
    {
        Destroy(gameObject);
    }
}
