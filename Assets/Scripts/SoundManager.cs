using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip score,hit,highscore;
	static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        score = Resources.Load<AudioClip> ("score");
		hit = Resources.Load<AudioClip> ("hit");
        highscore = Resources.Load<AudioClip> ("highscore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void PlaySound (string clip)
		{
			switch (clip)
			{
				case "score":
				audioSrc.PlayOneShot(score);
				break;
                case "hit":
				audioSrc.PlayOneShot(hit);
				break;
                case "highscore":
                audioSrc.PlayOneShot(highscore);
                break;
            }
        }

}
