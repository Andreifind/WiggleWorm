using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Shop : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    GameObject sc;
    public GameObject confirmBuyPannel;
    bool panelActive;
    Skin selectedSkinFromShop;
    Skin currentActiveSkin;
    Button[] buttons;
    private void Start()
    {
        sc = GameObject.Find("SceneController");

        buttons = confirmBuyPannel.GetComponentsInChildren<Button>(true);
        ResetButtons();
        confirmBuyPannel.SetActive(false);
        panelActive = false;


        //Find and select selected Skin
        string currentSkinName = PlayerPrefs.GetString("currentSkin", "Default");
        selectedSkinFromShop = null;
        currentActiveSkin = null;
        Skin[] skinsFromShop = GameObject.FindObjectsOfType<Skin>();
        for (int i = 0; i < skinsFromShop.Length;i++)
        {
            if (skinsFromShop[i].SkinName==currentSkinName)
            {
                currentActiveSkin = skinsFromShop[i];
                break;
            }
        }
        SetCurrentSkinFrame(null);
        //selectedSkinFromShop = PlayerPrefs.GetString("currentSkin", "Default");
    }
    private void SetCurrentSkinFrame(Skin otherSkin)
    {
        Image selectedSkinImage = currentActiveSkin.transform.GetChild(1).GetComponent<Image>();
        selectedSkinImage.color = Color.green;
        if (otherSkin!=null)
        {
            Image selectedOtherSkinImage = otherSkin.transform.GetChild(1).GetComponent<Image>();
            selectedOtherSkinImage.color = Color.white;
        }
    }
    public void CloseShop()
    {
        SceneManager.UnloadSceneAsync("ShopV2");
    }

    public void SelectSkin(Skin selectedSkin)
    {
        if (panelActive==false && sc!=null)
        {
            selectedSkinFromShop = selectedSkin;
            string currentSkinName = sc.GetComponent<SceneHandler>().GetSkin();
            //Debug.Log("currentSkinName: "+currentSkinName+" selectedkin: "+selectedSkin.SkinName)
            if (selectedSkin.Owned && selectedSkin.SkinName!= currentSkinName)
            {
                //////Change Skin -> Change underText to selected
                //////OR CHANGE UNDER FRAME


                PlayerPrefs.SetString("currentSkin", selectedSkin.SkinName);
                if (sc != null)
                    sc.GetComponent<SceneHandler>().SetSkin();

                //SKIN CHANGE IN GAME FRAME
                Skin tmp = currentActiveSkin;
                currentActiveSkin = selectedSkin;
                SetCurrentSkinFrame(tmp);
                //Make chenar green on new skjn
                //Remove green chenar from old skin
                //SetCurrentSkinFrame(currentSkinName);

            }
            else
            {
                if (selectedSkin.Owned==false)
                {
                    //Send Try To buy message  X
                    //Open new message tab  X
                    confirmBuyPannel.SetActive(true);
                    panelActive = true;

                    Text panelText = confirmBuyPannel.GetComponentInChildren<Text>();
                    panelText.text = panelText.text.Replace("{SkinName}", selectedSkin.SkinName);
                    panelText.text = panelText.text.Replace("{cost}", selectedSkin.Price.ToString());
                }
                //Selected itself?
               
            }
        }
        else
        {
            //Nika
            //Debug.Log("ELSE!");
        }

    }
 
    public void BuySkin()
    {
        //SHOW ARE YOU SURE YOU WANT TO BUY?   -> THIS IS MADE IN SELECTSKIN()
        if (selectedSkinFromShop!=null)
        {
            int globalCoins = PlayerPrefs.GetInt("coins", 0);
            if (selectedSkinFromShop.Price <= globalCoins)
            {
                globalCoins -= selectedSkinFromShop.Price;
                PlayerPrefs.SetInt("coins", globalCoins);
                PlayerPrefs.SetInt(selectedSkinFromShop.SkinName, 1);
                selectedSkinFromShop.Owned = true;
                //CHANGE COINS UNDER THE SKIN WITH OWNED X
                //Make Icon offline X
                selectedSkinFromShop.UpdateUnderText();
                selectedSkinFromShop.UnCover();

                //Close UI X - Maybe add a green text with Succesfully bought X
                Text panelText = confirmBuyPannel.GetComponentInChildren<Text>();
                panelText.color = Color.green;
                panelText.text = "Success!";
                sc.GetComponent<SceneHandler>().ForceCoinUpdate();
                //Hide little buttons show one big button         X
                //Button[] buttons;
                //buttons = confirmBuyPannel.GetComponentsInChildren<Button>();
                SelectSkin(selectedSkinFromShop);
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].gameObject.name.Contains("Yes") || (buttons[i].gameObject.name.Contains("No")))
                        buttons[i].gameObject.SetActive(false);
                    if (buttons[i].gameObject.name.Contains("CloseButton"))
                        buttons[i].gameObject.SetActive(true);
                }
            }
            else
            {
                //Show now enough money - change text      X
                Text panelText = confirmBuyPannel.GetComponentInChildren<Text>();
                panelText.color = Color.red;
                panelText.text = "Not enough coins!";
                //Hide little buttons show one big button         X
                //Button[] buttons;
                //buttons = confirmBuyPannel.GetComponentsInChildren<Button>();
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].gameObject.name.Contains("Yes") || (buttons[i].gameObject.name.Contains("No")))
                        buttons[i].gameObject.SetActive(false);
                    if (buttons[i].gameObject.name.Contains("CloseButton"))
                        buttons[i].gameObject.SetActive(true);
                }
            }
        }
        
    }

    public void CloseBuySkin()
    {


        //Close the new message tab  X
        panelActive = false;
        confirmBuyPannel.SetActive(false);

        //Reset form X
        //Text X
        Text panelText = confirmBuyPannel.GetComponentInChildren<Text>();
        panelText.text = "Are you sure you want to buy\n{SkinName} for {cost} coins ?";
        panelText.color = Color.white;
        //panelText.text = panelText.text.Replace(selectedSkinFromShop.SkinName, "{SkinName}");
        //panelText.text = panelText.text.Replace(selectedSkinFromShop.Price.ToString(), "{cost}");

        //Buttons X
        //buttons = confirmBuyPannel.GetComponentsInChildren<Button>();
        ResetButtons();

    }
    private void ResetButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i].gameObject.name.Contains("Yes") || (buttons[i].gameObject.name.Contains("No")))
                buttons[i].gameObject.SetActive(true);
            if (buttons[i].gameObject.name.Contains("CloseButton"))
                buttons[i].gameObject.SetActive(false);
        }
    }
}
