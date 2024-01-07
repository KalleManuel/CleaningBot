using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanHoover : MonoBehaviour
{

    public float range;
    public GameObject player;
    private PlayerInventory inventory;
  

    void Start()
    {  

        inventory = player.GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, range);
        
        foreach (Collider2D collider2D in colliderArray)
        {
            if (collider2D.TryGetComponent<Cleanable>(out Cleanable objectToClean))
            {
                inventory.AddCoin(objectToClean.value);
                Destroy(objectToClean.gameObject);
            }
        }
    }
}
