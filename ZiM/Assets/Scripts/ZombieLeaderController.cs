using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieLeaderController : MonoBehaviour
{
    bool onUBorder;                //Used to detect if in Upper boarder.
    bool onRBorder;                //Used to detect if in Right boarder.
    bool onDBorder;                //Used to detect if in Lower boarder.
    bool onLBorder;                //Used to detect if in Upper boarder.
    bool canMove;                  //Indicator of player clicked.
    bool destinationSet;           //Destination point is set.
    bool attacking;                //Used to know if the zombie is attacking.
    float damageWait;              //Seconds the player will wait  to make damage.
    float strength = 3;            //Variable used to define how much damage will deal.
    float speed = 4;               //Initial speed of the player.
    float distToBystander;         //Distance to calculate how far away from bystander.
    float maxBDist = 3;            //Distace at which the zombie will atemp to attack a bystander.
    Vector3 targetPosition;        //Destination of the movement.
    Collider2D coll;               //Collider for interactions.
    GameObject closestBystander;   //GameObject to calculate distance between the zombie and closest bystander
    GameObject enemy;              //Bystander that is being attacked.

    public float Healthpoints;                  //Remaining life of the character.
    public float MaxHealthpoints = 10;          //Maximum healtpoints of the character.
    public HealthBarController Healthbar;       //Declaration of the Healthbar of this character.

    void Start()
    {
        Healthpoints = MaxHealthpoints;
        Healthbar.SetHealth(Healthpoints, MaxHealthpoints);
        coll = GetComponent<Collider2D>();     
        StartCoroutine("AreaCheck");
        targetPosition = transform.position;
        float zRef;
        zRef = targetPosition.y;
        targetPosition.z = zRef;
        transform.position =  targetPosition;
    }


    void Update()
    {
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //Register click on player
        if(Input.GetMouseButtonDown(0))
            {
            if(coll == Physics2D.OverlapPoint(mousePos))
            {
                canMove = true;
            }
        }

        

        if(Input.GetMouseButtonUp(0) && canMove == true)
        {
            Vector2 posRef = Camera.main.ScreenToWorldPoint(Input.mousePosition);     //Register the destination point by using a Vector2 variable to avoid changinf z value
            targetPosition = posRef;
            CheckBorders();
            float zRef = targetPosition.y;
            targetPosition.z = zRef;
            destinationSet = true;
            canMove = false;
        
        }

        if(destinationSet == true)                                                 //Begin movement if everything is ready
        {
            Turn();
            Moving();
         
        }
    }

    void Moving()                                                                   
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            canMove = false;
            destinationSet = false;
        }
        
        
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

    GameObject FindClosestBystander()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Bystander");
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
            Destroy(gameObject);
        }
    }

    private IEnumerator Attacking()
    {
        if (attacking == true)
        {
            var enemyChar = enemy.GetComponent<BystanderController>();
            enemyChar.DamageDone(strength);
        }
        damageWait = Random.Range(1.5f, 2.5f);
        yield return new WaitForSeconds(damageWait);
        StartCoroutine("Attacking");
    }

    private IEnumerator AreaCheck()                                 //Coroutine to Keep zombies checking if they are inside the Leader area
    {
        float checkWait;
        checkWait = Random.Range(2.5f, 4f);
        yield return new WaitForSeconds(checkWait);
        if (attacking == false)
        {
            if (destinationSet == false)
            {
                closestBystander = FindClosestBystander();
                if (closestBystander)
                {
                    distToBystander = Vector2.Distance(this.transform.position, closestBystander.transform.position);
                    if (distToBystander < maxBDist)
                    {
                        targetPosition = closestBystander.transform.position;
                        destinationSet = true;
                    }
                }
            }
        }
        
        StartCoroutine("AreaCheck");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject collided = col.gameObject;
        if (collided.name == "BorderU")
        {
            onUBorder = true;
            destinationSet = false;
        }

        if (collided.name == "BorderR")
        {
            onRBorder = true;
            destinationSet = false;
        }

        if (collided.name == "BorderD")
        {
            onDBorder = true;
            destinationSet = false;
        }

        if (collided.name == "BorderL")
        {
            onLBorder = true;
            destinationSet = false;
        }

        if (collided.tag == "Bystander")
        {
            destinationSet = false;
            enemy = collided;
            attacking = true;
            StartCoroutine("Attacking");
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag == "Bystander")
        {
            enemy = col.gameObject;
            attacking = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bystander")
        {
            enemy = col.gameObject;
            attacking = false;
            StopCoroutine("Attacking");
        }
    }
}
