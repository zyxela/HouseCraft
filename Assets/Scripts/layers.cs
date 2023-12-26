using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class layers : MonoBehaviour
{

    List<List<GameObject>> list = new List<List<GameObject>>();
    List<GameObject> blocks = new List<GameObject>();
    public float Ystart, Yend;
    public int lastElement; // прозpачный элемент
    public GameObject Permission;
    GameObject auds, win;

    int Count = 0;
    int CurrentLayer;

    public int coins =50000;
    void Start()
    {
       //PlayerPrefs.DeleteAll();
        win = GameObject.FindGameObjectWithTag("Win"); win.SetActive(false);
        auds = GameObject.FindGameObjectWithTag("Sound");
        Collect();
        GetPrefs();
        if (list.Count-1 != CurrentLayer)
        {
            SetUnActive();
            Uper(CurrentLayer);
            MOM();
            Children();
            if (CurrentLayer != 0) { Combiner2(); }

        }

        else
        {
            MOM();
            Children();
            Combiner2();
        }
       
    }


    bool wp = true;// win permission
    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Camera.main.GetComponent<CameraRotateAround>().sp = Vector3.zero;
            var selection = hit.transform;
            var selectionRend = selection.GetComponent<Renderer>();
            if ((selectionRend != null) && selectionRend.sharedMaterial == materials[lastElement])
            {
                if (selectionRend.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == currentSprite)
                {
                    auds.GetComponent<AudioSource>().Play();
                    selectionRend.material = ReturnMaterial(selectionRend.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
                    Destroy(selectionRend.transform.GetChild(0).gameObject);
                    Count++;
                    Permission.GetComponent<Permis>().permission2 = false;
                }


            }

        }

        else
            Permission.GetComponent<Permis>().permission2 = true;


     
        if(CurrentLayer < list.Count)
        {
            if (list[CurrentLayer].Count == Count)
            {
                Count = 0;
                CurrentLayer++;
                Uper(CurrentLayer);
                Combiner();
                PlayerPrefs.SetInt("CurrentLayer" + SceneManager.GetActiveScene().buildIndex, CurrentLayer);
            }
        }
        
        else if(wp)
        {
            win.SetActive(true);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins")+coins);
            wp = false;
           
        }
      
    }

    void Collect()
    {
        int x = 0;
        for (float i = Ystart; i < Yend + 1; i++)
        {
            blocks = new List<GameObject>();
            list.Add(blocks);
            for (int j = 0; j < transform.childCount; j++)
            {
                if (transform.GetChild(j).transform.position.y == i)
                {
                    list[x].Add(transform.GetChild(j).gameObject);
                }

            }
            x++;
        }

    }

    void SetUnActive()
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < list[i].Count; j++)
            {
                list[i][j].SetActive(false);
            }
        }
    }

    public GameObject rec;
    void Uper(int x)
    {
            for (int j = 0; j < list[x].Count; j++)
            {
                list[x][j].SetActive(true);
                GameObject recog = Instantiate(rec, new Vector3(list[x][j].transform.position.x, list[x][j].transform.position.y + 1.5f, list[x][j].transform.position.z), Quaternion.identity, list[x][j].transform);
                recog.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = MaterialsAndPictures(list[x][j].GetComponent<MeshRenderer>().sharedMaterial);
                list[x][j].GetComponent<MeshRenderer>().material = materials[lastElement];

            }
        
       
    }


    public List<Material> materials = new List<Material>();
    public List<Sprite> sprites = new List<Sprite>();
    Sprite MaterialsAndPictures(Material input_mat)
    {
        for (int i = 0; i < materials.Count - 1; i++)
        {
            if (input_mat == materials[i])
                return sprites[i];
        }
        return sprites[0];
    }

    Material ReturnMaterial(Sprite input_sprite)
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            if (input_sprite == sprites[i])
                return materials[i];
        }
        return materials[0];
    }

    public Sprite currentSprite = null;
    public void TakeSpriteFromBtn(UnityEngine.UI.Button btn)
    {
        currentSprite = btn.GetComponent<UnityEngine.UI.Image>().sprite;
    }


    void Combiner()
    {

        for (int z = 0; z < transform.GetChild(0).childCount; z++)
        {
            for (int m = 0; m < list[CurrentLayer - 1].Count; m++)
            {
                if (list[CurrentLayer - 1][m].GetComponent<MeshRenderer>().sharedMaterial == transform.GetChild(0).transform.GetChild(z).GetComponent<MeshRenderer>().sharedMaterial)
                {
                    list[CurrentLayer - 1][m].transform.parent = transform.GetChild(0).transform.GetChild(z);
                }
            }
        }


        for (int k = 0; k < transform.GetChild(0).childCount; k++)
        {
            MeshFilter[] meshFilters = transform.GetChild(0).transform.GetChild(k).GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];
            for (int i = 0; i < meshFilters.Length; i++)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            }


            transform.GetChild(0).transform.GetChild(k).GetComponent<MeshFilter>().mesh = new Mesh();
            transform.GetChild(0).transform.GetChild(k).GetComponent<MeshFilter>().mesh.CombineMeshes(combine);

            transform.GetChild(0).transform.GetChild(k).transform.localScale = new Vector3(1, 1, 1);
            transform.GetChild(0).transform.GetChild(k).transform.rotation = Quaternion.identity;
            transform.GetChild(0).transform.GetChild(k).transform.position = Vector3.zero;



        }

        for (int j = 0; j < list[CurrentLayer - 1].Count; j++)
        {
            Destroy(list[CurrentLayer - 1][j]);
        }

    }

    void Children()
    {

        for (int k = 0; k < materials.Count - 1; k++)
        {
            GameObject ngo = new GameObject();
            ngo.transform.parent = transform.GetChild(0);
            ngo.AddComponent<MeshRenderer>();
            ngo.AddComponent<MeshFilter>();
            ngo.GetComponent<MeshRenderer>().sharedMaterial = materials[k];

        }
    }

    void MOM()
    {
        GameObject ngo = new GameObject();
        ngo.transform.parent = transform;
        ngo.transform.SetSiblingIndex(0);
    }

    public void GetPrefs()
    {

        if (!PlayerPrefs.HasKey("CurrentLayer" + SceneManager.GetActiveScene().buildIndex))
        {
            CurrentLayer = 0;
        }
        else
        {
            CurrentLayer = PlayerPrefs.GetInt("CurrentLayer" + SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Combiner2()
    {
        for (int z = 0; z < transform.GetChild(0).childCount; z++)
        {
        for (int v = 0; v < CurrentLayer; v++)
            {
                for (int m = 0; m < list[v].Count; m++)
                {
                    if (list[v][m].GetComponent<MeshRenderer>().sharedMaterial == transform.GetChild(0).transform.GetChild(z).GetComponent<MeshRenderer>().sharedMaterial)
                    {
                        list[v][m].transform.parent = transform.GetChild(0).transform.GetChild(z);
                        list[v][m].SetActive(true);
                    }
                }
            }
           
        }   


        for (int k = 0; k < transform.GetChild(0).childCount; k++)
        {
            MeshFilter[] meshFilters = transform.GetChild(0).transform.GetChild(k).GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];
            for (int i = 0; i < meshFilters.Length; i++)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            }


            transform.GetChild(0).transform.GetChild(k).GetComponent<MeshFilter>().mesh = new Mesh();
            transform.GetChild(0).transform.GetChild(k).GetComponent<MeshFilter>().mesh.CombineMeshes(combine);

            transform.GetChild(0).transform.GetChild(k).transform.localScale = new Vector3(1, 1, 1);
            transform.GetChild(0).transform.GetChild(k).transform.rotation = Quaternion.identity;
            transform.GetChild(0).transform.GetChild(k).transform.position = Vector3.zero;



        }

        for (int y = 0; y < CurrentLayer; y++) 
        {

            for (int j = 0; j < list[y].Count; j++)
            { 
                Destroy(list[y][j]);
            }
        }
    }

}
