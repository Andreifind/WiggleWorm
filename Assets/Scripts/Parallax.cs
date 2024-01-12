using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    float length, startpos;
    public float parallax;
    private Rigidbody2D rb;
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void Update()
    {
             float temp = transform.position.x;
        if (temp<startpos-length) transform.position=new Vector2 (transform.position.x+length,transform.position.y);

    }
    void FixedUpdate()
    {
        transform.Translate(Vector2.left * parallax * Time.deltaTime * 7);
    }
}
