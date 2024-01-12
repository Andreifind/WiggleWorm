using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VoteScript : MonoBehaviour
{
    // Start is called before the first frame update

    public void LaterHater()
    {
        //int timesEnteredTheGame = PlayerPrefs.GetInt("iJoinedTheGame", 0);
        //PlayerPrefs.SetInt("iJoinedTheGame", timesEnteredTheGame - 1);
        //voteArea.SetActive(false);
        SceneManager.LoadScene("StartMenu2");
        //Close Vote
        //WE SET YOU AS NO VOTED ;)
    }

    public void Voted()
    {
        PlayerPrefs.SetInt("voted", 1);
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.hydragamez.wiggleworm");
        SceneManager.LoadScene("StartMenu2");
        //OPEN LINK
        //voteArea.SetActive(false);
        //Close Vote
        
    }
}
