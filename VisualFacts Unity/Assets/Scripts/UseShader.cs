using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UseShader : MonoBehaviour {

	public Material AngleMat;
    public Material PlaneMat;
	private Material OldMat;
	public Transform CamTransform;
    public GameObject Objekt;
	private GameObject[] Tafeln;
	public GameObject sliderAng;
    public GameObject sliderPlane;
    public GameObject sliderPanel;
	public GameObject CloseShaderButton;
    public GameObject ColorFilterPanel;
    public DistanceScroller dist;

    private Vector4 shaderColor;

    void Start()
    {
        init();
        setShaderColor(3);
    }

    public void init()
    {
        Tafeln = new GameObject[Objekt.transform.childCount];
        for (int i = 0; i < Objekt.transform.childCount; i++)
        {
            Tafeln.SetValue(Objekt.transform.GetChild(i).gameObject, i); 
        }
        
    }

    public void setShaderColor(int c)
    {
        dist.setColor(c);
        setAngleColor(c);
    }

    private void setAngleColor(int c)
    {
        switch (c)
        {
            case 1:
                shaderColor = new Vector4(1f, 0f, 0f, 1f);
                break;
            case 2:
                shaderColor = new Vector4(0f, 1f, 0f, 1f);
                break;
            case 3:
                shaderColor = new Vector4(0f, 0f, 1f, 1f);
                break;
        }
    }

    public void UseShaderAngle (Slider sli)
	{
		AngleMat.SetFloat ("_Angle", sli.value);
	}

    public void ActivateShaderPlane()
    {
        OldMat = Tafeln[0].GetComponent<MeshRenderer>().material;
        for (int i = 0; i < Objekt.transform.childCount; i++)
        {
            Tafeln[i].GetComponent<MeshRenderer>().sharedMaterial = PlaneMat;
            Tafeln[i].GetComponent<MeshRenderer>().sharedMaterial.shader = Shader.Find("Unlit/DistanceColor");
        }

        var bounds = new Bounds(Vector3.zero, Vector3.one);
        MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
        }

        sliderPanel.SetActive(true);
        sliderPlane.SetActive(true);
        CloseShaderButton.SetActive(true);
    }

	public void ActivateShaderAngle()
	{
        OldMat = Tafeln[0].GetComponent<MeshRenderer>().material;
        for (int i = 0; i < Objekt.transform.childCount; i++)
        {
            Tafeln[i].GetComponent<Renderer>().sharedMaterial = AngleMat;
            Tafeln[i].GetComponent<MeshRenderer>().sharedMaterial.shader = Shader.Find("Custom/NormalShader");
        }
		sliderAng.SetActive (true);
        ColorFilterPanel.SetActive(true);
		CloseShaderButton.SetActive (true);
	}

	public void DeavtivateShaders()
	{
        for (int i = 0; i < Objekt.transform.childCount; i++)
        {
            Tafeln[i].GetComponent<Renderer>().sharedMaterial.shader = Shader.Find("Standard");
            Tafeln[i].GetComponent<Renderer>().material = OldMat;
        }
		sliderAng.SetActive (false);
        sliderPanel.SetActive(false);
        sliderPlane.SetActive(false);
        CloseShaderButton.SetActive (false);
        ColorFilterPanel.SetActive(false);
    }


	void Update ()
	{
		Vector3 v = CamTransform.position.normalized;
		AngleMat.SetVector ("_ClipPlaneNormal", new Vector4 (v.x, v.y, v.z, 0f));
        AngleMat.SetVector("_ShaderColor", shaderColor);
    }
}