using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class add : MonoBehaviour
{
    GameObject building;
    GameObject Prefab_Identity;
    public float x, y, z;
    bool permision = false;
    DateTime time;
    GameObject sound;
    public int n;// element's number
  
    private void Start()
    {
        transform.parent.transform.parent.GetComponent<SavetoJSON>().Save();
        Prefab_Identity = GameObject.FindGameObjectWithTag("PrefID");
        building = GameObject.FindGameObjectWithTag("building");
        sound = GameObject.FindGameObjectWithTag("Sound");
    }

    void FixedUpdate()
    {
    
        if (permision)
        {
            if (DateTime.Now >= time)
            {
                Destroyer();
            }
        }
    }
    private void OnMouseDown()
    {
        permision = true;
        time = DateTime.Now.AddSeconds(2);
    }
    private void OnMouseEnter()
    {
    //    gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
    private void OnMouseUp()
    {
        permision = false;
    }
    private void OnMouseExit()
    {
      //  gameObject.GetComponent<Renderer>().material.color = Color.white;
        permision = false;
    }

    private void OnMouseUpAsButton()
    {
        if((GameObject.FindGameObjectWithTag("Finish").GetComponent<fild>().l-1 == transform.position.x)
            || (GameObject.FindGameObjectWithTag("Finish").GetComponent<fild>().l - 1 == transform.position.z)
            || (GameObject.FindGameObjectWithTag("Finish").GetComponent<fild>().l2 + 1 == transform.position.z) 
            || (GameObject.FindGameObjectWithTag("Finish").GetComponent<fild>().l2 + 1 == transform.position.x)) 
        { 
            GameObject.FindGameObjectWithTag("Finish").GetComponent<fild>().create = true;
        }

        sound.GetComponent<AudioSource>().Play();
        Instantiate(Prefab_Identity.GetComponent<PrefIdentity>().Inst_Pref, new Vector3(transform.position.x + x/10, transform.position.y + y/10, transform.position.z + z/10), Quaternion.identity, building.transform);
    }


    void Destroyer()
    {
        Destroy(transform.parent.gameObject);
    }

   
}
