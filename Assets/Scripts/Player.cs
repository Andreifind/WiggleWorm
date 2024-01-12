using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //[SerializeField]
    private float swingSpeed = 1;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    public float swingSpeedIncrement = 0.0030f;
    private bool obstacleHit = false;

    Animator animator;

    private int score = 0;
    public int Score
    {
        get { return score; }
    }

    private bool alive = true;
    public bool Alive
    {
        get { return alive; }
    }


    Rigidbody2D body;

    public SceneHandler sceneHandler;
    private Score scoreText;
    private Sounds sound;
    private float oldSpeed = -1;
    private float afterAddSpeed = 0.25f;

    int highscore;
    bool highscoreHit=false;
    public GameObject highscoreEffect;
   
    private int sessionCoins=0;
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        animator=this.GetComponent<Animator>();
        sound = this.GetComponent<Sounds>();
        scoreText = sceneHandler.GetScore();
        highscore = PlayerPrefs.GetInt("score", 0);
        //body.velocity = new Vector2(speed * swingSpeed, 0);
        sound.StartMove();
    }

    //Phzc
    private void FixedUpdate()
    {
        RayCastHitCheckScore();
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTap();
        /*if (Input.GetKeyDown("space"))  //cheat score
        {
            score+=20;
        }
        if (Input.GetKeyDown("escape"))  //reset highscore
        {
            score=69420;
        }*/
    }

    private void CheckTap()
    {

         if (Input.GetMouseButtonDown(0))
        {
            if (alive)
            {
                speed = -speed;
                sound.PlayTapSound();
                //Movement();
                Animation();
            }

         }
    }
    private void Movement()
    {
        if (alive)
        {
            
            body.position +=  new Vector2(speed * swingSpeed ,0f)*  Time.deltaTime;
            ///ASTEA 2 SUNT EGALE FFS!
            //body.velocity = new Vector2(speed * swingSpeed, 0);
        }
        else
            body.velocity = Vector2.zero;
        swingSpeed += swingSpeedIncrement;
        //body.velocity = Vector2.zero;
    }
    private void Animation()
    {
        if (speed > 0)
        {
            animator.ResetTrigger("lookright");
            animator.ResetTrigger("lookleft");
            animator.SetTrigger("lookright");
        }
        else
        {
            animator.ResetTrigger("lookright");
            animator.ResetTrigger("lookleft");
            animator.SetTrigger("lookleft");
        }
        swingSpeed = 1;
    }
    private void RayCastHitCheckScore()
    {
        float colliderRange = 8f;
        Vector3 offsetPoz = new Vector3(colliderRange, 0f, 0f);

        RaycastHit2D ray = Physics2D.Raycast(transform.position - offsetPoz, Vector2.right,colliderRange*2);
        Debug.DrawRay(transform.position - offsetPoz, offsetPoz * 2,Color.red);

        if (!ray)
            obstacleHit = false;
        if (obstacleHit == false && ray && alive)
        {
            obstacleHit = true;
            score++;
            //SoundManager.PlaySound ("score");
            sound.PlayScore();
            if (scoreText!=null)
            {
                scoreText.ChangeScore(score);
            }
            if (score > highscore && highscoreHit == false && highscore>0)
            {
                sound.PlayHighcore();
                GameObject effect = Instantiate(highscoreEffect, new Vector3(3.17f, 2.78f,-4f), Quaternion.identity);
                Destroy(effect, 3f);
                highscoreHit = true;
            }
        }

        #region DumpConde
        //RaycastHit2D hitLeft = Physics2D.Raycast(transform.position- offsetPoz, transform.InverseTransformDirection(Vector2.left), colliderRange);
        //RaycastHit2D hitRight = Physics2D.Raycast(transform.position + offsetPoz, transform.InverseTransformDirection(Vector2.right), colliderRange);


        //Debug.DrawRay(transform.position- offsetPoz, transform.InverseTransformDirection(Vector2.left)* colliderRange, Color.red);
        //Debug.DrawRay(transform.position+ offsetPoz, transform.InverseTransformDirection(Vector2.right)* colliderRange, Color.red);


        /*if (!hitLeft&& !hitRight)
        {
            obstacleHit = false;
        }

        if ((hitLeft||hitRight) && obstacleHit == false)
        {
            obstacleHit = true;
            score++;
            Debug.Log(score);
        }*/



        /*if (hitLeft && obstacleHit==false)
        {
            obstacleHit = true;
            Debug.Log("Lovit Stanga"+hitLeft.collider.name);
        }
        if (hitRight && obstacleHit == false)
        {
            obstacleHit = true;
            Debug.Log("Lovit Dreapta" + hitRight.collider.name);
        }*/
        /*if (hitLeft||hitRight)
        {
            
        }*/
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alive && other.gameObject.GetComponent<Coin>()!=null)
        {
            sessionCoins++;
            sound.PlayCoinPickup();
            Destroy(other.gameObject);
        }

        if (alive && (other.gameObject.GetComponent<Piatra>() != null  || other.gameObject.GetComponent<Border>() != null))
        {

            //SoundManager.PlaySound ("hit");
            body.velocity = Vector2.zero;
            sound.PlayHit();
            alive = false;
            sound.StopMove();
            oldSpeed = Time.timeScale;
            Time.timeScale = 1;
            Debug.Log("O murit rama, gata.");
            sceneHandler.runningManager.PlayTimedAdd();
            sceneHandler.runningManager.PlayScoreAdd(score);
            sceneHandler.ActivateDeath(this.score, this.sessionCoins);
        }
    }

    public void Respawn()
    {
        RunningManager.roundWatchedAdds = false;
        swingSpeed = 1;
        //speed = Mathf.Abs(speed) + 0.5f;
        Time.timeScale = oldSpeed + afterAddSpeed;  //AFTER ADD SPEED
        //body.velocity = new Vector2(speed * swingSpeed, 0);
        sound.StartMove();
        alive = true;

    }

}
