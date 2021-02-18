using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothesShopController : MonoBehaviour
{
    public Button headButton;
    public Button bodyButton;
    public Button legsButton;
    public Button head1Button;
    public Button head2Button;
    public Button head3Button;
    public Button body1Button;
    public Button body2Button;
    public Button body3Button;
    public Button legs1Button;
    public Button legs2Button;
    public Button legs3Button;
    public Button acceptButton;
    public Button cancelButton;
    public Text warningText;
    public Text confText;
    public GameObject headItems;
    public GameObject bodyItems;
    public GameObject legsItems;
    public GameObject confWindow;

    int clothType;
    int clothIndex;
    int clothPrice;

    void Start()
    {
        headButton.onClick.AddListener(HeadButton);
        bodyButton.onClick.AddListener(BodyButton);
        legsButton.onClick.AddListener(LegsButton);
        head1Button.onClick.AddListener(Head1Button);
        head2Button.onClick.AddListener(Head2Button);
        head3Button.onClick.AddListener(Head3Button);
        body1Button.onClick.AddListener(Body1Button);
        body2Button.onClick.AddListener(Body2Button);
        body3Button.onClick.AddListener(Body3Button);
        legs1Button.onClick.AddListener(Legs1Button);
        legs2Button.onClick.AddListener(Legs2Button);
        legs3Button.onClick.AddListener(Legs3Button);
        acceptButton.onClick.AddListener(AcceptButton);
        cancelButton.onClick.AddListener(CancelButton);
    }

    void Update()
    {
        
    }

    void HeadButton()
    {
        headButton.interactable = false;
        bodyButton.interactable = true;
        legsButton.interactable = true;
        headItems.SetActive(true);
        bodyItems.SetActive(false);
        legsItems.SetActive(false);
    }

    void BodyButton()
    {
        headButton.interactable = true;
        bodyButton.interactable = false;
        legsButton.interactable = true;
        headItems.SetActive(false);
        bodyItems.SetActive(true);
        legsItems.SetActive(false);
    }

    void LegsButton()
    {
        headButton.interactable = true;
        bodyButton.interactable = true;
        legsButton.interactable = false;
        headItems.SetActive(false);
        bodyItems.SetActive(false);
        legsItems.SetActive(true);
    }

    void Head1Button()
    {
        warningText.text = "";
        CheckFound(0, 0, 150);
    }

    void Head2Button()
    {
        warningText.text = "";
        CheckFound(0, 1, 160);
    }

    void Head3Button()
    {
        warningText.text = "";
        CheckFound(0, 2, 170);
    }

    void Body1Button()
    {
        warningText.text = "";
        CheckFound(1, 0, 150);
    }

    void Body2Button()
    {
        warningText.text = "";
        CheckFound(1, 1, 160);
    }

    void Body3Button()
    {
        warningText.text = "";
        CheckFound(1, 2, 170);
    }

    void Legs1Button()
    {
        warningText.text = "";
        CheckFound(2, 0, 150);
    }

    void Legs2Button()
    {
        warningText.text = "";
        CheckFound(2, 1, 160);
    }

    void Legs3Button()
    {
        warningText.text = "";
        CheckFound(2, 2, 170);
    }

    void AcceptButton()
    {
        GameController.clothFound[0, clothType, clothIndex] = true;
        GameController.currency -= clothPrice;
        confWindow.SetActive(false);
    }

    void CancelButton()
    {
        confWindow.SetActive(false);
    }

    void CheckFound(int type, int index, int price)
    {
        if (GameController.clothFound[0, type, index] == true)
        {
            StartCoroutine(ShowMessage("You already have this cloth."));
        }
        else
        {
            if(GameController.currency >= price)
            {
                confWindow.SetActive(true);
                confText.text = "Sure you want to buy for $" + price + "?";
                clothType = type;
                clothIndex = index;
                clothPrice = price;
            }
            else
            {
                StartCoroutine(ShowMessage("You don't have enough coins =(."));
            }
        }
    }

    IEnumerator ShowMessage(string msg)
    {
        warningText.text = msg;
        warningText.enabled = true;
        yield return new WaitForSeconds(3f);
        warningText.enabled = false;
    }
}
