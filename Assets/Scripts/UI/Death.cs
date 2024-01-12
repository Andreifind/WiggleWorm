using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject deathPanel;
    public GameObject placeholderPannel;
    public GameObject[] rewardPanels;  //0 default //1 cool
    public Text scoreText;
    public Text highScoreText;
    public Text totalCoinsText;
    public GameObject highScorePannelGO;
    public Sounds sounds;

    private bool increseScore = true;
    private int auxScore = 0;
    private int auxHighScore = 0;
    private int auxTotalCoins = 0;
    private float scoreIncrementTime = 0.075f;
    // Update is called once per frame
    IEnumerator scoreEffect(int score)
    {
        if (increseScore==true && auxScore<=score)
        {
            scoreText.transform.GetChild(0).GetComponent<Text>().text = auxScore.ToString();
            auxScore++;
            if (sounds != null)
                sounds.PlayScoreGrowingSound();
            yield return new WaitForSeconds(scoreIncrementTime);
            yield return scoreEffect(score);
        }
        yield return null;

    }

    IEnumerator highScoreEffect(int score)
    {
        if (increseScore == true && auxHighScore <= score)
        {
            highScoreText.transform.GetChild(0).GetComponent<Text>().text = auxHighScore.ToString();
            auxHighScore++;
            yield return new WaitForSeconds(scoreIncrementTime/3);
            yield return highScoreEffect(score);
        }
        yield return null;
    }

    IEnumerator totalCoinsEffect(int coins)
    {
        if (increseScore == true && auxTotalCoins <= coins)
        {
            totalCoinsText.transform.GetChild(0).GetComponent<Text>().text = auxTotalCoins.ToString();
            auxTotalCoins++;
            yield return new WaitForSeconds(scoreIncrementTime / 5);
            yield return totalCoinsEffect(coins);
        }
        yield return null;
    }


    public void Dead(int score, int coins,Achivements achivements)
    {
        //SET GET DEATHS
        int deaths = PlayerPrefs.GetInt("deaths", 0);
        deaths++;
        PlayerPrefs.SetInt("deaths", deaths);
        increseScore = true;
        //SET ACHIVEMENTS IF NEEDED
        if (achivements!=null)
        {
            if (Social.localUser.authenticated==true || RunningManager.isGooglePlayConected==true)
            {
                //For deaths
                //I USE IF IN CASE SCORE >=
                if (deaths>=1)
                    Social.ReportProgress(GPGSIds.achievement_there_is_a_first_time_for_everything, 100.0, null);

                if (deaths >= 10)
                    Social.ReportProgress(GPGSIds.achievement_dont_give_up,100.0,null);

                if (deaths >= 100)
                    Social.ReportProgress(GPGSIds.achievement_come_on_keep_going, 100.0, null);

                if (deaths >= 500)
                    Social.ReportProgress(GPGSIds.achievement_poor_worm, 100.0, null);

                if (deaths >= 1000)
                    Social.ReportProgress(GPGSIds.achievement_what_did_he_do_to_deserve_this, 100.0, null);

                if (deaths >= 10000)
                    Social.ReportProgress(GPGSIds.achievement_there_once_was_a_worm,100.0,null);

                //For score
                if (score >= 10)
                    Social.ReportProgress(GPGSIds.achievement_nice_try, 100.0, null);

                if (score >= 20)
                    Social.ReportProgress(GPGSIds.achievement_you_are_still_a_newbie,100.0,null);

                if (score >= 50)
                    Social.ReportProgress(GPGSIds.achievement_yeah_boy, 100.0, null);

                if (score >= 80)
                    Social.ReportProgress(GPGSIds.achievement_youve_gone_too_far, 100.0, null);

                if (score >= 100)
                    Social.ReportProgress(GPGSIds.achievement_master_of_the_game, 100.0, null);

                if (score >= 1000)
                    Social.ReportProgress(GPGSIds.achievement_hacker,100.0,null);



                if (score>0)
                    Social.ReportScore(score, GPGSIds.leaderboard_leaderboard, null);
            }
        }
        //StartCoroutine(toggleButtons());

        deathPanel.SetActive(true);
        auxScore = 0;
        auxHighScore = 0;


        //READ FROM FILE HIGHTSCORE
        int highscore = PlayerPrefs.GetInt("score", 0);

        scoreText.text = "SCORE:";
        StartCoroutine(scoreEffect(score));
        //scoreText.text += " " + score.ToString();
        //scoreText.transform.GetChild(0).GetComponent<Text>().text = score.ToString();
        //Set new Highscore
        if (score>highscore /*&& score!=69420*/)
        {
            highscore = score;
            PlayerPrefs.SetInt("score", score);
            highScorePannelGO.SetActive(true);
            /*if (RunningManager.isGooglePlayConected == true)
                Social.ReportScore(score, GPGSIds.leaderboard_leaderboard, null);*/
        }
        /*else if(score==69420)
        {
            score=0;
            highscore=0;
            PlayerPrefs.SetInt("score", score);
        }*/

        //MAKE THE SAME FOR HIGHSCORE
        highScoreText.text = "HIGHSCORE:";
        StartCoroutine(highScoreEffect(highscore));
        //highScoreText.text += " " + highscore.ToString();


        //PLACEHOLDER AREA
        if (score>=100)
        {
            GameObject newTab = Instantiate(rewardPanels[rewardPanels.Length-1], placeholderPannel.transform.position,Quaternion.identity,placeholderPannel.transform.parent);
            newTab.transform.SetAsFirstSibling();
            Destroy(placeholderPannel.gameObject);
            placeholderPannel = newTab;
        }
        else if (score>=20 && score<=49) //Newbie
        {
            GameObject newTab = Instantiate(rewardPanels[1], placeholderPannel.transform.position, Quaternion.identity, placeholderPannel.transform.parent);
            newTab.transform.SetAsFirstSibling();
            Destroy(placeholderPannel.gameObject);
            placeholderPannel = newTab;
        }
        else if (score>=50&& score<=79) //Cowboy
        {
            GameObject newTab = Instantiate(rewardPanels[2], placeholderPannel.transform.position, Quaternion.identity, placeholderPannel.transform.parent);
            newTab.transform.SetAsFirstSibling();
            Destroy(placeholderPannel.gameObject);
            placeholderPannel = newTab;
        }
        else if (score >=80&& score<=99) //Scarf
        {
            GameObject newTab = Instantiate(rewardPanels[3], placeholderPannel.transform.position, Quaternion.identity, placeholderPannel.transform.parent);
            newTab.transform.SetAsFirstSibling();
            Destroy(placeholderPannel.gameObject);
            placeholderPannel = newTab;
        }

        else
        {
            GameObject newTab = Instantiate(rewardPanels[0], placeholderPannel.transform.position, Quaternion.identity, placeholderPannel.transform.parent);
            newTab.transform.SetAsFirstSibling();
            Destroy(placeholderPannel.gameObject);
            placeholderPannel = newTab;
        }

        //COINS AREA
        int globalCoins = PlayerPrefs.GetInt("coins", 0);
        globalCoins += coins;
        PlayerPrefs.SetInt("coins", globalCoins);
        StartCoroutine(totalCoinsEffect(globalCoins));
        //Debug.Log("TOTAL COINS: " + globalCoins);

    }

    public void Respawn()
    {
        Piatra[] pietre = GameObject.FindObjectsOfType<Piatra>();
        Coin[] coins = GameObject.FindObjectsOfType<Coin>();
        for (int i =0;i< pietre.Length;i++)
        {
            Destroy(pietre[i].gameObject);
        }
        for (int i = 0;i<coins.Length;i++)
        {
            Destroy(coins[i].gameObject);
        }

        deathPanel.SetActive(false);

        //Terminate all corutines
        increseScore = false;


    }

    public void ForceUpdateCoins()
    {
        totalCoinsText.transform.GetChild(0).GetComponent<Text>().text = PlayerPrefs.GetInt("coins", 0).ToString();
    }
}
