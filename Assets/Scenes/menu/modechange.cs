using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class modechange : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private void Start()
    {
        ChekCoins();
        SAL();
    }

    public GameObject surv, creative, shop, panelBuy, buylvlpn;
    public TextMeshProUGUI coins;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (x == 0 )
            {

                if (eventData.delta.x > 0)
                {
                    creative.SetActive(false);
                    surv.SetActive(true);
                }


                else
                {
                    surv.SetActive(false);
                    creative.SetActive(true);
                }
            }
               
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
    int l;
    public void LoadLVL(int i)
    {
       
            SceneManager.LoadScene(i);
        
       
    }
    int x = 0;
    int y = 0;
    public void shopPanel()
    {
        if ( (!shop.activeSelf) || (shop.transform.GetSiblingIndex() != transform.childCount - 4) ) 
        {
            shop.SetActive(true);
            shop.transform.SetSiblingIndex(transform.childCount-4);
        }
        else
        {
            shop.SetActive(false);
        }
     
    }

 
    public void Buy()
    {
        panelBuy.SetActive(true);
        panelBuy.transform.GetChild(0).transform.GetChild(2).GetComponent<Image>().sprite = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<Image>().sprite;
    }

    public void BuyLVlbtn(GameObject go)// кнопка "купить"
    {
        go.SetActive(false);
    }

    public void Cancel()
    {
        panelBuy.SetActive(false);
        buylvlpn.SetActive(false);
    }

    public GameObject lvlbtn;
    public void AddLVL()
    {
       if(PlayerPrefs.GetInt("coins") > 100)
        {
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 100);
            coins.text = Convert.ToString(PlayerPrefs.GetInt("coins"));
            int i = PlayerPrefs.GetInt("LVLCount") + 1;
            lvlbtn.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(i);
            Instantiate(lvlbtn, transform.GetChild(1).transform.GetChild(0).transform.GetChild(0));
            PlayerPrefs.SetInt("LVLCount", i);
        }
      
    }

    void ChekCoins()
    {
        if(!PlayerPrefs.HasKey("coins"))
        {
            PlayerPrefs.SetInt("coins", 20);
            coins.text = Convert.ToString(PlayerPrefs.GetInt("coins"));
        }
        else
        {
            coins.text = Convert.ToString(PlayerPrefs.GetInt("coins"));
        }

    }


    void SAL()// Start ad lvl
    {
        if (PlayerPrefs.HasKey("LVLCount"))
        {
            for (int i = 0; i < PlayerPrefs.GetInt("LVLCount"); i++)
            {
                lvlbtn.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(i + 1);
                Instantiate(lvlbtn, transform.GetChild(1).transform.GetChild(0).transform.GetChild(0));
            }
        }
        
    }

}
