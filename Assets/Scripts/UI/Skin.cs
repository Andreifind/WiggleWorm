using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private string skinName;
    [SerializeField]
    private int price;
    [SerializeField]
    private bool owned;

    public string SkinName
    {
        set { skinName = value; }
        get { return skinName; }
    }

    public int Price
    {
        set { price = value; }
        get { return price; }
    }

    public bool Owned
    {
        set { owned = value; }
        get { return owned; }
    }
    void Start()
    {
        //Default Skin always unlocked - Haha nice try
        if (SkinName == "Default")
        {
            PlayerPrefs.SetInt(SkinName, 1);
            Owned = true;
        }



        Owned = PlayerPrefs.GetInt(SkinName, 0) == 1 ? true : false;
        if (Owned==false)
        {
            UpdatePriceNotOwned();
            Cover();
        }
        else
            UpdateUnderText();
    }
    public void UpdateUnderText()
    {
        if (Owned && SkinName != "Default")
        {
            Text skinText = this.GetComponentInChildren<Text>();
            skinText.text = "Owned";
            skinText.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void UpdatePriceNotOwned()
    {
        Text skinText = this.GetComponentInChildren<Text>();
        skinText.text = Price.ToString();
    }

    public void Cover()
    {
        Image img = this.gameObject.GetComponent<Image>();
        img.color = Color.black;
    }

    public void UnCover()
    {
        Image img = this.gameObject.GetComponent<Image>();
        img.color = Color.white;
    }
    // Update is called once per frame
}
