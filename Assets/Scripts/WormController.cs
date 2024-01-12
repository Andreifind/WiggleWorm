using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormController : MonoBehaviour
{
    Animator animator;
    public Transform transformMenuReference;
    // Start is called before the first frame update
    void Start()
    {
        animator=this.GetComponent<Animator>();
    }

    // Update is called once per frame
    public void activ()
    {
        transformMenuReference.GetComponent<TapToStart>().StartTranzition();
        Destroy(gameObject);
    }
    public void Touch()
    {
        animator.SetTrigger("istouch");
    }

}
