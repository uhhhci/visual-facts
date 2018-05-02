using System;
using System.IO;
using System.Globalization;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Interaction : MonoBehaviour {

    public Canvas canvas;
    public GameObject screenshotButton;
    public GameObject screenshotDisplay;
    public GameObject rahmen;
    public Camera camera;
    public Animator anim;

    private int HEIGHT = 2160;
    private int WIDTH = 3840;

    static UI_Interaction()
    {
#if UNITY_STANDALONE_OSX
        Debug.Log("Runs on OSX");
#elif UNITY_STANDALONE_WIN
        if(!Directory.Exists("./Screenshots"))
        {
            Directory.CreateDirectory("./Screenshots");
        }
        Debug.Log("Runs on Windows");
#endif
    }

    void Start()
    {
    }

    public void setResolution(int i)
    {
        switch(i)
        {
            case 1:
                WIDTH = 1280;
                HEIGHT = 720;
                
                    break;
            case 2:
                WIDTH = 1920;
                HEIGHT = 1080;
                    break;
            case 3:
                WIDTH = 3840;
                HEIGHT = 2160;
                    break;
            case 4:
                WIDTH = 5120;
                HEIGHT = 2880;
                break;

        }
        Debug.Log(i);
    }

    public void TakeScreenshot()
    {
        // ANimation???
        //anim.SetTrigger("TakeScreenshot");
        //if(anim.GetCurrentAnimatorStateInfo(0).IsName("Flash idle"))
        //{
            //Debug.Log("flash");
            Texture2D screenshotPreview = screenshotTexture(400, 225);
            screenshotDisplay.GetComponent<RawImage>().texture = screenshotPreview;

            Texture2D screenshot = screenshotTexture(WIDTH, HEIGHT);
            byte[] bytes = screenshot.EncodeToPNG();
            System.IO.File.WriteAllBytes(screenshotName(), bytes);
        //}

        
    }

    private String screenshotName()
    {
        DateTime time = DateTime.Now;
        var culture = new CultureInfo("de-DE");
        var str = time.ToString(culture);
        str = str.Replace(":", "-").Replace(" ", "_");
        return "./Screenshots/Screenshot" + str + ".png";
    }

    private Texture2D screenshotTexture(int width, int height)
    {
        RenderTexture rt = new RenderTexture(width, height, 24);
        RenderTexture org = RenderTexture.active;
        RenderTexture.active = rt;

        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, true);
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.targetTexture = rt;
        camera.Render();
        screenshot.ReadPixels(new Rect(0, 0, WIDTH, height), 0, 0);
        screenshot.Apply();
        camera.targetTexture = null;
        RenderTexture.active = org;
        Destroy(rt);
        camera.clearFlags = CameraClearFlags.Depth;
        rahmen.SetActive(true);
        return screenshot;
    
    }
}