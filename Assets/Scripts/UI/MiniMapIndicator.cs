using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIndicator : MonoBehaviour
{
    public GameObject indicator;
    public GameObject target;

    public Renderer rd;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rd = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if (rd.isVisible == false)
            {
                if (indicator.activeSelf == false)
                {
                    indicator.SetActive(true);
                }

                Vector2 direction = target.transform.position - transform.position;


                RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, direction);

                for (int i = 0; i < ray.Length; i++)
                {


                    if (ray[i].collider.gameObject.name == "CamBox")
                    {
                        indicator.transform.position = ray[i].point;
                    }
                }

            }
            else
            {
                if (indicator.activeSelf == true)
                {
                    indicator.SetActive(false);
                }
            }
        }
        
    } 
}
