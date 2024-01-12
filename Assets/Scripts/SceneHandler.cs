using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{

    // Start is called before the first frame update
    Spawner spawner = null;
    public GameObject player;
    Vector3 startPosition;
    public Score score;
    Death death = null;
    Achivements achivements = null;
    public Text startingInCountdownText;
    public RunningManager runningManager;
    SkinManager skinManager;

    private string currentSkin;

    public Button[] deathSceneButtons;
    private float pauzeFor = 1.8f;

    void Start()
    {
        spawner = this.GetComponent<Spawner>();
        //score = this.GetComponent<Score>();
        death = this.GetComponent<Death>();
        achivements = this.GetComponent<Achivements>();

        skinManager = player.GetComponent<SkinManager>();
        startPosition = player.transform.position;
        SetSkin();
    }

    // Update is called once per frame




    public Spawner GetSpawner()
    {
        return spawner;
    }
    public Score GetScore()
    {
        return score;
    }
    
    public Death GetDeath()
    {
        return death;
    }

    #region Buttons
    //ONLY BUTTONS
    public void Restart()
    {
        /*if (runningManager != null)
        {
            runningManager.PlayTimedAdd();
        }*/

        if (RunningManager.freeze==false)
        {
            runningManager.HideBanner();
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }

    public void RestartFromAdd()
    {
        if (RunningManager.freeze == false && runningManager.IsRewardAddReady()==true)
        {
            if (runningManager != null)
                runningManager.PlayFullVideoAdd();


            runningManager.HideBanner();

            
            player.transform.position = startPosition;
            death.Respawn();

            //Starting in 3..2..1
            if (startingInCountdownText!=null)
            {
                startingInCountdownText.gameObject.SetActive(true);
                StartCoroutine(RespawnFromAddComplete(3));
            }
            else
            {
                player.GetComponent<Player>().Respawn();
                spawner.enabled = true;
                score.enabled = true;
            }

            /*player.GetComponent<Player>().Respawn();
            spawner.enabled = true;
            score.enabled = true;*/

            //death.Respawn();
        }
    }
    IEnumerator RespawnFromAddComplete(int ct)
    {
        if (ct>0)
        {
            startingInCountdownText.text = "Starting in\n"+ct;
            yield return new WaitForSeconds(1f);
            ct--;
            yield return RespawnFromAddComplete(ct);
        }
        startingInCountdownText.gameObject.SetActive(false);
        player.GetComponent<Player>().Respawn();
        spawner.enabled = true;
        score.enabled = true;
        yield return null;
    }

    public void Home()
    {
        if (RunningManager.freeze == false)
        {
            runningManager.HideBanner();
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }


    public void Leaderboard()
    {
        if (RunningManager.isGooglePlayConected==true)
        {
            Social.ShowLeaderboardUI();
        }
    }

    public void Shop()
    {
        //THIS WILL EXECUTE AFTER SHOP BUTTON IS PRESSED
        //runningManager.HideBanner();
        //Time.timeScale = 1;
        //SceneManager.
        SceneManager.LoadScene("ShopV2",LoadSceneMode.Additive);
        //SceneManager.SetActiveScene(Scene.);
    }
    #endregion

    IEnumerator toggleButtons()
    {
        for (int i = 0; i < deathSceneButtons.Length; i++)
        {
            //ColorBlock tmp = deathSceneButtons[i].colors;
            //tmp.normalColor = new Color(tmp.normalColor.r, tmp.normalColor.g, tmp.normalColor.b, tmp.normalColor.a == 1 ? 0.2f : 1f);
            //deathSceneButtons[i].colors = tmp;
            deathSceneButtons[i].interactable = !deathSceneButtons[i].interactable;
            //deathSceneButtons[i].enabled = !deathSceneButtons[i].enabled;

            //deathSceneButtons[i].color= c;
        }
        yield return null;
    }

    public void ActivateDeath(int scorePoints, int collectedSessionCoins)
    {
        StartCoroutine(toggleButtons());
        runningManager.ShowBanner();

        RunningManager.freeze = true;
        StartCoroutine(stopFreeze(pauzeFor));

        spawner.enabled = false;
        score.enabled = false;
        death.Dead(scorePoints, collectedSessionCoins, achivements);
    }

    IEnumerator stopFreeze(float pauzeFor)
    {
        yield return new WaitForSeconds(pauzeFor);
        RunningManager.freeze = false;
       StartCoroutine( toggleButtons());
    }

    public void SetSkin()
    {
        currentSkin = PlayerPrefs.GetString("currentSkin", "Default");
        skinManager.SetSkin(currentSkin, player);
    }

    public string GetSkin()
    {
        return currentSkin;
    }

    public void ForceCoinUpdate()
    {
        death.ForceUpdateCoins();
    }

}
