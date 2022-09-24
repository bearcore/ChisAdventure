using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class JumpBar : MonoBehaviour


{
    public Image sliderBackgroud;
    private Slider mySlider;
    private int launchPowerLevel;
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        mySlider = this.GetComponent<Slider>();
        mySlider.value = 0;
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()

    {
        if (Input.GetKey("space"))
        {

            ActivateSlider();
        }
        if (Input.GetKeyDown("space"))
        {
            ResetSlider();
        }
        if (Input.GetKeyUp("space"))
        {
            PrintPowerLevel();
        }
    }
    private void PrintPowerLevel()
    {
        text.SetText("Dein Launch Powerlevel ist {0}", launchPowerLevel);
        Debug.Log(launchPowerLevel);
        Debug.Log(text.text);
    }
    private void ResetSlider()
    {
        mySlider.value = 0;
    }
    private void ActivateSlider()
    {
        if ( mySlider.value <= 0.17f)
        {
            Debug.Log("kleiner 1");
            mySlider.value += 0.2f*Time.deltaTime;
            launchPowerLevel = 0;
            sliderBackgroud.color = Color.white;
           

        } else if (mySlider.value <= 0.33)
        {
            Debug.Log("kleiner 2");
            mySlider.value += 0.3f*Time.deltaTime;
            launchPowerLevel = 1;
            sliderBackgroud.color = Color.gray;

        } else if (mySlider.value <= 0.5)
        {
            Debug.Log("kleiner 3");
            mySlider.value += 0.5f*Time.deltaTime;
            launchPowerLevel = 2;
            sliderBackgroud.color = Color.cyan;

        } else if (mySlider.value <= 0.5)
        {
            Debug.Log("kleiner 4");
            mySlider.value += 0.7f*Time.deltaTime;
            launchPowerLevel = 3;
            sliderBackgroud.color = Color.blue;
        }
        else if (mySlider.value <= 0.66)
        { 
            Debug.Log("kleiner 5");
        mySlider.value += Time.deltaTime;
            launchPowerLevel = 4;

            sliderBackgroud.color = Color.green;


        } else if (mySlider.value <= 0.83)
        {
            Debug.Log("kleiner 6");
            launchPowerLevel = 5;
            mySlider.value += 2f*Time.deltaTime;

            sliderBackgroud.color = Color.yellow;
        } else if (mySlider.value < 0.99)
        {
            Debug.Log("6");
            mySlider.value += 3f*Time.deltaTime;
            launchPowerLevel = 6;
            sliderBackgroud.color = Color.red;

        } else if (mySlider.value >= 0.99)
        {

            Debug.Log("Überspannt");
            mySlider.value = 0;
            launchPowerLevel = 0;
        }

        

    }
}
