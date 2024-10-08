using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : MonoBehaviour
{
    [Header("General Item Information")]
    public int weaponID;
    public string itemName;
    public Sprite itemImage;
    public int itemLevel;
    public int itemMaxLevel;
    public string[] levelDescription;
    public bool activated;
    public bool available;
    public bool startItem;
    public bool maxLevelReached;
 
    
    public enum Type { Default, Attack, Defense}
    public Type type = Type.Default;



   

}

