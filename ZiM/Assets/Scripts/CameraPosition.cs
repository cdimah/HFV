using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    bool drag;
    bool clickedOnZombie;
    float minX;
    float maxX;
    Vector3 origin;
    Vector3 diference;

    void Start()
    {
        GameObject sceneController = GameObject.Find("SceneController");
        SceneController sceneContScript = sceneController.GetComponent<SceneController>();
        
        float height = 2 * Camera.main.orthographicSize; 
        float width = height * Camera.main.aspect; 
        minX = transform.position.x; 
        maxX = minX + sceneContScript.sceneSizeX - width; 
    }


    void LateUpdate()
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
}
