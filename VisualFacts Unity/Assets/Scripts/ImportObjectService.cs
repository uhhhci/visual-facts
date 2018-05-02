using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImportObjectService : MonoBehaviour
{

    //bool isWorking = false;
    //public Texture2D defaultTexture;


    //void Update()
    //{
    //    if (Input.GetKeyDown("l") && !isWorking)
    //    {
    //        isWorking = true;
    //        Debug.Log("Starting Import");
    //        ImportObjFile(url, UpdateProgress, ImportComplete);
    //    }
    //}

    public Transform objekt;
    public GameObject progress_bar;
    public GameObject progress_bar2;
    public Object_Interaction script;
    public DistanceScroller dist;


    void ImportComplete(GameObject go)
    {
        go.transform.position = new Vector3(1, 2, 3);
    }

    void OnApplicationQuit()
    {
        ObjImporter.StopAllThreads();
        Debug.Log("QUIT");
    }

    //public Texture2D[] textures;
    //public string url = "http://orbcreation.com/SimpleObj/Ayumi.obj";

    /// <summary>
    /// Download obj from url (e.g. "http://orbcreation.com/SimpleObj/Ayumi.obj") or from file
    /// (e.g. "file:///C:/Users/admin/Documents/TEMP/Leiden23/Leiden_23_60k_v2.obj")
    /// </summary>
    /// <param name="path"></param>
    public void ImportObjFile(string path, Action<float> progressCallback, Action<GameObject> resultCallback)
    {
        StartCoroutine(DownloadAndImportAllInBackground(path, progressCallback, resultCallback));
    }

    private void UpdateProgress(float obj)
    {
        Debug.Log("Progress: " + obj + "%");
        
        //EditorGUI.ProgressBar(new Rect(3, 45, position.width - 6, 20), armor / 100, "Armor");
    }

    private IEnumerator DownloadAndImportAllInBackground(string url, Action<float> progressCallback, Action<GameObject> result)
    {
        string objString = null;
        string mtlString = null;
        Hashtable textures = null;
        GameObject importedObject = null;
        Texture2D tex = null;

        progress_bar2.SetActive(true);
        objekt.transform.position = Vector3.zero;

        yield return StartCoroutine(DownloadFile(url, retval => objString = retval));
        //yield return StartCoroutine(DownloadFile(url.Substring(0, url.Length - 4) + ".mtl", retval => mtlString = retval));
        yield return StartCoroutine(DownloadFile(url + ".mtl", retval => mtlString = retval));
        if (mtlString != null && mtlString.Length > 0)
        {
            string path = url;
            int lastSlash = path.LastIndexOf('/', path.Length - 1);
            if (lastSlash >= 0) path = path.Substring(0, lastSlash + 1);
            Hashtable[] mtls = ObjImporter.ImportMaterialSpecs(mtlString);
            for (int i = 0; i < mtls.Length; i++)
            {
                if (mtls[i].ContainsKey("mainTexName"))
                {
                    Texture2D texture = null;
                    string texUrl = path + mtls[i]["mainTexName"];
                    yield return StartCoroutine(DownloadTexture(texUrl, retval => texture = retval));
                    if (texture != null)
                    {
                        if (textures == null) textures = new Hashtable();
                        textures[mtls[i]["mainTexName"]] = texture;
                        tex = texture;
                    }
                }
            }
        }

        
        //yield return StartCoroutine(DownloadFile(url, retval => objString = retval));

        if (objString != null && objString.Length > 0)
        {
            yield return StartCoroutine(ObjImporter.ImportInBackground(objString, mtlString, textures, r => importedObject = r, progressCallback));
        }

        if (importedObject.transform.childCount == 0)
        {
            importedObject.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            importedObject.GetComponent<Renderer>().material.SetTexture("_MainTex", tex);

            result(importedObject);
            importedObject.AddComponent<MeshCollider>();
            importedObject.transform.localScale = new Vector3(-1, 1, 1);
            importedObject.transform.parent = objekt;
            GameObject.Find("Scene").GetComponent<UseShader>().init();
        }
        else
        {
            var count = importedObject.transform.childCount;
            for (int i = 0; i < count; i++)
            {
                

                var obj = importedObject.transform.GetChild(0);

                obj.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
                obj.GetComponent<Renderer>().material.SetTexture("_MainTex", tex);

                obj.transform.localScale = new Vector3(-1, 1, 1); // Spiegelt das importierte Objekt, geändert
                obj.gameObject.AddComponent<MeshCollider>();
                obj.gameObject.transform.parent = objekt;
                result(obj.gameObject);
                GameObject.Find("Scene").GetComponent<UseShader>().init();
            }
        }
        Debug.Log("Done");
        progress_bar.SetActive(false);
        progress_bar2.SetActive(false);
        progress_bar.GetComponent<Slider>().value = 0;
        progress_bar2.GetComponent<Slider>().value = 0;

        script.resetFront();
        script.zentrum();
        dist.setSize();
    }

    private IEnumerator DownloadFile(string url, System.Action<string> result)
    {
        Debug.Log("Downloading " + url);
        WWW www = new WWW(url);

        while (!www.isDone)
        {
            Debug.Log("Progress: " + www.progress * 100.0 + "%");
            progress_bar2.GetComponent<Slider>().value = www.progress*100;
            yield return new WaitForSeconds(0.1f);
        }

        //yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Downloaded " + www.bytesDownloaded + " bytes");
            progress_bar2.GetComponent<Slider>().value = 100;
        }
        result(www.text);
    }

    private IEnumerator DownloadTexture(string url, System.Action<Texture2D> result)
    {
        Debug.Log("Downloading " + url);
        WWW www = new WWW(url);
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Downloaded " + www.bytesDownloaded + " bytes");
        }
        result(www.texture);
    }

}
