using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperVacuum : MonoBehaviour
{
    private GameObject vacuum;
    private Item_VacumBoost vf;
    // Start is called before the first frame update
    void Start()
    {
        vacuum = GameObject.FindGameObjectWithTag("Vacum");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject c = collision.gameObject; 

        if(c.gameObject.tag == "Player")
        {
            vf = vacuum.GetComponent<Item_VacumBoost>();
            vacuum.GetComponent<CircleCollider2D>().radius = 30f;
            vf.SuperVac = true;
            Destroy(gameObject);
            
        }
    }
}
