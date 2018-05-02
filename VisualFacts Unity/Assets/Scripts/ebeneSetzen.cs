using System.Collections.Generic;
using UnityEngine;

public class ebeneSetzen : MonoBehaviour {
    public Camera cam;
    public GameObject camCtr;
    public GameObject otafel;
    public GameObject m;
    private Transform tafel;
    private List<Vector3> punkte = new List<Vector3>();

    // Use this for initialization
    void Start () {
        tafel = otafel.GetComponent<Transform>();  
        zentrum();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
                if (hit.collider != null)
                {
                    //hit.collider.enabled = false;
                    var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    go.transform.localScale -= new Vector3(0.9f, 0.9f, 0.9f);
                    go.transform.position = hit.point;
                    punkte.Add(hit.point);
                }
        }
        if (Input.GetKeyDown("f"))
        {
            ebene();
        }
        else
        {
            var sonne = GameObject.Find("Sun");
            var LS = GameObject.Find("LightSourceMap");
            Vector3 possonne = sonne.transform.position;
            Vector3 mouse = Input.mousePosition;
            Vector3[] v = new Vector3[4];
            LS.GetComponent<RectTransform>().GetWorldCorners(v);
            var posxmin = v[0].x;
            var posymax = v[1].y;
            var posxmax = v[3].x;
            var posymin = v[0].y;
            float radius = (posxmax - posxmin) / 2;
            Vector3 centerPosition;
            centerPosition.x = (posxmin + radius);
            centerPosition.y = (posymin + radius);
            centerPosition.z = 0;
            float distance = Vector3.Distance(mouse, centerPosition);

            LightPosition(sonne.transform.position, v, centerPosition, radius);
        }
        if (Input.GetKeyDown("b"))
        {
            zentrum();
        }
	}

    void LightPosition(Vector3 possonne, Vector3[] v, Vector3 center, float radius)
    {
        var Licht = GameObject.Find("cam light");
        //Quaternion ursprung = new Quaternion (0, 0, 0, 1);//(0.7f, 0, 0, 0.7f);
        Quaternion ursprung = cam.transform.localRotation;
        ursprung.eulerAngles = new Vector3(0, -90, 0);
        Vector3 Nullpunkt = new Vector3(0, 0, 0);
        Licht.transform.SetPositionAndRotation(Nullpunkt, ursprung);
        //Licht.transform.localRotation = ursprung;
        float width = (v[3].x) - (v[0].x);
        float height = width;
        float posx = (v[3].x) - possonne.x;
        float posy = (v[1].y) - possonne.y;
        float anteilx = ((posx / width) - 0.5f) * (-2);
        float anteily = ((posy / height) - 0.5f) * (-2);
        float distance = Vector3.Distance(possonne, center);
        float gradzahlx = (distance / radius * 90 - 90) * (-1);
        float gradzahly = Vector2.Angle(new Vector2(0, 1), new Vector2(anteilx, anteily));
        if (anteilx < 0)
        {
            Licht.transform.Rotate(-gradzahlx, gradzahly, 0);
        }
        else
        {
            gradzahly = Vector2.Angle(new Vector2(0, -1), new Vector2(anteilx, anteily));
            Licht.transform.Rotate(-gradzahlx, (gradzahly + 180), 0);
        }
    }

    void ebene()
    {
        var plane = new Plane(punkte[0], punkte[1], punkte[2]);

        if (Vector3.Angle(plane.normal, new Vector3(0,-1,0)) > 90)
        {
            tafel.rotation = Quaternion.FromToRotation(plane.normal, new Vector3(0, -1, 0));
        }
        else
        {
            tafel.rotation = Quaternion.FromToRotation(plane.normal, new Vector3(0, 1, 0));
        }
        camCtr.transform.rotation = Quaternion.Euler(0, 0, 90);
        camCtr.transform.Rotate(180, 0, 0);
        zentrum();
    }

    void zentrum()
    {
        var rs = otafel.GetComponentsInChildren<Renderer>();
        var b = rs[0].bounds;

        for (int i = 1; i < tafel.childCount; i++)
        {
            b.Encapsulate(rs[i].bounds);
        }
        //camCtr.transform.position = b.center;
        /*
        var me = m.GetComponent<MeshFilter>();
        Debug.Log("start "+Time.realtimeSinceStartup);
        Debug.Log(me.mesh.vertexCount);
        var mittelmittel = Vector3.zero;
        for (int i = 0; i < tafel.childCount; i++)
        {
            var mittelwert = Vector3.zero;
            for (int j = 0; j < tafel.GetChild(i).GetComponent<MeshFilter>().mesh.vertexCount; j++)
            {
                var x = tafel.GetChild(i).GetComponent<MeshFilter>().mesh.vertices[j];
                mittelwert += x;
            }
            Debug.Log(mittelwert);
            mittelwert /= tafel.GetChild(i).GetComponent<MeshFilter>().mesh.vertexCount;
            mittelmittel += mittelwert;
            Debug.Log(tafel.GetChild(i).GetComponent<MeshFilter>().mesh.vertexCount);
        }
        mittelmittel /= tafel.childCount;
        Debug.Log(mittelmittel.x);
        Debug.Log(mittelmittel.y);
        Debug.Log(mittelmittel.z);
        GameObject go = new GameObject();
        go.transform.position = mittelmittel;
        Debug.Log("end " + Time.realtimeSinceStartup);*/
    }
}
