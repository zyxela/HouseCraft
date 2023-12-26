using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Android;

public class CUMeralookAT : MonoBehaviour
{
    Vector2 tch1, tch2, stch1, stch2; //"stch"(x) - start touch
    float sdistance, distance;
    public float speed;


    void Update()
    {

        //Zoom
        if (Input.touchCount == 2)
        {
           
            tch1 = Input.touches[0].position;
            tch2 = Input.touches[1].position;
            distance = Distance(tch1.x, tch2.x, tch1.y, tch2.y, distance);
            if ((stch1  == Vector2.zero) && (stch2 == Vector2.zero))
            {
                stch1 = Input.touches[0].position;
                stch2 = Input.touches[1].position;
                sdistance = Distance(stch1.x, stch2.x, stch1.y, stch2.y, sdistance);
            }
            if (sdistance > distance )
            {
                Camera.main.orthographicSize += 1f * speed;
            }
            if (sdistance < distance && Camera.main.orthographicSize > 3)
            {
                Camera.main.orthographicSize -= 1f * speed;
            }



        }
        if(Input.touchCount == 0)
        {
            stch1 = Vector2.zero;
            stch2 = Vector2.zero;
        }

    }

    float Distance(float n1, float n2, float n3, float n4, float dist)
    {
        dist = Convert.ToSingle(Math.Sqrt(Math.Pow(Math.Max(n1,n2)- Math.Min(n1, n2), 2) + Math.Pow(Math.Max(n3, n4)- Math.Min(n3,n4), 2)));
        return dist;
    }
}
