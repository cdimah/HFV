using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int numZombies;                  //Numer of Zombies that will be created at the beingin of the Stage.
    public int numBystanders;               //Numer of Bystanders that will be created at the beingin of the Stage.
    public int lostZombies;                 //Number of zombies lost during the game.
    public int transformedZombies;          //Number of Bystanders trasformed to Zombie.
    public float sceneSizeX;                //Length of the Scene on the "x" axis.
    public GameObject zombiePrefab;         //Gameobject to be able to create the Zombies.
    public GameObject bystanderPrefab;      //Gameobject to be able to create the Bystanders.
    public GameObject resScript;            //Gameobject made to create de Results review at the end of the minigame.
    public GameObject UIPrefab;             //Create the Game Object that shows the UI.

    bool finishedLevel = false;

    void Awake()
    {
        numZombies = GameController.zombToSend;
        numBystanders = GameController.numBystanders;
        sceneSizeX = GameController.citySize;
        lostZombies = 0;
        transformedZombies = 0;
    }

    void Start()
    {
        GameObject UIShow = Instantiate(UIPrefab, new Vector2(0, 0), Quaternion.identity);
        float height = 2 * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        int zom;
        int bys;
        for (bys = numBystanders; bys > 0; bys--)
        {
            float positionX = Random.Range(Camera.main.transform.position.x + width /2f, Camera.main.transform.position.x + sceneSizeX - 2f - width / 2f);
            float positionY = Random.Range(height / 2f - 2.5f, 2f -height / 2f);
            Vector2 bysPosition = new Vector2(positionX, positionY);
            CreateBystander(bysPosition);
        }

        for (zom = numZombies; zom > 0; zom--)
        {
            Vector2 leadRef = GameObject.Find("ZombieLeader").transform.position;
            float setPositionX;
            float setPositionY;
            int select = Random.Range(1, 5);
            if (select == 1)
            {
                setPositionX = 1f;
                setPositionY = 1f;
            }
            else if (select == 2)
            {
                setPositionX = 1f;
                setPositionY = -1f;
            }
            else if (select == 3)
            {
                setPositionX = -1f;
                setPositionY = 1f;
            }
            else
            {
                setPositionX = -1f;
                setPositionY = -1f;
            }

            leadRef.x = leadRef.x + (Random.Range(0f, 4.5f) * setPositionX);
            leadRef.y = leadRef.y + (Random.Range(0f, 3f) * setPositionY);
            CreateZombie(leadRef);
            transformedZombies -= 1;
        }
    }
    /*
        Since this Script is made to be the main controller of the Minigame Scene, mostly everything happens at the very begining.
        The funtion Awake begins by getting the size of the camera to then proceed to create the number of zombies according
        to the stage size.After it does the same but with the Bystanders.
    */

    void Update ()
    {
        int bystandersLeft = GameObject.FindGameObjectsWithTag("Bystander").Length;
        if(bystandersLeft == 0 && finishedLevel == false)
        {
            FinishLevel();
        }
    }

    public void CreateZombie(Vector2 origin)
    {
        GameObject newZombie = Instantiate(zombiePrefab, origin, Quaternion.identity);
        newZombie.name = "Zombie";
        transformedZombies += 1;
    }
    /*
        CreatZombie() function creates a Zombie with the name Zombie. Nothing more =p
    */

    public void CreateBystander(Vector2 origin)
    {
        GameObject newBystander = Instantiate(bystanderPrefab, origin, Quaternion.identity);
        newBystander.name = "Bystander";
    }
    /*
        CreatBystander() function creates a Bystander with the name Bystander. Nothing more =p
    */

    public void FinishLevel()
    {
        finishedLevel = true;
        int zombiesLeft = GameObject.FindGameObjectsWithTag("Zombie").Length;
        GameObject leader;
        leader = GameObject.Find("ZombieLeader");
        if(leader != null)
        {
            zombiesLeft -= 1;
        }
        GameController.qZombies += zombiesLeft;
        if (GameController.qZombies > GameController.maxZombies)
        {
            GameController.qZombies = GameController.maxZombies;
        }
        GameObject resultScript = Instantiate(resScript, new Vector2(0, 0), Quaternion.identity);
        resultScript.GetComponent<ResultScript>().zombiesLeft = zombiesLeft;
        resultScript.GetComponent<ResultScript>().transformedZombies = transformedZombies;
        resultScript.GetComponent<ResultScript>().lostZombies = lostZombies;
        resultScript.GetComponent<ResultScript>().totalOfZombies = GameController.qZombies;
    }
}
