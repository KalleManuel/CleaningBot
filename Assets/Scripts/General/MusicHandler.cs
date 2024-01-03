using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] song;
    public AudioSource musicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer.clip = song[0];
        musicPlayer.Play();
    }

 public void PlaySong(int songNumber)
    {
        musicPlayer.clip = song[songNumber];
        musicPlayer.Play();

    }
}
