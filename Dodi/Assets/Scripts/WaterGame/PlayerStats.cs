using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string playerNumber;
    public WaterController controller;
    public bool isHolding;
    public float procent;
    public string username;
    public GameObject player;

    private void Start()
    {
        player.name = PlayerPrefs.GetString(playerNumber);
    }
    void Update()
    {
        isHolding = controller.pressed;
        procent = controller.getProcent();

        PlayerPrefs.SetFloat(username, procent);
    }
    
}
