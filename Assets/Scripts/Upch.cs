using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.Animations;

public class creativecamera : MonoBehaviour
{

    public void UpChildren()
    {
        transform.SetSiblingIndex(0);
    }

}