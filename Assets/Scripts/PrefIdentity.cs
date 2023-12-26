using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefIdentity : MonoBehaviour
{
    public GameObject Inst_Pref;
   
    public void CgengePref(GameObject prefab)
    {
        Inst_Pref = prefab;
       
    }
    public void Up(Button btn)
    {
        btn.transform.SetSiblingIndex(0);
    }
}
