using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class matchSliderValue : MonoBehaviour {

	public void setValue(Slider s)
	{
		GetComponent<InputField>().text = Mathf.Round(s.value * -5 + 100).ToString();
	}
	// Use this for initialization
	void Start () {
		GetComponent<InputField>().text = 100.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
