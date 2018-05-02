using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public UseShader sh;
    public UI_Interaction ui;
    public backgroundControl back;
    public ObjectColor oc;

    public GameObject objColorW;
    public GameObject objColorS;
    public GameObject objColorG;
    public GameObject objColorOrg;

    public GameObject backColorG;
    public GameObject backColorS;

    public GameObject shColorB;
    public GameObject shColorR;
    public GameObject shColorG;

    public GameObject resHD;
    public GameObject resFHD;
    public GameObject res4K;
    public GameObject res5K;

    public void pressButton(Image i)
    {
        Debug.Log("x");
    }

    public void pressObj(GameObject go)
    {
        disableObject();
        //Aufrug des Material ändern
        switch (go.name)
        {
            case "ObjectColorWhite":
                oc.setMaterial(1);
                objColorW.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                break;
            case "ObjectColorSand":
                oc.setMaterial(3);
                objColorS.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                break;
            case "ObjectColorGray":
                oc.setMaterial(2);
                objColorG.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                break;
            case "ObjectColorOrg":
                oc.setMaterial(4);
                objColorOrg.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                break;
        }
    }

    public void pressRes(GameObject go)
    {
        disableRes();
        switch (go.name)
        {
            case "Res720":
                resHD.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                ui.setResolution(1);
                break;
            case "Res1080":
                resFHD.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                ui.setResolution(2);
                break;
            case "Res4k":
                res4K.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                ui.setResolution(3);
                break;
            case "Res5k":
                res5K.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
                ui.setResolution(4);
                break;
        }
    }

    public void pressShColor(GameObject go)
    {
        disableShader();
        switch (go.name)
        {
            case "ShaderColorRed":
                shColorR.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                sh.setShaderColor(1);
                break;
            case "ShaderColorGreen":
                shColorG.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                sh.setShaderColor(2);
                break;
            case "ShaderColorBlue":
                shColorB.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                sh.setShaderColor(3);
                break;
        }
    }

    public void pressBackColor(GameObject go)
    {
        disableBackground();
        switch(go.name)
        {
            case "BackColorGray":
                backColorG.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                back.setBackground(1);
                break;
            case "BackColorSand":
                backColorS.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
                back.setBackground(2);
                break;
        }
    }

    //Zwischenlösung (Alpha = eins mit buttons)
    public void disableRes()
    {
        resHD.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        resFHD.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        res4K.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        res5K.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    public void disableShader()
    {
        shColorB.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        shColorR.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        shColorG.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    public void disableBackground()
    {
        backColorG.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        backColorS.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    public void disableObject()
    {
        objColorW.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        objColorG.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        objColorS.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        objColorOrg.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }

    public void closeSettings()
    {
        gameObject.SetActive(false);
    }
    
    // Use this for initialization
	void Start () {
        backColorG.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        shColorB.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        resFHD.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        objColorG.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
