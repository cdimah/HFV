using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    public bool finishedLevel = false;      //Bool made to lock the movement of the camera.

    bool drag;              //Bool to know if the view is being dragged.
    bool clickedOnZombie;   //Bool to know if clicked over a Zombie.
    float minX;             //Left limit of the stage in the "x" axis.
    float maxX;             //Right limit of the stage in the "y" axis.
    float sceneSizeX;       //The maximum value at which the main camera will go.
    Vector3 origin;         //Point in whiche the movement begins.
    Vector3 diference;      //Distance that the view travels.

    void Start()
    {
        sceneSizeX = GameController.citySize;
        float height = 2 * Camera.main.orthographicSize; 
        float width = height * Camera.main.aspect; 
        minX = transform.position.x; 
        maxX = minX + sceneSizeX - width; 
    }
    /*
        Start() function gets the stage specs of the SceneController stript and then proceeds to creat the boundries of the
        "x" axis.
    */


    void LateUpdate()
    {
        if(finishedLevel == false)
        {
            if (Input.GetMouseButton(0))
            {
                diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
                if (clickedOnZombie == true)
                {
                    return;
                }
                else
                {
                    CheckClickedZombie();
                    if (drag == false && clickedOnZombie == false)
                    {
                        drag = true;
                        origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    }
                }
            }
            else
            {
                drag = false;
                clickedOnZombie = false;
            }

            if (drag == true)
            {
                if (minX < (origin.x - diference.x) && maxX > (origin.x - diference.x))
                {
                    Camera.main.transform.position = new Vector3((origin.x - diference.x), transform.position.y, transform.position.z);

                }
            }
        }
        /*
            LateUpdate() function checks everyframe if there is a click if the level isn't is finished. If it is, then lock the position.
            If the level continues, gets the position and check if a Zombie was hit.
            If hit, does nothing. If hit something else or nothing, set the drag bool to true to indicate there will be movement.
            When drag bool is true, the funtion will get the difference between the origin and the movement of the pointer to move
            the camera accordingly.
        */
    }

    void CheckClickedZombie()
    {
        RaycastHit2D hit;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null && hit.collider.tag == "Zombie")
        {
            clickedOnZombie = true;
        }
        else
        {
            clickedOnZombie = false;
        }
    }
    /*
        Check if Clicked over a Zombie by gettong the clicked position and then theck if it collides with an aitem with
        a "Zombie" tag.
    */
}
