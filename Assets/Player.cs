using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;

    public static PlayerInventory playerInventory;
    public static PlayerExperience playerXP;
    public static Transform playerPosition;

    private void Awake()
    {
        player = this;
        playerInventory = gameObject.GetComponentInChildren<PlayerInventory>();
        playerXP = gameObject.GetComponentInChildren<PlayerExperience>();
        playerPosition = gameObject.GetComponentInChildren<Transform>();
    }
    
}
