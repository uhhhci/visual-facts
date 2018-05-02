using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFB;
using UnityEngine.UI;
//using UnityEditor;




public class ImportExample : MonoBehaviour {

    //private string path = @"file:///C:/Users/hci/Desktop/Leiden Objekt/Leiden_23_60k_v2.obj";

    public GameObject objekt;
    public GameObject progress_bar;

    void Start()
    {
        
    }


    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("i"))
        {
            import();
        }
        if (p != 0)
        {
            progress_bar.SetActive(true);
        }
        else
        {
            progress_bar.SetActive(false);
        }
        progress_bar.GetComponent<Slider>().value = p;
        
	}

    public void import()
    {

        string[] paths = StandaloneFileBrowser.OpenFilePanel("Choose a file to import", "", "obj", false);
		if (paths.Length != 0)
		{
			string path = "file:///" + paths[0];
            path = path.Replace("\\", "/");
            Debug.Log(path);
            GetComponent<ImportObjectService>().ImportObjFile(path, progressUpdate, resultCallback);
            
            for (int i = 0; i < objekt.transform.childCount; i++)
            {
                GameObject.Destroy(objekt.transform.GetChild(i).gameObject);
            }
        }
        
    }

    private void resultCallback(GameObject obj)
    {
        obj.transform.position = new Vector3(0, 0, 0);
        //obj.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);
        //obj.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
        obj.GetComponent<Renderer>().material.SetFloat("_Glossiness", 0.0f);
        p = 0;
    }

    float p = 0;

    private void progressUpdate(float percent)
    {
        Debug.Log("Progress: " + percent + "%");
        p = percent;
    }
}
