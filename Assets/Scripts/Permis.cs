using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permis : MonoBehaviour
{
    public bool permission = true;// - отвечает за текущий режим камеры: вращение или перемещение
    public bool permission2 = true; // - разрешение на вращение/перемещение камеры

    int i = 1;
    public void IfDown()
    {
        if (i == 1)
        {
            permission = false;
            i *= -1;
        }

        else
        {
            permission = true;
            i *= -1;
        }
            
    }
    public void Totrue()
    {
        permission2 = true;
    }

    public void Tofalse()
    {
        permission2 = false;
    }


    
   
}
