using NVIDIA.Flex;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterController : MonoBehaviour
{
    public FlexContainer flex;
    public string InputButton;
    public Text text;
    public float timeInSeconds;

    private FlexSourceActor actor;
    private float time;
    private float procent;
    private bool isFull = false;
    public bool pressed = false;
    private bool usingText = false;

    void Start()
    {
        actor = GetComponent<FlexSourceActor>();
        if(flex.maxParticles < timeInSeconds*1000)
        {
            Debug.LogError("ERROR, PLEASE INCREASE maxParticles IN FLEX CONTAINER!");
            timeInSeconds = flex.maxParticles / 1000;
        }
        if(text!=null)
        {
            usingText = true;
        }
    }
    private void Update()
    {
        if (time >= timeInSeconds)
        {
            isFull = true;
        }
        if (Input.GetKeyDown(InputButton) && isFull==false)
        {
            pressed = true;
        }
        if (Input.GetKeyUp(InputButton) || isFull == true)
        {
            pressed = false;
        }
        if (pressed == true && isFull == false)
        {
            time += Time.deltaTime;
        }
        actor.isActive = pressed;

        procent = (float)Math.Round(time * 100 / timeInSeconds, 2);
        if (procent > 100) { procent = 100; }
        if (usingText)
        {
            text.text = procent.ToString() + "%";
        }
    }
    public float getProcent()
    {
        return procent;
    }
}
