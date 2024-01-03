using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject [] droppedDead;
    public GameObject[] droppedDamage;
    public GameObject stain;
    private Transform collectables;

    private void Start()
    {
        collectables = GameObject.FindGameObjectWithTag("Collectables").transform;
    }

    public void Drop()
    {
        
        for (int i = 0; i< droppedDead.Length; i++)
        {
            Instantiate(droppedDead[i], transform.position, transform.rotation,collectables.transform);
            
        }
        
        
    }
    public void DamageDrop()
    {
        for (int i = 0; i < droppedDamage.Length; i++)
        {
           Instantiate(droppedDamage[i], transform.position, transform.rotation, collectables.transform);
        }

    }
    public void DropStain(Vector3 killerPos)
    {
        GameObject spawnedStain = Instantiate(stain, transform.position, transform.rotation, collectables.transform);
        spawnedStain.GetComponent<Stain>().RotateStain(killerPos);
        
        
    }
}
