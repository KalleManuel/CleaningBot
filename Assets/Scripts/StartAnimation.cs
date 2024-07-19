using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartAnimation : MonoBehaviour
{
    [System.Serializable]
    public class TheDialog
    {
        public string speaker;
        public float speakerTime;
        public Sprite speakerSprite;
        public string responder;
        public float responderTime;
        public Sprite responderSprite;
    }

    public TheDialog[] dialog;

    public SceneHandler ch;
    public float dialogTime = 3;

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI responderText;
    public Image speakerImage;
    public Image responderImage;

    private void Start()
    {
        speakerText.text = "";
        responderText.text = "";
        speakerImage.sprite = null;
        

        StartCoroutine(PlayDialog());
    }

    IEnumerator PlayDialog()
    {
        for (int i = 0; i < dialog.Length; i++)
        {

            if (dialog[i].speaker != "")
            {
                speakerImage.sprite = dialog[i].speakerSprite;
                speakerText.text = dialog[i].speaker;
                dialogTime = dialog[i].speakerTime;
                yield return new WaitForSeconds(dialogTime);
                speakerText.text = "";
                
                if (dialog[i].responder != "")
                {
                    responderImage.sprite = dialog[i].responderSprite;
                    responderText.text = dialog[i].responder;
                    dialogTime = dialog[i].responderTime;
                    yield return new WaitForSeconds(dialogTime);
                    responderText.text = "";
                }
            }
            else if (dialog[i].responder != "")
            {
                speakerImage.sprite = dialog[i].speakerSprite;
                responderImage.sprite = dialog[i].responderSprite;
                responderText.text = dialog[i].responder;
                dialogTime = dialog[i].responderTime;
                yield return new WaitForSeconds(dialogTime);
                responderText.text = "";
            }
            else
            {
                speakerImage.sprite = dialog[i].speakerSprite;
                responderImage.sprite = dialog[i].responderSprite;
            }

        }

        PlayGame();
    }

    public void PlayGame()
    {
        ch.PlayLevelOne();
    }

}
