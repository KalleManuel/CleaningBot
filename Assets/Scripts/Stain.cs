using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain : MonoBehaviour
{
    //public Vector3 killerPosition;
  //  public  Transform killerAim;
  
    public GameObject [] stain;
    

    private void Start()
    {
        
    }

    public void RotateStain(Vector3 killerPosition)
    {
       // killerAim.position = new Vector3(killerPosition.x,killerPosition.y,0);
        

        Vector2 targetDirection = (killerPosition - transform.position).normalized;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle - 180));
        Debug.Log(q);
        transform.localRotation = q;

        int random = Random.Range(0, stain.Length); 
      
        //float randomAngle = Random.Range(0, 259);

        //transform.localRotation = Quaternion.Euler(0, 0, randomAngle);

        Instantiate(stain[random], transform.position, transform.rotation,transform);
        
    }
}