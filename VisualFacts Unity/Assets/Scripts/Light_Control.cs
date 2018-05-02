
using UnityEngine;
using UnityEngine.UI;

public class Light_Control : MonoBehaviour {
    public GameObject lighting;
    public GameObject camControl;
    public GameObject lightMap;
    public Transform sonne;
    public RectTransform lockLightButton;

    private bool lightIsLocked = false;
    private RectTransform map;
    private bool drag;  

    void Start()
    {
        map = lightMap.GetComponent<RectTransform>();
        drag = false;
    }

    void lightPosition(Vector3[] mapBoundaries, Vector3 center, float radius)
    {
        lighting.transform.localRotation = camControl.transform.rotation;
        float mapSize = (mapBoundaries[3].x) - (mapBoundaries[0].x);
        float anteilx = (((mapBoundaries[3].x - sonne.position.x) / mapSize) - 0.5f) * (-2);
        float anteily = (((mapBoundaries[1].y - sonne.position.y) / mapSize) - 0.5f) * (-2);

        float distance = Vector3.Distance(sonne.position, center);

		float gradzahlx = Vector2.Angle(new Vector2(-1,0), new Vector2(anteilx, anteily));
		float gradzahly = (distance / radius) * 90;
		/*if (anteily < 0) {
			gradzahlx = 360 - gradzahlx;
			/*if (anteilx > 0) {
				//gradzahly *= -1;
				//gradzahly += 180;

				lighting.transform.eulerAngles += new Vector3(gradzahlx,-gradzahly+180,0);

			} else {
				lighting.transform.eulerAngles += new Vector3(gradzahlx,gradzahly,0);

			}
		} else {
			if (anteilx > 0) {
				//gradzahly *= -1;
				//gradzahly += 180;

				lighting.transform.eulerAngles += new Vector3(gradzahlx,-gradzahly+180,0);

			} else {
				lighting.transform.eulerAngles += new Vector3(gradzahlx,gradzahly,0);

			}
			
		}
        if (anteilx > 0)
        {
            gradzahlx = Vector2.Angle(new Vector2(1, 0), new Vector2(anteilx, anteily));
        }
        lighting.transform.eulerAngles += new Vector3(gradzahlx, gradzahly, 0);*/
        /*
		if (anteilx > 0) {
			//gradzahly *= -1;
			//gradzahly += 180;

			lighting.transform.localEulerAngles += new Vector3(gradzahlx,-gradzahly+180,180);

		} else {
			lighting.transform.localEulerAngles += new Vector3(gradzahlx,gradzahly,0);
				
		}*/
        //Debug.Log ("glo"+lighting.transform.eulerAngles);
        //Debug.Log ("loc"+lighting.transform.localEulerAngles);

        /*if (anteilx > 0) {
			gradzahly *= -1;
			gradzahlx *= -1;
		} *//*else if (anteilx == 0)
		{
			gradzahly = 0;
			gradzahlx = (distance / radius + 1) * 90;
		}*/
            //Debug.Log(gradzahlx + "," + gradzahly);
            //lighting.transform.Rotate(gradzahlx + 90,gradzahly,0);


        // Lineare Berechnung. Für exakte Werte müssen Distanz/Radius einbezogen werden
        lighting.transform.Rotate(anteily*90, anteilx * -90, 0);   
    }

    void updateLight()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3[] mapBoundaries = new Vector3[4];
        map.GetWorldCorners(mapBoundaries);
        var posxmin = mapBoundaries[0].x;
        var posymax = mapBoundaries[2].y;
        var posxmax = mapBoundaries[2].x;
        var posymin = mapBoundaries[0].y;
        float radius = (posxmax - posxmin) / 2;
        Vector3 mapCenter;
        mapCenter.x = (posxmin + radius);
        mapCenter.y = (posymin + radius);
        mapCenter.z = 0;
        float distance = Vector3.Distance(mouse, mapCenter);

        if ((mouse.x > posxmin) & (mouse.y < posymax) & (mouse.x < posxmax) & (mouse.y > posymin))
        {
            if (Input.GetMouseButtonDown(0))
            {
                drag = true;
            }
        }
        if (drag & (distance < radius))
        {
            sonne.position = new Vector3(mouse.x, mouse.y, 0);
        }
        if (drag & (distance > radius))
        {
            Vector3 bleibImKreis = mouse - mapCenter;
            bleibImKreis *= radius / distance;
            sonne.position = bleibImKreis + mapCenter;
        }
        lightPosition(mapBoundaries, mapCenter, radius);
        if (Input.GetMouseButtonUp(0))
        {
            drag = false;
        }
    }

    public void lockLight()
    {
        lightIsLocked = !lightIsLocked;
        if (lightIsLocked)
        {
            lockLightButton.GetComponent<Image>().color = new Color(1,1,1,0.3f);
        }
        else
        {
            lockLightButton.GetComponent<Image>().color = new Color(1,1,1,1);
        }
    }

    void Update()
    {
        if (!lightIsLocked)
        {
            updateLight();
        }
    }
}
