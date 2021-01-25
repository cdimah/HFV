using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectManager : MonoBehaviour
{
    public GameObject UIPrefab;
    public GameObject presentWindow;
    public GameObject zombie;
    public GameObject trash;
    public GameObject item;
    public GameObject currency;
    public GameObject clothes;

    bool zombieEntering;
    bool zombieMovingI;
    bool zombieMovingO;
    bool zombieExiting;
    string cityCollected;
    string presentText;
    string itemFoundName;
    int zombsReturned;
    int currencyWon;
    float height;
    float width;
    Vector2 zombieDestinyI;
    Vector2 zombieDestinyO;
    GameObject newZombie;
    GameObject newPresent;
    GameObject present;
    GameObject presentWin;

    [SerializeField]
    GameObject[] mexicoItems;
    [SerializeField]
    GameObject[] newYorkItems;
    [SerializeField]
    GameObject[] tokioItems;

    

    void Start()
    {
        GameObject UIShow = Instantiate(UIPrefab, new Vector2(0, 0), Quaternion.identity);
        cityCollected = GameController.collectedCity;
        if (cityCollected == "Mexico")
        {
            zombsReturned = GameController.zombSentCollMexico;
        }
        else if (cityCollected == "NewYork")
        {
            zombsReturned = GameController.zombSentCollNewYork;
        }
        else if (cityCollected == "Tokio")
        {
            zombsReturned = GameController.zombSentCollTokio;
        }
        zombieEntering = true;
        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        zombieDestinyI = Camera.main.transform.position;
        zombieDestinyO = new Vector2((width / 2) + 2f, 0f);

    }

    void Update()
    {
        if(zombieEntering == true)
        {
            ZombiePresents();
            ZombieEnter();
        }

        if(newZombie.transform.position.x == zombieDestinyI.x)
        {
            PresentingItem();
            zombieMovingI = false;
        }

        if(zombieMovingI == true)
        {
            Moving(zombieDestinyI);
        }

        if (zombieMovingO == true)
        {
            Moving(zombieDestinyO);
        }

        if (presentWin)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Destroy(presentWin.gameObject);
                zombieExiting = true;
            }
        }

        if(zombieExiting == true)
        {
            ZombieExits();
        }

        if(newZombie.transform.position.x == zombieDestinyO.x)
        {
            zombieMovingO = false;
            Destroy(newZombie.gameObject);
            zombsReturned -= 1;
            if (cityCollected == "Mexico")
            {
                GameController.zombSentCollMexico -= 1;
            }
            else if (cityCollected == "NewYork")
            {
                GameController.zombSentCollNewYork -= 1;
            }
            else if (cityCollected == "Tokio")
            {
                GameController.zombSentCollTokio -= 1;
            }
            if (zombsReturned > 0)
            {
                zombieEntering = true;
            }
            else
            {
                if (cityCollected == "Mexico")
                {
                    GameController.mexicoCollecting = false;
                }
                else if (cityCollected == "NewYork")
                {
                    GameController.newYorkCollecting = false;
                }
                else if (cityCollected == "Tokio")
                {
                    GameController.tokioCollecting = false;
                }

                SceneManager.LoadScene("MainSc");
            }
        }
    }

    void ZombiePresents()
    {
        currencyWon = 0;
        int foundProb = Random.Range(1, 11);
        if(foundProb < 4)
        {
            present = trash;
            presentText = "Looks like... well, garbage.";

        }
        else if(foundProb < 7)
        {
            if (cityCollected == "Mexico")
            {
                int itemFound;
                itemFound = Random.Range(0, (GameController.itemsPerCity));
                itemFoundName = mexicoItems[itemFound].name;
                if (GameController.mexicoItems[itemFound] == true)
                {
                    currencyWon = Random.Range(5, 11);
                    presentText = "Wow!! it's a " + itemFoundName + "!\nAnother one... I think we can sell it for $" + currencyWon + ".";
                }
                else
                {
                    GameController.mexicoItems[itemFound] = true;
                    presentText = "Is that a " + itemFoundName + "? Amazing!\nLets add it to the collection right away!";
                }
                item = mexicoItems[itemFound];
            }
            else if (cityCollected == "NewYork")
            {
                int itemFound;
                itemFound = Random.Range(0, (GameController.itemsPerCity));
                itemFoundName = newYorkItems[itemFound].name;
                if (GameController.newYorkItems[itemFound] == true)
                {
                    currencyWon = Random.Range(5, 11);
                    presentText = "Wow!! it's a " + itemFoundName + "!\nAnother one... I think we can sell it for $" + currencyWon + ".";
                }
                else
                {
                    GameController.newYorkItems[itemFound] = true;
                    presentText = "Is that a " + itemFoundName + "? Amazing!\nLets add it to the collection right away!";
                }
                item = newYorkItems[itemFound];

            }
            else if (cityCollected == "Tokio")
            {
                int itemFound;
                itemFound = Random.Range(0, (GameController.itemsPerCity));
                itemFoundName = tokioItems[itemFound].name;
                if (GameController.tokioItems[itemFound] == true)
                {
                    currencyWon = Random.Range(5, 11);
                    presentText = "Wow!! it's a " + itemFoundName + "!\nAnother one... I think we can sell it for $" + currencyWon + ".";
                }
                else
                {
                    GameController.tokioItems[itemFound] = true;
                    presentText = "Is that a " + itemFoundName + "? Amazing!\nLets add it to the collection right away!";
                }
                item = tokioItems[itemFound];
            }
            present = item;
        }
        else if(foundProb < 9)
        {
            present = currency;
            int currencyFound;
            currencyFound = Random.Range(0, 3);
            if(currencyFound == 0)
            {
                currencyWon = Random.Range(3, 5);
                presentText = "Where did you get that spare change?\nWell, lets count it. 1, 2, 2.5... $" + currencyWon + ".\nNot bad!";

            }
            else if (currencyFound == 1)
            {
                currencyWon = Random.Range(5, 11);
                presentText = "Is that a wallet? Did you steal this?\nAnd inside we have $" + currencyWon + ".\nNice!";
            }
            else if (currencyFound == 2)
            {
                currencyWon = Random.Range(10, 20);
                presentText = "A bag of gold coins!\nAccording to my calculations, there is $" + currencyWon + " in there. Yay!";
            }
        }
        else
        {
            present = clothes;
            presentText = "Some fashion clothes! We don´t have a wardrobe but we will have it soon! ^";
        }
        GameController.currency += currencyWon;
    }

    void ZombieEnter()
    {
        zombieEntering = false;
        zombieMovingI = true;
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        float setPositionX = -(width / 2) -2f;
        float setPositionY = 0f;
        Vector2 zombieOrigin = new Vector2(setPositionX, setPositionY);
        newZombie = Instantiate(zombie, zombieOrigin, Quaternion.identity);
        Vector2 presentOrigin = new Vector3(setPositionX + 1f, setPositionY, -1f);
        newPresent = Instantiate(present, presentOrigin, Quaternion.identity);
        newPresent.transform.parent = newZombie.transform;
    }

    void PresentingItem()
    {
        if (zombieMovingI == true)
        {
            presentWin = Instantiate(presentWindow, new Vector2(0, 0), Quaternion.identity);
            presentWin.GetComponent<PresentWindow>().presentText.text = "" + presentText;
        }
    }

    void ZombieExits()
    {
        zombieExiting = false;
        zombieMovingO = true;
    }

    void Moving(Vector2 destiny)
    {
        newZombie.transform.position = Vector3.MoveTowards(newZombie.transform.position, destiny, 5 * Time.deltaTime);
    }
}
