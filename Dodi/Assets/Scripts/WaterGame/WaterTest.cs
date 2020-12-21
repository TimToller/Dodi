using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaterTest : MonoBehaviour
{
    public ParticleSystem part;
    public Text tex;
    public String ButtonToPress;
    private double score = 0;
    [Range(-300.0f,300.0f)]
    public float speed;

    private void Start()
    {
        tex.text = score.ToString();
        part.Stop();
    }
    void Update()
    {
        if (score > 100.0f) { score = 100.0f; part.Stop(); }
        else if (score < 0.0f) { score = 0.0f; }
        if (Input.GetKeyDown(ButtonToPress))
        {
            if (score < 100.0f)
            {
                part.Play();
            }
        }
        if (Input.GetKeyUp(ButtonToPress))
        {
            part.Stop();
        }
        tex.text = score.ToString();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (score <= 100 && score >= 0)
        {
            score += speed / 10 * Time.deltaTime;
            score = Math.Round(score, 2);
        }
    }
    public double getScore()
    {
        score = Math.Round(score, 2);
        return score;
    }

}
