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
    float speed = 4;               //Initial speed of the player.
    float distToBystander;         //Distance to calculate how far away from bystander.
    float maxBDist = 3;            //Distace at which the zombie will atemp to attack a bystander.
    Vector3 targetPosition;        //Destination of the movement.
    Collider2D coll;               //Collider for interactions.
    GameObject closestBystander;   //GameObject to calculate distance between the zombie and closest bystander
    GameObject enemy;              //Bystander that is being attacked.
    public GameObject head;        //Gameobject to show selected head.
    public GameObject body;        //Gameobject to show selected body.
    public GameObject legs;        //Gameobject to show selected legs.
    SpriteRenderer headSR;
    SpriteRenderer bodySR;
    SpriteRenderer legsSR;

    public float Healthpoints;                  //Remaining life of the character.
    public float MaxHealthpoints;               //Maximum healtpoints of the character.
    public float strength;                      //Variable used to define how much damage will deal.
    public HealthBarController Healthbar;       //Declaration of the Healthbar of this character.

    void Start()
    {
        headSR = head.GetComponent<SpriteRenderer>();
        bodySR = body.GetComponent<SpriteRenderer>();
        legsSR = legs.GetComponent<SpriteRenderer>();
        head = GameController.head;
        if (GameController.head)
        {
            headSR.sprite = GameController.head.GetComponent<SpriteRenderer>().sprite;
        }
        if (GameController.body)
        {
            bodySR.sprite = GameController.body.GetComponent<SpriteRenderer>().sprite;
        }
        if (GameController.legs)
        {
            legsSR.sprite = GameController.legs.GetComponent<SpriteRenderer>().sprite;
        }
        Healthpoints = MaxHealthpoints;
        Healthbar.SetHealth(Healthpoints, MaxHealthpoints);
        coll = GetComponent<Collider2D>();     
        StartCoroutine("AreaCheck");
        targetPosition = transform.position;
        float zRef;
        zRef = targetPosition.y;
        targetPosition.z = zRef;
        transform.position = targetPosition;
    }
    /*
        In the 2 first lines of code is defined the Healt of the Zombie.
        3rd line just calls the collider of the Game Object.
        4th line initiates the funtion “AreaCheck” that will maintain the Zombie Leader looking for near Bystanders.
        Last 5 lines set the correct position of the Zombie in the ”z” axis, setting it equal to the “y” axis. 
     */

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    
        if(Input.GetMouseButtonDown(0))
            {
            if(coll == Physics2D.OverlapPoint(mousePos))
            {
                canMove = true;
            }
        }

        if(Input.GetMouseButtonUp(0) && canMove == true)
        {
            Vector2 posRef = Camera.main.ScreenToWorldPoint(Input.mousePosition);     
            targetPosition = posRef;
            CheckBorders();
            float zRef = targetPosition.y;
            targetPosition.z = zRef;
            destinationSet = true;
            canMove = false;
        }

        if(destinationSet == true)                                                 
        {
            Turn();
            Moving();
        }
    }
    /*
        In the first line and first “if” is checked if the there is a click and if it landed on the Zombie.
        The second “if” checks if when the button is released and if it was over he Zombie to then save the location of the
        mouse as the destination of the Zombie. The CheckBoarders() function is called to prevent the Zombie from going outside of
        th stage. The last “if” just execute the Turn() and Moving() function to move the Zombie.
    */

    void Moving()                                                                   
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            canMove = false;
            destinationSet = false;
        }
    }
    /*
        Moving() function just move the game object to set destination and let the script know when it reaches it.
    */

    void Turn()
    {
        if (transform.position.x != targetPosition.x)               
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
    /*
        Turn() function just check where the Zombie is walking to and turn the sprite to dace each direction.
    */


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
    /*
        CheckBoarders() funcion will check of the bool of any boarder (Up, down, left and right) is set to ”true” and then
        lock the “x” (for the upper and lower boearder) or “y” (For the sides boarders) axis movement.
        If the bools are not set to true then is confirmed as false.
    */

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
    /*
        FincClosestBystander() start by creating an array of every object named "Bystander" and then going trough all of
        them and whenever it find a closer one, ot assign it to the "closes" variable.
        When the whole array os checked, it returns the "closest" Bystander.
    */

    public void DamageDone(float damage)
    {
        Healthpoints -= damage;
        Healthbar.SetHealth(Healthpoints, MaxHealthpoints);
        if (Healthpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
    /*
        DamageDone() function is intended to be called from an enemy GameObject to execute the damage on this Zombie.
        This funtions receives the strength of the attacking enemy and the substract t to the current health.
    */

    private IEnumerator Attacking()
    {
        if (enemy != null)
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
    }
    /*
        Attacking() checksif the bool "attacking" is set to true and then calls the funtion DamageDone() of the enemy
        with the own strength as a parameter.
        then it waits for a 1.5 to 2.5 seconds to execute the funtion again.
    */

    private IEnumerator AreaCheck()                                 
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
    /*
        Areacheck() function start by waiting for 2.5 to 4 seconds, then cjeck if the Zombie is no attacking. If not,
        it looks for the closest Bystander with the FindClosesBystander() function.
        Once it finds the closestone, it sets a destination to this Bystander but only if it is in range (maxBDist).
        In any other case, it does nothing.
    */

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
    /*
        OnTriggerEnter2D() function checks if ther is a collision. If there is, it assigns the collided object to a
        variable and then checks if it is any of the walls or a Bystander.
        If it is a wall, the function sets the current bool to true and then stops the movement.
        If the collided object is a Bystander, it stops and then starts the coroutine Attack().
    */

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bystander")
        {
            enemy = col.gameObject;
            attacking = true;
        }
    }
    /*
        OnTriggerStay2D() function is just checked if there is a Bystander still colliding.
    */ 

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bystander")
        {
            enemy = col.gameObject;
            attacking = false;
            StopCoroutine("Attacking");
        }
    }
    /*
        OnTriggerExit2D() function stop the coroutine Attacking if the collision with a Bystander is finished.
    */
}
