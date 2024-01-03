using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] imageProgress;
    
    [SerializeField]
    private Color transparacy;
    private Color orgCol;
    private Image imageSlot;

    [SerializeField]
    private GameObject life;
    
    public bool alive;

    // Start is called before the first frame update
    void Start()
    {
       
        imageSlot = GetComponent<Image>();
        orgCol = imageSlot.color;
        imageSlot.color = transparacy;
        alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {

            if (life.transform.localScale.y > 0.8f)
            {
                imageSlot.sprite = imageProgress[0];
            }

            else if (life.transform.localScale.y > 0.5f)
            {
                imageSlot.sprite = imageProgress[1];
            }

            else if (life.transform.localScale.y > 0.001f)
            {
                imageSlot.sprite = imageProgress[2];
            }
            else
            {
                IsDead();
                alive = false;
                
            }
        }
    }

    public void FollowPlayer()
    {
        imageSlot.color = orgCol;
        
    }

    public void IsDead()
    {
        imageSlot.sprite = imageProgress[3];
    }
}
