using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeInText : MonoBehaviour
{
    private TMP_Text tex;
    private Color col;

    [SerializeField]
    private float waitBefore = 0;
    [SerializeField]
    private float textDuration = 0;
    [SerializeField]
    private float fadeSpeed = 1;

    [SerializeField]
    public bool fadeIn, fadeOut, destroyAfterReading;


    // Start is called before the first frame update
    void Start()
    {
        tex = GetComponent<TextMeshProUGUI>();
        

        if (fadeIn)
        {
            col = tex.color;
            col.a = 0;
            tex.color = col;
        }

        if (!fadeIn && fadeOut)
        {
            col = tex.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            if (waitBefore >= 0f)
            {
                waitBefore -= Time.unscaledDeltaTime;
            }
            else
            {
                if (col.a <= 0.99f)
                {
                    col.a += fadeSpeed * Time.unscaledDeltaTime;
                    tex.color = col;
                }
                else
                {
                   
                    col.a = 1f;
                    tex.color = col;
                    fadeIn = false;
                    
                }
            }
        } 
        else if (fadeOut)
        {
            if(textDuration >= 0f)
            {
                textDuration -= Time.unscaledDeltaTime;
            }
            else
            {
                if (col.a >= 0.01f)
                {
                    col.a -= fadeSpeed * Time.unscaledDeltaTime;
                    tex.color = col;

                }

                else 
                {
                    col.a = 0;
                    tex.color = col;
                    fadeOut = false;
                }
                
            } 
        }
        else if (destroyAfterReading && !fadeIn && !fadeOut)
        {
            Destroy(gameObject);
        }         
    }
}
