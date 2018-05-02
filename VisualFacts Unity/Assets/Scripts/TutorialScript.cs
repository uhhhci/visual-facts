using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour {

    private int count = 0;
    public GameObject part1;
    public GameObject part2;
    public GameObject part3;
    public GameObject part4;
    public GameObject part5;
    public GameObject part6;
    public GameObject part7;
    public GameObject part8;
    public GameObject ImReady;
    public GameObject ContinueButton;
    public GameObject BackButton;
    public GameObject DontShowButton;
    public Object_Interaction oi;

    void Start () {
		
	}

    void Update()
    {
        if(oi.frontMode)
        {
            ContinueButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
            BackButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        }
        else
        {
            ContinueButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            BackButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
    }

    public void Back()
    {
        if (!oi.frontMode)
        {
            count -= 2;
            Continue();
        }
    }

    public void DontShow()
    {
        count = 7;
        Continue();
    }

    public void Continue()
    {
        switch (count)
        {
            case 0:
                disable();
                part1.SetActive(false);
                part2.SetActive(true);
                BackButton.SetActive(false);
                DontShowButton.SetActive(false);
                count++;
                break;
            case 1:
                disable();
                part2.SetActive(false);
                part3.SetActive(true);
                BackButton.SetActive(true);
                count++;
                break;
            case 2:
                disable();
                part3.SetActive(false);
                part4.SetActive(true);
                count++;
                oi.resetFront();
                break;
            case 3:
                if (!oi.frontMode)
                {
                    disable();
                    part4.SetActive(false);
                    part5.SetActive(true);
                    count++;
                }
                break;
            case 4:
                disable();
                part5.SetActive(false);
                part6.SetActive(true);
                count++;
                break;
            case 5:
                disable();
                part6.SetActive(false);
                part7.SetActive(true);
                count++;
                break;
            case 6:
                disable();
                part7.SetActive(false);
                part8.SetActive(true);
                ContinueButton.SetActive(false);
                ImReady.SetActive(true);
                count++;
                break;
            case 7:
                disable();
                part8.SetActive(false);
                ImReady.SetActive(false);
                BackButton.SetActive(false);
                DontShowButton.SetActive(false);
                ContinueButton.SetActive(false);
                count++;
                break;
        }
        }

    private void disable()
    {
        part1.SetActive(false);
        part2.SetActive(false);
        part3.SetActive(false);
        part4.SetActive(false);
        part5.SetActive(false);
        part6.SetActive(false);
        part7.SetActive(false);
        part8.SetActive(false); 
    }

    public void resetTut()
    {
        count = 0;
        part1.SetActive(true);
        BackButton.SetActive(false);
        DontShowButton.SetActive(true);
        ContinueButton.SetActive(true);
    }
}

