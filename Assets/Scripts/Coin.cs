using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float speed=2.0f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.Translate(Vector2.up * speed * Time.deltaTime);
        rb.velocity = new Vector2(0.0f, speed);
        rb.bodyType = RigidbodyType2D.Kinematic;
        if (transform.position.y>7)
            Destroy(gameObject);
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>().coins+=1;
        Destroy(gameObject);
    }*/
}
