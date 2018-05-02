using System;
using System.IO;
using System.Globalization;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Screenshot : MonoBehaviour {

	public Canvas canvas;
    public GameObject button;
    public GameObject display;

	private bool enableShoot = true;

	void Start () 
	{
        Button b = button.GetComponent<Button>();
        b.onClick.AddListener(TakeScreenshot);            
    }

	public void TakeScreenshot()
	{
		canvas.enabled = false;
		if (enableShoot)
		{
			enableShoot = false;
			StartCoroutine (shoot());
		}
	}

	IEnumerator shoot ()
	{
		DateTime time = DateTime.Now;
		var culture = new CultureInfo("de-DE");
		yield return(0);
		canvas.enabled = false;
		yield return new WaitForEndOfFrame();
        var str = time.ToString(culture);
        str = str.Replace(":", "-").Replace(" ", "_");
        Application.CaptureScreenshot ("Screenshot" + str + ".png");
		Texture2D screenshot = new Texture2D (Screen.width, Screen.height, TextureFormat.RGB24, true);
		screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		screenshot.Apply ();
		display.GetComponent<RawImage> ().texture = screenshot;
		while (!File.Exists ("Screenshot" + str + ".png")) 
		{
			yield return(0);
		}
		canvas.enabled = true;
		enableShoot = true;
	}
}
