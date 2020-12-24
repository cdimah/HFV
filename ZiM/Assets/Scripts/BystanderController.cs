using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderController : MonoBehaviour
{

    bool onUBorder = false;        //Used to detect if in Upper boarder.
    bool onRBorder = false;        //Used to detect if in Right boarder.
    bool onDBorder = false;        //Used to detect if in Lower boarder.
    bool onLBorder = false;        //Used to detect if in Upper boarder.
    bool attacking = false;        //Used to know if the zombie is attacking.
    bool destinationSet;           //Destination point is set.
    float speed;                   //Speed at which the Bystander will run.
    float damageWait;              //Seconds the player will wait  to make damage.
    float strength;                //Variable used to define how much damage will deal.
    float setPositionX;            //Variable to calculate X direction of Euphoric.
    float setPositionY;            //Variable to calculate Y direction of Euphoric.
    float checkWait;               //Seconds the bystander will wait before cecking if in area.
    float distToZombie;            //Used to calculate distance to closest Zombie.
    float zRef;                    //Used to assign y value to z.
    float maxDist = 3;             //Distance at which will run from zombie.
    Vector2 refPos;
    Vector3 targetPosition;
    Vector3 euphoricDirection;     //Vector to determinate direction at which the Euphoric will run.
    GameObject closestZombie;
    Collider2D coll;           //Colider for interactions.
    SpriteRenderer mySR;
    GameObject enemyChar;              //Bystander that is being attacked.


    public float Healthpoints;      //Remaining life of the character.
    public float MaxHealthpoints = 10; //Maximum healtpoints of the character.
    public HealthBarController Healthbar;    //Declaration of the Healthbar of this character.
    public enum BystanderType       //List of types of Bystanders.
    {
        Shocked,
        Frightened,
        Smart,
        Euphoric,
        Brave
    }

    BystanderType thisBystander;             //Declare the enum of the bystander's type.

    void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
        int i = Random.Range(0, 5);
        if(i == 0)
        {
            thisBystander = BystanderType.Shocked;
        } else if (i == 1)
        {
            thisBystander = BystanderType.Frightened;
        } else if (i == 2)
        {
            thisBystander = BystanderType.Smart;
        } else if (i == 3)
        {
            thisBystander = BystanderType.Euphoric;
        } else if (i == 4)
        {
            thisBystander = BystanderType.Brave;
        }
        Healthpoints = MaxHealthpoints;
        Healthbar.SetHealth(Healthpoints, MaxHealthpoints);
        coll = GetComponent<Collider2D>();     //Create collider for all interactions.
        targetPosition = transform.position;
        zRef = targetPosition.y;
        targetPosition.z = zRef;
        transform.position = targetPosition;
        switch (thisBystander)
        {
            case BystanderType.Shocked:
                break;
            case BystanderType.Frightened:
                speed = 2;
                mySR.color = Color.blue;
                break;
            case BystanderType.Smart:
                speed = 4;
                strength = 1;
                mySR.color = Color.green;
                break;
            case BystanderType.Euphoric:
                speed = 5;
                strength = 1;
                mySR.color = Color.red;
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
                euphoricDirection = new Vector3(setPositionX, setPositionY, 0f);
                break;
            case BystanderType.Brave:
                speed = 3;
                strength = 2;
                mySR.color = Color.yellow;
                break;
        }
        closestZombie = FindClosestZombie();
        StartCoroutine("ZombieCheck");
    }

    // Update is called once per frame
    void Update()
    {
        switch(thisBystander)
        {
            case BystanderType.Shocked:
                //Only include shocked animation
                break;
            case BystanderType.Frightened:
                if (destinationSet == true)
                {
                    Turn();
                    Moving();
                }
                break;
            case BystanderType.Smart:
                if (destinationSet == true)
                {
                    Turn();
                    Moving();
                }
                break;
            case BystanderType.Euphoric:
                if (destinationSet == true)
                {
                    EuphoricMoving();
                }
                break;
            case BystanderType.Brave:
                if (destinationSet == true)
                {
                    Turn();
                    Moving();
                }
                break;
        }

        
    }

    void CalcDest()
    {
        refPos = transform.position;

        switch (thisBystander)
        {
            case BystanderType.Shocked:
                break;
            case BystanderType.Frightened:
                if (refPos.x > closestZombie.transform.position.x)
                {
                    targetPosition.x = refPos.x + (Random.Range(1.5f, 2.5f));
                }
                else if (refPos.x < closestZombie.transform.position.x)
                {
                    targetPosition.x = refPos.x + (Random.Range(1.5f, 2.5f) * -1);
                }

                if (refPos.y > closestZombie.transform.position.y)
                {
                    targetPosition.y = refPos.y + (Random.Range(1.5f, 2.5f));
                }
                else if (refPos.y < closestZombie.transform.position.y)
                {
                    targetPosition.y = refPos.y + (Random.Range(1.5f, 2.5f) * -1);
                }
                destinationSet = true;

                break;
            case BystanderType.Smart:
                if (refPos.x > closestZombie.transform.position.x)
                {
                    targetPosition.x = refPos.x + (Random.Range(1f, 2.5f));
                }
                else if (refPos.x < closestZombie.transform.position.x)
                {
                    targetPosition.x = refPos.x + (Random.Range(1f, 2.5f) * -1);
                }

                if (refPos.y > closestZombie.transform.position.y)
                {
                    targetPosition.y = refPos.y + (Random.Range(1f, 2.5f));
                }
                else if (refPos.y < closestZombie.transform.position.y)
                {
                    targetPosition.y = refPos.y + (Random.Range(1f, 2.5f) * -1);
                }
                destinationSet = true;

                break;
            case BystanderType.Euphoric:
                destinationSet = true;
                break;
            case BystanderType.Brave:
                targetPosition = closestZombie.transform.position;
                destinationSet = true;

                break;
        }
        
    }

    void Moving()
    {
        CheckBorders();
        zRef = targetPosition.y;
        targetPosition.z = zRef;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            destinationSet = false;
        }
    }

    void EuphoricMoving()
    {
        euphoricDirection.z = euphoricDirection.y;
        if(euphoricDirection.x == 1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.Translate(euphoricDirection * speed * Time.deltaTime);
    }

    void Turn()
    {
        if (transform.position.x != targetPosition.x)               //To avoid turning at begining or moving only on 'y' axis
        {

            if (transform.position.x < targetPosition.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    void CheckBorders()
    {
        if (onUBorder == true && targetPosition.y > transform.position.y)
        {
            targetPosition.y = transform.position.y;
        }
        else if (onUBorder == true && targetPosition.y < transform.position.y)
        {
            onUBorder = false;
        }

        if (onRBorder == true && targetPosition.x > transform.position.x)
        {
            targetPosition.x = transform.position.x;
        }
        else if (onRBorder == true && targetPosition.x < transform.position.x)
        {
            onRBorder = false;
        }

        if (onDBorder == true && targetPosition.y < transform.position.y)
        {
            targetPosition.y = transform.position.y;
        }
        else if (onDBorder == true && targetPosition.y > transform.position.y)
        {
            onDBorder = false;
        }

        if (onLBorder == true && targetPosition.x < transform.position.x)
        {
            targetPosition.x = transform.position.x;
        }
        else if (onLBorder == true && targetPosition.x > transform.position.x)
        {
            onLBorder = false;
        }

    }

    GameObject FindClosestZombie()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Zombie");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public void DamageDone(float damage)
    {
        Healthpoints -= damage;
        Healthbar.SetHealth(Healthpoints, MaxHealthpoints);
        if (Healthpoints <= 0)
        {
            GameObject controller = GameObject.Find("SceneController");
            var cont = controller.GetComponent<SceneController>();
            cont.CreateZombie(transform.position);
            Destroy(gameObject);
        }
    }
    private IEnumerator Attacking()
    {
        if (attacking == true)
        {
            if (enemyChar.name == "ZombieLeader")
            {
                var enemy = enemyChar.GetComponent<ZombieLeaderController>();
                enemy.DamageDone(strength);
            }

            if (enemyChar.name == "Zombie")
            {
                var enemy = enemyChar.GetComponent<ZombieController>();
                enemy.DamageDone(strength);
            }
        }
        damageWait = Random.Range(1.5f, 2.5f);
        yield return new WaitForSeconds(damageWait);
        StartCoroutine("Attacking");
    }

    private IEnumerator ZombieCheck()                                 //Coroutine to Keep bystander checking for Zombies nearby
    {
        checkWait = Random.Range(1f, 2.5f);
        yield return new WaitForSeconds(checkWait);

        if (attacking == false)
        {
            if (destinationSet == false)
            {
                closestZombie = FindClosestZombie();
                distToZombie = Vector2.Distance(this.transform.position, closestZombie.transform.position);
                if (distToZombie < maxDist)
                {
                    CalcDest();
                }
            }
        }
       
        StartCoroutine("ZombieCheck");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject collided = col.gameObject;
        if (thisBystander == BystanderType.Euphoric)
        {
            if (collided.name == "BorderU")
            {
                euphoricDirection.y = euphoricDirection.y * - 1;
            }

            if (collided.name == "BorderR")
            {
                euphoricDirection.x = euphoricDirection.x * -1;
            }

            if (collided.name == "BorderD")
            {
                euphoricDirection.y = euphoricDirection.y * -1;
            }

            if (collided.name == "BorderL")
            {
                euphoricDirection.x = euphoricDirection.x * -1;
            }
            if (collided.tag == "Zombie")
            {
                
                if (collided.name == "ZombieLeader")
                {
                    var enemy = collided.GetComponent<ZombieLeaderController>();
                    enemy.DamageDone(strength);
                }

                if (collided.name == "Zombie")
                {
                    var enemy = collided.GetComponent<ZombieController>();
                    enemy.DamageDone(strength);
                }
            }
        }
        else
        {
            if (collided.name == "BorderU")
            {
                onUBorder = true;
                destinationSet = false;
            }

            if (collided.name == "BorderR")
            {
                onRBorder = true;
                destinationSet = false;
                if (thisBystander == BystanderType.Smart)
                {
                    enemyChar = FindClosestZombie();
                    StartCoroutine("Attacking");
                }
            }

            if (collided.name == "BorderD")
            {
                onDBorder = true;
                destinationSet = false;
            }

            if (collided.name == "BorderL")
            {
                destinationSet = false;
                if (thisBystander == BystanderType.Smart)
                {
                    destinationSet = false;
                    enemyChar = FindClosestZombie();
                    StartCoroutine("Attacking");
                }
            }

            if (collided.tag == "Zombie")
            {
                if (thisBystander == BystanderType.Brave)
                {
                    destinationSet = false;
                    enemyChar = collided;
                    StartCoroutine("Attacking");
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        GameObject collided = col.gameObject;
        enemyChar = collided.gameObject;
        switch (thisBystander)
        {
            case BystanderType.Shocked:
                break;
            case BystanderType.Frightened:
                break;
            case BystanderType.Smart:
                if (onLBorder == true)
                {
                    if (collided.tag == "Zombie")
                    {
                        destinationSet = false;
                        attacking = true;
                    }
                }
                else if (onRBorder == true)
                {
                    if (collided.tag == "Zombie")
                    {
                        destinationSet = false;
                        attacking = true;
                    }
                }
                break;
            case BystanderType.Euphoric:
                break;
            case BystanderType.Brave:

                if (collided.tag == "Zombie")
                {
                    destinationSet = false;
                    attacking = true;
                }
                break;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        GameObject collided = col.gameObject;
        enemyChar = collided.gameObject;
        switch (thisBystander)
        {
            case BystanderType.Shocked:
                break;
            case BystanderType.Frightened:
                break;
            case BystanderType.Smart:
                if (collided.tag == "Zombie")
                {
                    attacking = false;
                    StopCoroutine("Attacking");
                }
                break;
            case BystanderType.Euphoric:
                break;
            case BystanderType.Brave:
                if (collided.tag == "Zombie")
                {
                    attacking = false;
                    StopCoroutine("Attacking");
                }
                break;
        }
    }
}
