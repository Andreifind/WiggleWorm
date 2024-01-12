using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthscroll : MonoBehaviour
{
    float length, startpos;
    Rigidbody2D rb;
    public float parallax;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        startpos = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }
    void Update()
    {
            float temp = transform.position.y;
        if (temp-6>startpos-length) transform.position=new Vector2 (transform.position.x,transform.position.y-length);
        //Debug.Log(temp);
        //Debug.Log(startpos);
        //Debug.Log(length);
    }
    void FixedUpdate()
    {
        //transform.Translate(Vector2.up * parallax * Time.deltaTime);
        rb.velocity = new Vector2(0.0f, 2f);
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
