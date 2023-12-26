using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavetoJSON : MonoBehaviour
{

    ToJson tj = new ToJson();
    public List<GameObject> gj;
    string str;
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        str = PlayerPrefs.GetString("kreativ");
        if (PlayerPrefs.HasKey(str))
        {
           Load();
        }
        else
        {
            string json = JsonUtility.ToJson(tj);
            PlayerPrefs.SetString(str, json);
            tj = JsonUtility.FromJson<ToJson>(PlayerPrefs.GetString(str));
        }
       
    }

    public void Save()
    {
        if (PlayerPrefs.HasKey(str) && tj.nmb!=null &&tj.number !=null)
        {
            tj.number.Clear();
            tj.nmb.Clear();
        }

        for (int i = 1; i< transform.childCount; i++)
        {
           tj.number.Add(transform.GetChild(i).transform.position);
           tj.nmb.Add(transform.GetChild(i).transform.GetChild(0).GetComponent<add>().n);
        }

        string json = JsonUtility.ToJson(tj);
        PlayerPrefs.SetString(str, json);

    }

    void Load()
    {
        tj = JsonUtility.FromJson<ToJson>(PlayerPrefs.GetString(str));
        for (int z=0; z < tj.number.Count; z++)
        {
            GameObject go = gj[tj.nmb[z]];

            Instantiate(go, tj.number[z], Quaternion.identity, transform);
        }
   
    } 


    private class ToJson
    {
        public List<Vector3> number;
        public List<int> nmb;

    }
}
