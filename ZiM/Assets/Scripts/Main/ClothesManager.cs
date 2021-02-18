using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClothesManager : MonoBehaviour
{
    public Button headAdd;
    public Button headLess;
    public Button bodyAdd;
    public Button bodyLess;
    public Button legsAdd;
    public Button legsLess;
    public Button close;
    public Button dontSave;
    public Button stay;
    public Button save;
    public Text headIndex;
    public Text headOrigin;
    public Text bodyIndex;
    public Text bodyOrigin;
    public Text legsIndex;
    public Text legsOrigin;
    public Text warningText;
    public Image selectedHead;
    public Image selectedBody;
    public Image selectedLegs;
    public GameObject confWindow;

    int clothesPerOrigin;
    int numberOfOrigins;
    float tempHead;
    float tempBody;
    float tempLegs;
    GameObject tempGOHead;
    GameObject tempGOBody;
    GameObject tempGOLegs;

    [SerializeField]
    GameObject[] head;
    [SerializeField]
    GameObject[] body;
    [SerializeField]
    GameObject[] legs;

    enum ClothOrigin
    {
        Shop,
        Mexico,
        NewYork,
        Tokio
    }

    ClothOrigin hOrigin;
    ClothOrigin bOrigin;
    ClothOrigin lOrigin;

    void Awake()
    {
        clothesPerOrigin = GameController.clothesPerOrigin;
        numberOfOrigins = GameController.numberOfCities + 1;
    }

    void Start()
    {
        tempHead = GameController.headIndex;
        tempBody = GameController.bodyIndex;
        tempLegs = GameController.legsIndex;
        tempGOHead = GameController.head;
        tempGOBody = GameController.body;
        tempGOLegs = GameController.legs;
        headAdd.onClick.AddListener(AddHead);
        headLess.onClick.AddListener(LessHead);
        bodyAdd.onClick.AddListener(AddBody);
        bodyLess.onClick.AddListener(LessBody);
        legsAdd.onClick.AddListener(AddLegs);
        legsLess.onClick.AddListener(LessLegs);
        dontSave.onClick.AddListener(DontSave);
        stay.onClick.AddListener(Stay);
        close.onClick.AddListener(Close);
        save.onClick.AddListener(Save);
        UpdateUI();
    }

    void Update()
    {
        if(numberOfOrigins * clothesPerOrigin == (int) tempHead + 1)
        {
            headAdd.interactable = false;
        }
        else
        {
            headAdd.interactable = true;
        }

        if (numberOfOrigins * clothesPerOrigin == (int)tempBody + 1)
        {
            bodyAdd.interactable = false;
        }
        else
        {
            bodyAdd.interactable = true;
        }

        if (numberOfOrigins * clothesPerOrigin == (int)tempLegs + 1)
        {
            legsAdd.interactable = false;
        }
        else
        {
            legsAdd.interactable = true;
        }

        if(tempHead == -1)
        {
            headLess.interactable = false;
        }
        else
        {
            headLess.interactable = true;
        }

        if (tempBody == -1)
        {
            bodyLess.interactable = false;
        }
        else
        {
            bodyLess.interactable = true;
        }

        if (tempLegs == -1)
        {
            legsLess.interactable = false;
        }
        else
        {
            legsLess.interactable = true;
        }
    }

    void UpdateUI()
    {
        int heIndex = (int)(tempHead % clothesPerOrigin) + 1;
        int heOrigin = (int)tempHead / clothesPerOrigin;
        int boIndex = (int)(tempBody % clothesPerOrigin) + 1;
        int boOrigin = (int)tempBody / clothesPerOrigin;
        int leIndex = (int)(tempLegs % clothesPerOrigin) + 1;
        int leOrigin = (int)tempLegs / clothesPerOrigin;
        save.interactable = true;
        if (tempGOHead != null)
        {
            selectedHead.enabled = true;
            selectedHead.sprite = tempGOHead.GetComponent<Clothes>().clothData.Wearing;
            if (GameController.clothFound[heOrigin, 0, heIndex - 1] == false)
            {
                Debug.Log("Head:" + heOrigin + " " + heIndex);
                selectedHead.GetComponent<Image>().color = Color.black;
                save.interactable = false;
                StartCoroutine(ShowMessage("You dont own this yet"));
            }
            else
            {
                selectedHead.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            selectedHead.enabled = false;
        }

        if (tempGOBody != null)
        {
            selectedBody.enabled = true;
            selectedBody.sprite = tempGOBody.GetComponent<Clothes>().clothData.Wearing;
            if (GameController.clothFound[boOrigin, 1, boIndex - 1] == false)
            {
                Debug.Log("Body:" + boOrigin + " " + boIndex);
                selectedBody.GetComponent<Image>().color = Color.black;
                save.interactable = false;
                StartCoroutine(ShowMessage("You dont own this yet"));
            }
            else
            {
                selectedBody.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            selectedBody.enabled = false;
            
        }

        if (tempGOLegs != null)
        {
            selectedLegs.enabled = true;
            selectedLegs.sprite = tempGOLegs.GetComponent<Clothes>().clothData.Wearing;
            if (GameController.clothFound[leOrigin, 2, leIndex - 1] == false)
            {
                Debug.Log("Legs:" + leOrigin + " " + leIndex);
                selectedLegs.GetComponent<Image>().color = Color.black;
                save.interactable = false;
                StartCoroutine(ShowMessage("You dont own this yet"));
            }
            else
            {
                selectedLegs.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            selectedLegs.enabled = false;
        }

        headIndex.text = heIndex.ToString();
        if(tempHead == -1f)
        {
            headOrigin.text = "";
        }
        else
        {
            hOrigin = (ClothOrigin)heOrigin;
            switch (hOrigin)
            {
                case ClothOrigin.Shop:
                    headOrigin.text = "Shop";
                    break;
                case ClothOrigin.Mexico:
                    headOrigin.text = "Mexico";
                    break;
                case ClothOrigin.NewYork:
                    headOrigin.text = "New York";
                    break;
                case ClothOrigin.Tokio:
                    headOrigin.text = "Tokio";
                    break;
            }
        }

        bodyIndex.text = boIndex.ToString();
        if (tempBody == -1f)
        {
            bodyOrigin.text = "";
        }
        else
        {
            bOrigin = (ClothOrigin)boOrigin;
            switch (bOrigin)
            {
                case ClothOrigin.Shop:
                    bodyOrigin.text = "Shop";
                    break;
                case ClothOrigin.Mexico:
                    bodyOrigin.text = "Mexico";
                    break;
                case ClothOrigin.NewYork:
                    bodyOrigin.text = "New York";
                    break;
                case ClothOrigin.Tokio:
                    bodyOrigin.text = "Tokio";
                    break;
            }
        }

        legsIndex.text = leIndex.ToString();
        if (tempLegs == -1f)
        {
            legsOrigin.text = "";
        }
        else
        {
            lOrigin = (ClothOrigin)leOrigin;
            switch (lOrigin)
            {
                case ClothOrigin.Shop:
                    legsOrigin.text = "Shop";
                    break;
                case ClothOrigin.Mexico:
                    legsOrigin.text = "Mexico";
                    break;
                case ClothOrigin.NewYork:
                    legsOrigin.text = "New York";
                    break;
                case ClothOrigin.Tokio:
                    legsOrigin.text = "Tokio";
                    break;
            }
        }
    }

    void AddHead()
    {
        int index = (int)tempHead + 1;
        tempHead += 1;
        tempGOHead = head[index];
        UpdateUI();
    }

    void LessHead()
    {
        int index = (int)tempHead- 1;
        tempHead -= 1;
        if(index == -1)
        {
            tempGOHead = null;
        }
        else
        {
            tempGOHead = head[index];
        }
        UpdateUI();

    }

    void AddBody()
    {
        int index = (int)tempBody + 1;
        tempBody += 1;
        tempGOBody = body[index];
        UpdateUI();
    }

    void LessBody()
    {
        int index = (int)tempBody - 1;
        tempBody -= 1;
        if (index == -1)
        {
            tempGOBody = null;
        }
        else
        {
            tempGOBody = body[index];
        }
        UpdateUI();
    }

    void AddLegs()
    {
        int index = (int)tempLegs + 1;
        tempLegs += 1;
        tempGOLegs = legs[index];
        UpdateUI();
    }

    void LessLegs()
    {
        int index = (int)tempLegs - 1;
        tempLegs -= 1;
        if (index == -1)
        {
            tempGOLegs = null;
        }
        else
        {
            tempGOLegs = legs[index];
        }
        UpdateUI();
    }

    void Close()
    {
        confWindow.SetActive(true);
    }

    void Stay()
    {
        confWindow.SetActive(false);
    }

    void DontSave()
    {
        Destroy(gameObject);
    }

    void Save()
    {
        GameController.headIndex = tempHead;
        GameController.head = tempGOHead;
        GameController.bodyIndex = tempBody;
        GameController.body = tempGOBody;
        GameController.legsIndex = tempLegs;
        GameController.legs = tempGOLegs;
        GameObject zombLeadr = GameObject.Find("ZombieLeader");
        if(zombLeadr)
        {
            var zombLeaderSC = zombLeadr.GetComponent<ZombieLeaderMovement>();
            zombLeaderSC.LookUpdate();
        }
        Destroy(gameObject);
    }

    IEnumerator ShowMessage(string msg)
    {
        warningText.text = msg;
        warningText.enabled = true;
        yield return new WaitForSeconds(3f);
        warningText.enabled = false;
    }
}
