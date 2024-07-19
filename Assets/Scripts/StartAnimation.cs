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
        public Sprite speakerSprite;
        public string responder;
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
        responderImage.sprite = null;

        StartCoroutine(PlayDialog());
    }

    IEnumerator PlayDialog()
    {
        for (int i = 0; i < dialog.Length; i++)
        {
            speakerImage.sprite = dialog[i].speakerSprite;
            responderImage.sprite = dialog[i].responderSprite;

            if (dialog[i].speaker != "")
            {
                
                speakerText.text = dialog[i].speaker;
                yield return new WaitForSeconds(dialogTime);
                speakerText.text = "";
                
                if (dialog[i].responder != "")
                {

                    responderText.text = dialog[i].responder;
                    yield return new WaitForSeconds(dialogTime);
                    responderText.text = "";
                }
            }
            else if (dialog[i].responder != "")
            {
                responderText.text = dialog[i].responder;
                yield return new WaitForSeconds(dialogTime);
                responderText.text = "";
            }

        }

        PlayGame();
    }

    public void PlayGame()
    {
        ch.PlayLevelOne();
    }

}
