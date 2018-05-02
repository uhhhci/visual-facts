using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundControl : MonoBehaviour {

    public Sprite sand;
    public Sprite gray;

    public void setBackground(int i)
    {
        switch (i)
        {
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = gray;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = sand;
                break;
        }
        
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
