using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlastPickUp : MonoBehaviour
{
    public GameObject blast;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(blast, transform.position, transform.rotation);

            Destroy(gameObject);

        }
    }
}
