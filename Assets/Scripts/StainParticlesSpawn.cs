using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainParticlesSpawn : MonoBehaviour
{
  
    public GameObject[] stain;
    

    private void Start()
    {
        StartCoroutine(SpawnStains());
    }
    private IEnumerator SpawnStains()
    {
        
        int row = 1;
        int changeRow = 0;

        for (int i = 0; i < stain.Length; i++)
        {
            stain[i].SetActive(true);
            changeRow++;

            if (changeRow >= row)
            {
                changeRow = 0;
                row++;
                yield return new WaitForSeconds(0.05f);

            }

        }

       
    }
}