using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RECOGLookAT : MonoBehaviour
{

    void FixedUpdate()
    {
        transform.LookAt(Camera.main.transform.GetChild(1).transform);
    }
}
