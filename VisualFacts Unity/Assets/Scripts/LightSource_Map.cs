using UnityEngine;

public class LightSource_Map : MonoBehaviour
{
    public GameObject lockedLight;
    public GameObject unlockedLight;
    public GameObject cam;
    public GameObject sonne;
    public GameObject map;

    private RectTransform rect;
	private bool drag;
    
    void Start()
	{
		rect = map.GetComponent<RectTransform>();
		drag = false;
	}

	void LightPosition(Vector3 possonne, Vector3[] v, Vector3 center, float radius)
	{
        var Licht = unlockedLight;
        //Quaternion ursprung = new Quaternion (0, 0, 0, 1);//(0.7f, 0, 0, 0.7f);
        Quaternion ursprung = cam.transform.localRotation;
        ursprung.eulerAngles=new Vector3(0, -90, 0);
		Vector3 Nullpunkt = new Vector3 (0, 0, 0);
        Licht.transform.SetPositionAndRotation (Nullpunkt , ursprung);
        //Licht.transform.localRotation = ursprung;
        float width = (v [3].x) - (v [0].x);
		float height = width;
		float posx = (v [3].x) - possonne.x;
		float posy = (v [1].y) - possonne.y;
		float anteilx = ((posx / width) - 0.5f) * (-2);
		float anteily = ((posy / height) - 0.5f) * (-2);
		float distance = Vector3.Distance (possonne, center);
		float gradzahlx = (distance / radius * 90 - 90) * (-1);
		float gradzahly = Vector2.Angle (new Vector2 (0, 1), new Vector2 (anteilx, anteily));
		if (anteilx < 0) 
		{
            Licht.transform.Rotate (-gradzahlx, gradzahly, 0);
        } else 
		{
			gradzahly = Vector2.Angle (new Vector2 (0, -1), new Vector2 (anteilx, anteily));
			Licht.transform.Rotate (-gradzahlx, (gradzahly + 180), 0);
        }
	}

	void Update()
	{
		Vector3 possonne = sonne.transform.position;
		Vector3 mouse = Input.mousePosition;
		Vector3[] v = new Vector3[4];
		rect.GetWorldCorners (v);
		var posxmin = v [0].x;
		var posymax = v [1].y;
		var posxmax = v [3].x;
		var posymin = v [0].y;
		float radius = (posxmax - posxmin) / 2;
		Vector3 centerPosition;
		centerPosition.x = (posxmin + radius);
		centerPosition.y = (posymin + radius);
		centerPosition.z = 0;
		float distance = Vector3.Distance(mouse, centerPosition);

		if ((mouse.x > posxmin) & (mouse.y < posymax) & (mouse.x < posxmax) & (mouse.y > posymin)) 
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				drag = true;
			}
		}
		if (drag & (distance < radius)) 
		{
			sonne.transform.position = new Vector3 (mouse.x, mouse.y, 0);
			LightPosition (sonne.transform.position, v, centerPosition, radius);
		}
		if (drag & (distance > radius)) 
		{
			Vector3 bleibImKreis = mouse - centerPosition;
			bleibImKreis *= radius / distance;
			sonne.transform.position = bleibImKreis + centerPosition;
			LightPosition (sonne.transform.position, v, centerPosition, radius);
		}
		if (Input.GetMouseButtonUp (0)) 
		{
			drag = false;
		}
	}
}