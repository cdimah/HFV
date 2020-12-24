using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Slider Slider;       //Create an object of type Slider public to be defined in Unity UI.
    public Color Low;           //Color at which the Health bar will turn as it gets empty.
    public Color High;          //Color at which the Health bar will be when full.
    public Vector3 Offset;      //Position in reference to the Zombie or Bystander.

    public void SetHealth(float health, float maxHealth)  //Function to update the healthbar change from the owner.
    {
        Slider.gameObject.SetActive(health < maxHealth);
        Slider.value = health;
        Slider.maxValue = maxHealth;
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(Low, High, Slider.normalizedValue);
    }


    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);     //Just adapting the position every frame.
    }
}
