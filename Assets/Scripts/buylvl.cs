using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class buylvl : MonoBehaviour
{
    public GameObject buylvlpn;
    public GameObject it;
    public TextMeshProUGUI txt;
    public int price;

    private void Start()
    {
        ChekLock();

    }
    public void BuyLVL(Sprite img)
    {
        buylvlpn.SetActive(true);
        buylvlpn.transform.GetChild(0).transform.GetChild(2).GetComponent<Image>().sprite = img;
        buylvlpn.transform.GetChild(0).transform.GetChild(0).GetComponent<buylvl>().it = transform.GetChild(1).gameObject;
        buylvlpn.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Convert.ToString(price);
    }

    public void BuyIt()// эта ф-ция висит на кнопке "buy"
    {
        if (PlayerPrefs.GetInt("coins") - Convert.ToInt32(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text) > -1)
        {
            it.SetActive(false);
            buylvlpn.SetActive(false);
            int x = PlayerPrefs.GetInt("coins") - Convert.ToInt32(transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            PlayerPrefs.SetInt("coins", x);
            txt.text = Convert.ToString(PlayerPrefs.GetInt("coins"));
            PlayerPrefs.SetInt("Button" + it.transform.parent.name, 0);
        }

        else
        {
           
        }
        
    }

    void ChekLock()
    {
        if (gameObject.tag =="lvlbtn")
        {
            if (PlayerPrefs.HasKey("Button" + gameObject.name))
            {
                transform.GetChild(1).gameObject.SetActive(Convert.ToBoolean(PlayerPrefs.GetInt("Button" + gameObject.name)));
            }
            else
            {
                PlayerPrefs.SetInt("Button" + gameObject.name, 1);
            }
        }
        
    }
}
