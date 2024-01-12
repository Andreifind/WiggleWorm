using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnter : MonoBehaviour
{
    // Update is called once per frame
    public GameObject image;
    private bool pressed = false;
    private Animator animator;
    private void Start()
    {
        //Time.timeScale = 0;
        image.SetActive(true);
        animator = this.GetComponent<Animator>();
        StartCoroutine(PlayAnim());
    }
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0) && pressed == false)
        {
            pressed = true;
            StartCoroutine(PlayAnim());
        }*/
    }

    IEnumerator PlayAnim()
    {
        animator.SetTrigger("activate");
        yield return new WaitForSeconds(1);
        //Time.timeScale = 1;
        //Start Game
        //SceneManager.LoadScene(1);
    }
}
