using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadCreativeLvl : MonoBehaviour
{
    public void Loadclvl()
    {
        PlayerPrefs.SetString("kreativ", "kreativ" + transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
        SceneManager.LoadScene(32);
       
    }
}
