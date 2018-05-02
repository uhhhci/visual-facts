using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mausrotation : MonoBehaviour {
    public Transform camControl;
    public Transform cam;
    public float zoomSpeed;
    public float speed;
    private Vector3 position;
    private bool click;

	// Use this for initialization
	void Start () {
        position = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)||click)
        {
            click = true;
            camControl.localRotation *= Quaternion.Euler(0,
                                        (Input.mousePosition[0] - position[0]) / Screen.width * speed,   
                                        -(Input.mousePosition[1] - position[1]) / Screen.height * speed);
            Debug.Log(""+(Input.mousePosition[0] - position[0]) + (-1*(Input.mousePosition[1] - position[1])));
        }
        if(Input.GetMouseButtonUp(0))
        {
            click = false;
        }
        position = Input.mousePosition;		
	}
}
