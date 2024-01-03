using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject SpawnObject, eyespawner, projectile;
    private EnemyMovement enemyMovment;
    private bool goLeft, goRight;
    public float speed;
    private float timer; 


    private void Start()
    {
        enemyMovment = GetComponent<EnemyMovement>();

        enemyMovment.target = null;

       
        goLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (goLeft)
        {
            transform.Translate(-1 * Time.deltaTime * speed, 0, 0);
        }

        if (goRight)
        {
            transform.Translate(1 * Time.deltaTime * speed, 0, 0);
        }
        
    }

    public void SpawnEnemies()
    {
        goRight = false;
        goLeft = false;

        for (int i = 0; i<spawners.Length; i++)
        {
            Instantiate(SpawnObject, spawners[i].transform.position, SpawnObject.transform.rotation);
        }
    }

    public void Attack()
    {
        if (enemyMovment.target == null)
        {
            enemyMovment.target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else 
        {
            enemyMovment.target = null;
            goLeft = true;
        }
        

    }

    public void ChangeDirection()
    {
        if (goLeft)
        {
            goLeft = false;
            goRight = true;
            return;
        }
        else
        {
            goRight = false;
            goLeft = true;
            return;
        }
    }

    public void ToggleStandStill()
    {
        
    }

public void launchProjectile()
    {
        Instantiate(projectile, eyespawner.transform.position, eyespawner.transform.rotation);
    }
}
