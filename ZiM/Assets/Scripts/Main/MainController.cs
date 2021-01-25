using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public int numZombies;
    public float mansionSizeX;
    public GameObject leaderInPrefab;
    public GameObject zombieInPrefab;       //Gameobject to be able to create the Zombies.
    public GameObject UIPrefab;             //Create the Game Object that shows the UI.

    float height;
    float width;

    void Awake()
    {
        numZombies = GameController.qZombies;
        mansionSizeX = GameController.mansionSize;
        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
    }

    void Start()
    {
        GameObject UIShow = Instantiate(UIPrefab, new Vector2(0, 0), Quaternion.identity);
        

        for (int zom = numZombies; zom > 0; zom--)
        {
            CreateZombie();
        }
        CreateLeader();
    }

    public void CreateZombie()
    {
        float setPositionX = Random.Range(Camera.main.transform.position.x - (width / 2) + 2f, Camera.main.transform.position.x + mansionSizeX - 2f - width / 2);
        float setPositionY = Random.Range(-1f, -(height / 2) + 1f);
        Vector2 origin = new Vector2(setPositionX, setPositionY);
        GameObject newZombie = Instantiate(zombieInPrefab, origin, Quaternion.identity);
    }

    void CreateLeader()
    {
        float setPositionX = Random.Range(Camera.main.transform.position.x - (width / 2) + 2f, Camera.main.transform.position.x + mansionSizeX - 2f - width / 2);
        float setPositionY = Random.Range(-1f, -(height / 2) + 1f);
        Vector2 origin = new Vector2(setPositionX, setPositionY);
        GameObject newLeader = Instantiate(leaderInPrefab, origin, Quaternion.identity);
    }
}
