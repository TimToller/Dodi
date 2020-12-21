using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WaterLevelTest : MonoBehaviour
{
    public WaterTest ParticleScript;
    public bool UseYAsStartingPosition = true;
    public float endPos;
    public float startPos;
    private double procent;
    private float procentFl;

    private void Start()
    {
        if(UseYAsStartingPosition==true)
        {
            startPos = transform.position.y;
        } 
        else
        {
            transform.position = new Vector3(0, startPos, 0);
        }
    }
    void Update()
    {
        procent = ParticleScript.getScore();
        procentFl = (float)procent;
        procentFl = procentFl * ((endPos - startPos) / 100) + startPos;

        transform.position = new Vector3(0, procentFl, 0);
    }
}
