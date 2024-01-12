using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip hit;
    public AudioClip score;
    public AudioClip highscore;
    public AudioClip moveSound;
    public AudioClip tapSound;
    public AudioClip scoreGrowingSoundEffectMenu;
    public AudioClip coinPickup;
    private AudioSource source;
    private AudioSource ambient;
    private AudioSource coin;
    private AudioSource tap;


    void Start()
    {
        source = this.gameObject.AddComponent<AudioSource>();
        ambient = this.gameObject.AddComponent<AudioSource>();
        tap = this.gameObject.AddComponent<AudioSource>();
        coin = this.gameObject.AddComponent<AudioSource>();

        ambient.playOnAwake = false;
        ambient.priority -= 1;
        ambient.clip = moveSound;
        ambient.volume = 0.12f;
        ambient.loop = true;

        tap.volume = 0.2f;
        coin.volume = 0.2f;
    }

    // Update is called once per frame
    public void PlayHit()
    {
        source.PlayOneShot(hit);
    }
    public void PlayScore()
    {
        source.PlayOneShot(score);
    }
    public void PlayHighcore()
    {
        source.PlayOneShot(highscore);
    }

    public void StartMove()
    {
        if (ambient.isPlaying == false)
            ambient.Play();
        //ambient.PlayDelayed(1f);
        //ambient.PlayScheduled(1);

    }

    public void StopMove()
    {
        ambient.Stop();
    }

    public void PlayScoreGrowingSound()
    {
        //ambient.Stop();
        ambient.PlayOneShot(scoreGrowingSoundEffectMenu);
    }

    public void PlayTapSound()
    {
        tap.PlayOneShot(tapSound);
    }

    public void PlayCoinPickup()
    {
        coin.PlayOneShot(coinPickup);
    }
}
