using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonClickBleibDaScript : MonoBehaviour {


    public GameObject ColorFilter;
    public GameObject PlaneFilter;
    public GameObject colorSlider;
    public GameObject planeSlider;
    public GameObject panelSlider;
    public GameObject Back;
    public GameObject Menu;
    public GameObject newBack;
    public GameObject RahmenColor;
    public GameObject RahmenPlane;
    public GameObject ColorFilterPanel;


    public void aktiviereColorFilter()
    {
        ColorFilter.SetActive(false);
        Back.SetActive(false);
        newBack.SetActive(true);
        Menu.SetActive(false);
        RahmenColor.SetActive(true);
    }

    public void aktivierePlaneFilter()
    {
        PlaneFilter.SetActive(false);
        Back.SetActive(false);
        newBack.SetActive(true);
        Menu.SetActive(false);
        RahmenPlane.SetActive(true);
    }

    public void deaktiviereFilter()
    {
        ColorFilter.SetActive(true);
        PlaneFilter.SetActive(true);
        Back.SetActive(true);
        newBack.SetActive(false);
        Menu.SetActive(true);
        RahmenColor.SetActive(false);
        RahmenPlane.SetActive(false);
        colorSlider.SetActive(false);
        planeSlider.SetActive(false);
        panelSlider.SetActive(false);
        ColorFilterPanel.SetActive(false);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
