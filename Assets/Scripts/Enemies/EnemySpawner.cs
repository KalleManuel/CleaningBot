using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public bool spawn;

    private void Start()
    {
        spawn = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.tag != "EndTrigger")
        {
            if (collision.gameObject.tag == "Walkable")
            {

                spawn = true;
            }
        }
        */

        if (collision.gameObject.tag == "EndTrigger")
        {
            spawn = false;
        }
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.tag == "Walkable")
        {
          
            spawn = false;
        }
        */
        if (collision.gameObject.tag == "EndTrigger")
        {
            spawn = true;
        }

    }
}
