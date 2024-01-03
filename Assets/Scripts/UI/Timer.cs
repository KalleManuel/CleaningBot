using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private GameOver playerStatus;

    public float timer;
    public int seconds;
    public int minutes;
    public string minutestext;
    public string secondtext;

    public TextMeshProUGUI timertext;
    // Start is called before the first frame update
    void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameOver>();
        timer = 0;
        seconds = 0;
        minutes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerStatus.dead)
        {
            timer += Time.deltaTime;

            seconds = ((int)timer);

            if (seconds == 60)
            {
                minutes++;
                timer = 0;
            }

            if (seconds < 10)
            {
                secondtext = "0" + seconds;
            }
            else secondtext = "" + seconds;

            if (minutes < 10)
            {
                minutestext = "0" + minutes;
            }
            else minutestext = "" + minutes;

            timertext.text = minutestext + ":" + secondtext;
        }
    }
}
