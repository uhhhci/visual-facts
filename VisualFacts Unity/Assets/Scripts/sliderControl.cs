using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderControl : MonoBehaviour {

	public void addIntensity(float value)
	{
		GetComponent<Slider> ().value += value;
	}

	public void setIntensity(InputField textField)
	{
		GetComponent<Slider> ().value = int.Parse(textField.text);
	}
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}
