using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectManager : MonoBehaviour
{
    public GameObject UIPrefab;
    public GameObject presentWindow;
    public GameObject zombie;
    public GameObject raccoon;
    public GameObject trash;
    public GameObject item;
    public GameObject currency;
    public GameObject clothes;

    bool zombieEntering;
    bool zombieMovingI;
    bool zombieMovingO;
    bool zombieExiting;
    int cityCollected;
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
    GameObject[] collectibles;
    [SerializeField]
    GameObject[] head;
    [SerializeField]
    GameObject[] body;
    [SerializeField]
    GameObject[] legs;



    void Start()
    {
        GameObject UIShow = Instantiate(UIPrefab, new Vector2(0, 0), Quaternion.identity);
        cityCollected = GameController.collectedCity;
        zombsReturned = GameController.zombsSentToCollect[cityCollected];
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
            GameController.zombsSentToCollect[cityCollected] -= 1;
            if (zombsReturned > 0)
            {
                zombieEntering = true;
            }
            else
            {
                if(zombsReturned == 0 && GameController.zombieRaccoon == true)
                {
                    zombieEntering = true;
                }
                else
                {
                    GameController.cityCollecting[cityCollected] = false;
                    GameController.fashionMagazineCity[cityCollected] = false;
                    GameController.geekGlassesCity[cityCollected] = false;
                    SceneManager.LoadScene("MainSc");
                }
            }
        }
    }

    void ZombiePresents()
    {
        currencyWon = 0;
        int foundProb = Random.Range(0, 11);
        if(GameController.fashionMagazineCity[cityCollected] == true)
        {
            if(foundProb == 0 || foundProb == 1)
            {
                foundProb = 9;
            }
        }

        if(GameController.geekGlassesCity[cityCollected] == true)
        {
            if(foundProb == 3)
            {
                foundProb = 4;
            }
        }

        if(foundProb < 4)
        {
            present = trash;
            if(GameController.recycleBin == true)
            {
                presentText = "Garbage. Directly into the Recyvle Bin. I think we can get 2 coins for this";
                GameController.currency += currencyWon;
            }
            else
            {
                presentText = "Looks like... well, garbage.";
            }

        }
        else if(foundProb < 8)
        {
            int itemFound;
            itemFound = Random.Range(0, (GameController.collectiblesPerCity));
            if(GameController.collectibles[cityCollected, itemFound] == true)
            {
                if (GameController.recycleBin == true)
                {
                    currencyWon = Random.Range(5, 11);
                    presentText = "Wow!! it's a " + itemFoundName + "!\nAnother one... I think we can sell it for $" + currencyWon + ".";
                }
                else
                {
                    presentText = "Another " + itemFoundName + ".\nWe don't want a repeated item... To the trash!.";
                }
            }
            else
            {
                GameController.collectibles[cityCollected, itemFound] = true;
                presentText = "Is that a " + itemFoundName + "? Amazing!\nLets add it to the collection right away!";
            }
            int index = ((cityCollected) * GameController.collectiblesPerCity) + itemFound;
            item = collectibles[index];
            present = item;
        }
        else if(foundProb < 8)
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
            int clothFound = Random.Range(0, GameController.clothesPerOrigin);
            int clothType = Random.Range(0, 3);
            if(GameController.clothFound[(cityCollected + 1), clothType, clothFound] == true)
            {
                if (GameController.recycleBin == true)
                {
                    currencyWon = Random.Range(4, 10);
                    presentText = "We already have one like that. Maybe sell it for $" + currencyWon + ".";
                }
                else
                {
                    presentText = "And yet another repeated thing going to the trash.... =/";
                }

            }
            else
            {
                GameController.clothFound[(cityCollected + 1), clothType, clothFound] = true;
                presentText = "Nice! I have the perfect outfit for that!";

            }
            int index = ((cityCollected) * GameController.clothesPerOrigin) + clothFound;
            if (clothType == 0)
            {
                clothes = head[index];
            }
            else if(clothType == 1)
            {
                clothes = body[index];
            }
            else if (clothType == 2)
            {
                clothes = legs[index];
            }
            present = clothes;

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
        if(zombsReturned == 0)
        {
            newZombie = Instantiate(raccoon, zombieOrigin, Quaternion.identity);
        }
        else
        {
            newZombie = Instantiate(zombie, zombieOrigin, Quaternion.identity);
        }
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
