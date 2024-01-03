using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSuitScript : MonoBehaviour
{
    public GameObject helmet;
    public GameObject spaceSuitLocation;

    private bool helmetOn;
    public bool inBossfight;

    public string suitTag;
    public EndGame endGame;

    [SerializeField]
    private HumanMovment movement;

    private void Start()
    {
        endGame = GameObject.FindGameObjectWithTag("EndTrigger").GetComponent<EndGame>();
        spaceSuitLocation = GameObject.FindGameObjectWithTag(suitTag);

        helmet.SetActive(false);
    }

    private void Update()
    {
        if (endGame.bossDefeated)
        {
            if (inBossfight)
            {
                if (!helmetOn)
                {
                    if (movement.target != spaceSuitLocation.transform)
                    {
                        movement.target = spaceSuitLocation.transform;
                    }
                }
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!inBossfight)
        {
            if (collision.gameObject.tag == "EndTrigger")
            {
                inBossfight = true;
            }
        }
       
        if (collision.gameObject.tag == suitTag) 
        {
            if (endGame.bossDefeated)
            {
                helmet.SetActive(true);
                helmetOn = true;
                movement.target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
                Destroy(collision.gameObject);
            }
        }
    }
}
