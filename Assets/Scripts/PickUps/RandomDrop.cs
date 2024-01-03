using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    public GameObject [] DropPool;
    public int minDrop, maxDrop;
    private Transform parentTransform;

    private void Start()
    {
        parentTransform = GameObject.FindGameObjectWithTag("Collectables").transform;
    }

    public void Drop()
    {
        int randomAmount = Random.Range(minDrop, maxDrop);

        for(int i = 0; i< randomAmount; i++)
        {
            Instantiate(DropPool[Random.Range(0,DropPool.Length)], transform.position, transform.rotation,parentTransform.transform);
            
        }
        
    }
   

    
}
