using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fild : MonoBehaviour
{
    public GameObject panel;
    public GameObject addBlock;
    public float l = 9;// last position
    public int l2 = -1;
    public bool create = false;
    void Awake()
    {
        Strt();
    } 

    void Strt()
    {
        for(int i = 0; i < l; i++)
        {
            for(int k=0; k<l; k++)
            {
                Instantiate(panel, new Vector3(k , 0, i), Quaternion.identity, transform);
            }
        }
    }

    private void Update()
    {
        if (create)
        {
            expandFild();
        }
    }

    void expandFild()
    {
        for (int i = l2; i < l + 1; i++)
        {
            Instantiate(panel, new Vector3(i, 0, l), Quaternion.identity, transform);
        }
        for (int i = l2; i < l; i++)
        {
            Instantiate(panel, new Vector3(l, 0, i), Quaternion.identity, transform);
        }

        for (int i = l2; i < l; i++)
        {
            Instantiate(panel, new Vector3(i, 0, l2), Quaternion.identity, transform);
        }

        for (int i = l2+1; i < l; i++)
        {
            Instantiate(panel, new Vector3(l2, 0, i), Quaternion.identity, transform);
        }


        l2--;
        l++;
        create = false;
    }


}
