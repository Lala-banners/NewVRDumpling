using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    private bool isLightOn = false;
    private string lightName = "Point Light";
    private string emissionColour = "_EmissionColour";
    private float offIntensity = 0.0f, onIntensity = 3.0f;
    //public Button onOffButton;
    //private GameObject player;

    private void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        
        lights = GetComponentsInChildren<Light>();
        Debug.Log(lights.Length);

        foreach (Light light in lights)
        {
            if (light.name == lightName)
            {
                light.intensity = offIntensity;
            }
        }
        
        GetComponent<Renderer>().material.SetColor(emissionColour, Color.black);
        
        //onOffButton.onClick.AddListener((LightOn));
    }

    public void LightOn()
    {
        isLightOn = !isLightOn;

        if (isLightOn)
        {
            foreach (Light light in lights)
            {
                if (light.name == lightName)
                {
                    light.intensity = onIntensity;
                }
            }
            GetComponent<Renderer>().material.SetColor(emissionColour, Color.HSVToRGB(0.1533f, 0.1497f, 0.6549f));
        }
        else
        {
            foreach (Light light in lights)
            {
                if (light.name == lightName)
                {
                    light.intensity = offIntensity;
                }
            }
            GetComponent<Renderer>().material.SetColor(emissionColour, Color.black);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //Debug.Log("Hit player");
            LightOn();
        }
    }*/
}
