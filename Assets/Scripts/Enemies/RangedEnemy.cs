using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour
{
    public GameObject projectile, spawnPoint;
    public float range;

    public GameObject player;
    public NavMeshAgent agent;
    public Animator anim;
    public bool shooting = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shooting)
        {
            if (player != null)
            {
                if (Vector2.Distance(player.transform.position, transform.position) < range)
                {

                    agent.speed = 0;
                    shooting = true;
                    anim.SetBool("shoot", true);
                    anim.SetBool("move", false);
                }
            }
           
        }
    }

    public void Shoot()
    {
        Vector2 targetDirection =   transform.position- player.transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg -180;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));

        spawnPoint.transform.localRotation = Quaternion.Slerp(transform.localRotation, q, 200);

        Instantiate(projectile, spawnPoint.transform.position, spawnPoint.transform.rotation);
    }

    public void StartMoving()
    {
        anim.SetBool("shoot", false);
        anim.SetBool("move", true);
        agent.speed = 1;
        shooting = false;
    }
}
