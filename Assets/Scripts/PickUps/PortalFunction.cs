using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalFunction : MonoBehaviour
{
    public SpriteRenderer SpriteToAppear;
    
    // Start is called before the first frame update
    void Start()
    {
        SpriteToAppear.enabled = false;
    }
public void MakeAppear()
    {
        SpriteToAppear.enabled = true;
    }

    public void DestroyPortal()
    {
        Destroy(gameObject);
    }
}
