using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInImage : MonoBehaviour
{
    private Image img;
    private Color col;

    [SerializeField]
    private float fadeSpeed = 1;

    [SerializeField]
    private float waitBefore = 0;
    [SerializeField]
    private float imgDuration = 0;

    [SerializeField, Range(0, 1)]
    private float transparancy; 
    
    [SerializeField]
    private bool fadeIn, fadeOut, destroyAfterViewing, reset;


    // Start is called before the first frame update
    void Start()
    {

        ResetFunction();

    }

    public void ResetFunction()
    {
        img = GetComponent<Image>();

        if (fadeIn)
        {
            col = img.color;
            col.a = 0;
            img.color = col;

        }

        else if (!fadeIn && fadeOut)
        {
            col = img.color;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            reset = false;
        }

        if (fadeIn)
        {
            if (waitBefore >= 0f)
            {
                waitBefore -= Time.unscaledDeltaTime;
            }
            else
            {
                if (col.a <= (transparancy - 0.01f))
                {
                    col.a += fadeSpeed * Time.unscaledDeltaTime;
                    img.color = col;
                   
                }
                else
                {
                    
                    col.a = transparancy;
                    img.color = col;
                    fadeIn = false;
                }
            }
        }
        else if (fadeOut)
        {
            if (imgDuration >= 0f)
            {
                imgDuration -= Time.unscaledDeltaTime;
            }
            else
            {
                if (col.a >= 0.01f)
                {
                    col.a -= fadeSpeed * Time.unscaledDeltaTime;
                    img.color = col;
                
                }
                else 
                {
                    col.a = 0f;
                    img.color = col;
                    fadeOut = false;
                }
                
            }
        }
        else if (destroyAfterViewing && !fadeIn && !fadeOut)
        {
            Destroy(gameObject);
        }

        if (gameObject.activeSelf == false)
        {
            if (!reset)
            {
                ResetFunction();
                reset = true;
            }
            
        }

    }
}
