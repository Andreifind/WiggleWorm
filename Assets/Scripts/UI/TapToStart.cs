using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapToStart : MonoBehaviour
{
    // Update is called once per frame
    private bool pressed = false;
    private Animator animator;
    public Transform worm;
    public Animator[] otherObjects;
    private bool freeze = false;
    private void Start()
    {
        animator = this.GetComponent<Animator>();
        freeze = true;
        StartCoroutine(unfreezeAfter());
    }

    

    IEnumerator unfreezeAfter()
    {
        yield return new WaitForSeconds(1.2f);
        freeze = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& freeze==false&& pressed==false)
        {
            pressed = true;
            //StartCoroutine(PlayAnim());
            if (worm!=null)
            {
                worm.GetComponent<WormController>().Touch();
            }
            else
                SceneManager.LoadScene(0);
            //Tranzitia e apelata de ramuta StartTranzition() cu referinta aparent
        }
    }    
    public void StartTranzition()
    {
        pressed = true;
        StartCoroutine(PlayAnim());
    }
    IEnumerator PlayAnim()
    {
        animator.SetTrigger("pressed");
        for (int i = 0; i < otherObjects.Length;i++)
        {
            otherObjects[i].SetTrigger("fade");
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);
    }
}


