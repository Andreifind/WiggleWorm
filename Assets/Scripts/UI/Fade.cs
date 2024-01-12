using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private Text thisText;
    private Color thisColor;
    public float fadeSpeed=1.25f;
    // Start is called before the first frame update
    void Start()
    {
        thisText = this.GetComponent<Text>();
        thisColor = thisText.color;
    }

    // Update is called once per frame
    void Update()
    {
        thisColor.a = Math.Abs(Mathf.Sin(Time.time * fadeSpeed));
        this.GetComponent<Text>().color = thisColor;
    }
}
