using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallForAttention : MonoBehaviour
{
    public GameObject human;
    [SerializeField]
    private HumanMovment humanMovment;
    [SerializeField]
    private PlayerCompanions playerCompanions;
    public EndGame endgame;


    [SerializeField]
    private float range = 1.5f;

    private void Start()
    {
        endgame = GameObject.FindGameObjectWithTag("EndTrigger").GetComponent<EndGame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject != null)
            {
                if (!endgame.bossFightOn)
                {
                    collision.gameObject.GetComponent<EnemyMovement>().target = transform;
                }
            }
           
            
        }

        if (collision.gameObject.tag == "Player")
        {
            if (!humanMovment.beenFound)
            {
                humanMovment.beenFound = true;
                playerCompanions = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCompanions>();
                playerCompanions.AddCompanion(human);
                GetComponent<CircleCollider2D>().radius = range;
            }
           
            
        }
    }
}
