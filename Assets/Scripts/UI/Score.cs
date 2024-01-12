using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    // Start is called before the first frame update
    private Text scoreText;
    //private int score = 0;
     void Start()
    {
        scoreText = this.GetComponent<Text>();
    }
    public void ChangeScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }
}
