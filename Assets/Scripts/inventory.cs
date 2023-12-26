using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class inventory : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public GameObject panel, panel2;
    Vector2 startpos;
    GameObject au;
    void Start()
    {
        InDown();
        au = GameObject.FindGameObjectWithTag("Sound");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) < Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.y > 0)
            {
                InUp();
            }
            else
            {
                InDown();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
      
    }


    List<Transform> gjl;
    public void InUp()
    {
       
        panel.SetActive(true);
        for (int i = 0; i < panel2.transform.GetChild(0).childCount; )
        {
            panel2.transform.GetChild(0).GetChild(0).parent = panel.transform.GetChild(0);
        }
        panel2.SetActive(false);

    }

    public void InDown()
    {
        panel2.SetActive(true);
        for (int i = 0; i < panel.transform.GetChild(0).childCount;)
        {
            panel.transform.GetChild(0).GetChild(0).parent = panel2.transform.GetChild(0);
        }
        panel.SetActive(false);
    }

    public RectTransform btn;
    public void Menu(RectTransform pn)
    {   
       
        pn.anchoredPosition = new Vector2(-pn.anchoredPosition.x, 0);
        btn.Rotate(0,0, btn.transform.rotation.z+180);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Sound(GameObject x)
    {
        if (x.activeSelf == false)
        {
            au.GetComponent<AudioSource>().volume = 0;
            x.SetActive(true);
        }
        else
        {
            au.GetComponent<AudioSource>().volume = 0.16f;
            x.SetActive(false);
        }

    }
    public GameObject x,y;
    public void ModeChange()
    {

        if (x.activeSelf == true)
        {
            y.SetActive(true);
            x.SetActive(false);
        }

        else
        {
            y.SetActive(false);
            x.SetActive(true);
        }
            

    }
}
