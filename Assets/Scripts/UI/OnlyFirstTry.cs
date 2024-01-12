using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlyFirstTry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int timesEnteredTheGame = PlayerPrefs.GetInt("iJoinedTheGame", 0);
        int  firstTime = PlayerPrefs.GetInt("firstTime", 0);

        //IF NOT ANDROID NO VOTE FOR YOU!
        if (Application.platform != RuntimePlatform.Android)
            PlayerPrefs.SetInt("voted", 1);

        //VOTE AREA
        int fiveStar = PlayerPrefs.GetInt("voted", 0);


        if (fiveStar == 0 && firstTime == 1 && timesEnteredTheGame % 2 == 0)
        {
            //Show Vote
            SceneManager.LoadScene("VotingScene");

        }

        if (firstTime<=0)
        {
            PlayerPrefs.SetInt("firstTime", 1);
            SceneManager.LoadScene(2);
        }



        //END
        timesEnteredTheGame++;
        PlayerPrefs.SetInt("iJoinedTheGame", timesEnteredTheGame);

    }

    
    // Update is called once per frame
}
