using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    private Transform target;
    public string targetTag;
    private GameOver playerStats;
    

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerStats.dead)
        {
            Vector2 targetDirection = target.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));

            transform.localRotation = Quaternion.Slerp(transform.localRotation, q, 200);
        }
    
    }
}
