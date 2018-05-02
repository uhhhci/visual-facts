using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchSliderDrag : MonoBehaviour {

	private bool drag = false;
	// Use this for initialization
	void Start () {
		
	}

	public void startDrag()
	{
		drag = true;
		gameObject.SetActive (true);
	}

	private void stopDrag()
	{
		drag = false;
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (drag && !Input.GetMouseButton(0)) {
			stopDrag ();
		}
	}
}
