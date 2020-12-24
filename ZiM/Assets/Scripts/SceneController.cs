using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public int numZombies;
    public int numBystanders;
    public float sceneSizeX;
    public GameObject zombiePrefab;
    public GameObject bystanderPrefab;

    void Start()
    {
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
        }
    }

    public void CreateZombie(Vector2 origin)
    {
        GameObject newZombie = Instantiate(zombiePrefab, origin, Quaternion.identity);
        newZombie.name = "Zombie";
    }

    public void CreateBystander(Vector2 origin)
    {
        GameObject newBystander = Instantiate(bystanderPrefab, origin, Quaternion.identity);
        newBystander.name = "Bystander";
    }
}
